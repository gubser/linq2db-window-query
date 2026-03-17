
With SDK 10.0.103 the unit test passes:

```sh
docker compose run --build --rm test-103
```

Output:

```sh
Restore complete (0.5s)
  LinqToDbQueryTests net10.0 succeeded (2.5s) → bin/Debug/net10.0/LinqToDbQueryTests.dll
NUnit Adapter 5.0.0.0: Test execution started
Running all tests in /src/bin/Debug/net10.0/LinqToDbQueryTests.dll
   NUnit3TestExecutor discovered 2 of 2 NUnit test cases using Current Discovery mode, Non-Explicit run
NUnit Adapter 5.0.0.0: Test execution complete
  LinqToDbQueryTests test net10.0 succeeded (1.9s)

Test summary: total: 2, failed: 0, succeeded: 2, skipped: 0, duration: 1.9s
Build succeeded in 5.3s
```


With SDK 10.0.201 the unit test fails:

```sh
docker compose run --build --rm test-201
```

Output:

```sh
Restore complete (0.5s)
  LinqToDbQueryTests net10.0 succeeded (2.4s) → bin/Debug/net10.0/LinqToDbQueryTests.dll
NUnit Adapter 5.0.0.0: Test execution started
Running all tests in /src/bin/Debug/net10.0/LinqToDbQueryTests.dll
   NUnit3TestExecutor discovered 2 of 2 NUnit test cases using Current Discovery mode, Non-Explicit run
NUnit Adapter 5.0.0.0: Test execution complete
  LinqToDbQueryTests test net10.0 failed with 1 error(s) (1.8s)
    /_/Source/LinqToDB/Internal/Linq/Builder/ExpressionBuilder.cs(376): error TESTERROR: 
      TestWindow (88ms): Error Message: LinqToDB.LinqToDBException : The LINQ expression could not be converted to SQL.
      Expression:
      LinqToDbQuery10_0_201.dateIntervalContains(
      	d: Sql.Parameter<DateInterval>(DateInterval { Sunday, 01 March 2026, Monday, 02 March 2026, Tuesday, 03 March 2026, Wednesday, 04 March 2026, Thursday, 05 March 2026, Friday, 06
       March 2026, Saturday, 07 March 2026, Sunday, 08 March 2026, Monday, 09 March 2026, Tuesday, 10 March 2026, Wednesday, 11 March 2026, Thursday, 12 March 2026, Friday, 13 March 20
      26, Saturday, 14 March 2026, Sunday, 15 March 2026, Monday, 16 March 2026, Tuesday, 17 March 2026, Wednesday, 18 March 2026, Thursday, 19 March 2026, Friday, 20 March 2026, Satur
      day, 21 March 2026, Sunday, 22 March 2026, Monday, 23 March 2026, Tuesday, 24 March 2026, Wednesday, 25 March 2026, Thursday, 26 March 2026, Friday, 27 March 2026, Saturday, 28 M
      arch 2026, Sunday, 29 March 2026, Monday, 30 March 2026, Tuesday, 31 March 2026 }), 
      	e: d.Template.ValidFrom)
      Stack Trace:
         at LinqToDB.Internal.Linq.Builder.ExpressionBuilder.BuildSequence(BuildInfo buildInfo) in /_/Source/LinqToDB/Internal/Linq/Builder/ExpressionBuilder.cs:line 376
         at LinqToDB.Internal.Linq.Builder.ExpressionBuilder.Build[T](IQueryExpressions& expressions) in /_/Source/LinqToDB/Internal/Linq/Builder/ExpressionBuilder.cs:line 128
         at LinqToDB.Internal.Linq.Query`1.CreateQuery(ExpressionTreeOptimizationContext optimizationContext, ParametersContext parametersContext, IDataContext dataContext, IQueryExpre
      ssions& expressions) in /_/Source/LinqToDB/Internal/Linq/Query{T}.cs:line 403
         at LinqToDB.Internal.Linq.Query`1.GetQuery(IDataContext dataContext, IQueryExpressions& expressions, Boolean& dependsOnParameters) in /_/Source/LinqToDB/Internal/Linq/Query{T}
      .cs:line 380
         at LinqToDB.Internal.Linq.ExpressionQuery`1.GetQuery(IQueryExpressions& expression, Boolean cache, Boolean& dependsOnParameters) in /_/Source/LinqToDB/Internal/Linq/Expression
      Query.cs:line 91
         at LinqToDB.Internal.Linq.ExpressionQuery`1.System.Linq.IQueryProvider.Execute[TResult](Expression expression) in /_/Source/LinqToDB/Internal/Linq/ExpressionQuery.cs:line 312
         at LinqToDbQuery10_0_201.TestWindow() in /src/UnitTest1.fs:line 106
         at System.Reflection.MethodBaseInvoker.InterpretedInvoke_Method(Object obj, IntPtr* args)
         at System.Reflection.MethodBaseInvoker.InvokeWithNoArgs(Object obj, BindingFlags invokeAttr)

Test summary: total: 2, failed: 1, succeeded: 1, skipped: 0, duration: 1.8s
Build failed with 1 error(s) in 5.2s
```