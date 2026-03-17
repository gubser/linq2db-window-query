module LinqToDbQuery10_0_201

open System
open System.Linq
open NUnit.Framework
open NodaTime
open Npgsql
open LinqToDB
open LinqToDB.Data
open LinqToDB.Mapping
open LinqToDB.DataProvider.PostgreSQL
open LinqToDB.FSharp

type MyDataConnection(dataOptions: DataOptions) =
    inherit DataConnection(dataOptions)

type Template = 
    {
        [<Column("id")>]
        TemplateId: int

        [<Column("parent_id")>]
        ParentId: int

        [<Column("date", DataType=DataType.Date)>]
        ValidFrom: LocalDate
    }

let createDataOptions () =
    let connectionString = "Host=postgres;Port=5432;Username=postgres;Password=postgres;Database=postgres;Include Error Detail=true"

    let dataSource = 
        let builder = new NpgsqlDataSourceBuilder(connectionString)
        builder.UseNodaTime() |> ignore<TypeMapping.INpgsqlTypeMapper>
        builder.Build()

    let dataProvider = PostgreSQLTools.GetDataProvider(connectionString = connectionString)

    let mappingSchema = MappingSchema()
    mappingSchema.AddScalarType(typeof<LocalDate>)
    mappingSchema.AddScalarType(typeof<DateInterval>)

    (new DataOptions())
        .UseConnectionFactory(dataProvider, (fun _ -> dataSource.CreateConnection()))
        .UseMappingSchema(mappingSchema)
        .UseFSharp()

let dataOptions = createDataOptions()

[<Sql.Expression("{0} @> {1}", PreferServerSide = true)>]
let dateIntervalContains (d: DateInterval) (e: LocalDate) = d.Contains(e)

LinqToDB.Linq.Expressions.MapMember<DateInterval, LocalDate, bool>(
    (fun a b -> a.Contains b),
    dateIntervalContains
)

[<Test>]
let TestSimple () =
    use conn  = new MyDataConnection(dataOptions)

    let table = conn.CreateTempTable<Template>("my_entities", [
        { TemplateId = 1; ParentId = 1; ValidFrom = LocalDate(2026, 3, 15) }
        { TemplateId = 2; ParentId = 1;ValidFrom = LocalDate(2026, 3, 16) }
        { TemplateId = 3; ParentId = 1;ValidFrom = LocalDate(2026, 4, 17) }
    ])

    let dateInterval = DateInterval(LocalDate(2026, 3, 1), LocalDate(2026, 3, 31))

    let q2 =
        table
        |> _.Where(fun d -> Sql.Parameter(dateInterval).Contains(d.ValidFrom))
        |> _.Count()

    Assert.That(q2, Is.EqualTo(2))

[<Test>]
let TestWindow () =
    use conn  = new MyDataConnection(dataOptions)

    let table = conn.CreateTempTable<Template>("my_entities", [
        { TemplateId = 1; ParentId = 1; ValidFrom = LocalDate(2026, 3, 15) }
        { TemplateId = 2; ParentId = 1;ValidFrom = LocalDate(2026, 3, 16) }
        { TemplateId = 3; ParentId = 1;ValidFrom = LocalDate(2026, 4, 17) }
    ])

    let dateInterval = DateInterval(LocalDate(2026, 3, 1), LocalDate(2026, 3, 31))

    let cte = 
        table.Select(fun d -> {|
            ValidUntilExcludingEnd =
                Sql.Ext
                    .Lead<Nullable<LocalDate>>(d.ValidFrom)
                    .Over()
                    .PartitionBy(d.ParentId)
                    .OrderBy(d.ValidFrom)
                    .ToValue()

            Template = d
        |})
        |> _.AsCte("templates_with_valid_until")

    let count =
        cte
        |> _.Where(fun d -> Sql.Parameter(dateInterval).Contains(d.Template.ValidFrom))
        |> _.Count()

    Assert.That(count, Is.EqualTo(2))

