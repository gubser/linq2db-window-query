Using this workaround added to LinqToDbQueryTests.fsproj:

```xml
<PackageReference Update="FSharp.Core" Version="10.0.105" />
```

With SDK 10.0.103 the unit test passes:

```sh
docker compose run --build --rm test-103
```

Output:

```sh
Restore complete (0.5s)
  LinqToDbQueryTests net10.0 succeeded (2.9s) → bin/Debug/net10.0/LinqToDbQueryTests.dll
NUnit Adapter 5.0.0.0: Test execution started
Running all tests in /src/bin/Debug/net10.0/LinqToDbQueryTests.dll
   NUnit3TestExecutor discovered 2 of 2 NUnit test cases using Current Discovery mode, Non-Explicit run
NUnit Adapter 5.0.0.0: Test execution complete
  LinqToDbQueryTests test net10.0 succeeded (1.8s)

Test summary: total: 2, failed: 0, succeeded: 2, skipped: 0, duration: 1.8s
Build succeeded in 5.7s
```


With SDK 10.0.201 the unit test fails:

```sh
docker compose run --build --rm test-201
```

Output:

```sh
Restore complete (0.5s)
  LinqToDbQueryTests net10.0 succeeded (2.2s) → bin/Debug/net10.0/LinqToDbQueryTests.dll
NUnit Adapter 5.0.0.0: Test execution started
Running all tests in /src/bin/Debug/net10.0/LinqToDbQueryTests.dll
   NUnit3TestExecutor discovered 2 of 2 NUnit test cases using Current Discovery mode, Non-Explicit run
NUnit Adapter 5.0.0.0: Test execution complete
  LinqToDbQueryTests test net10.0 succeeded (1.8s)

Test summary: total: 2, failed: 0, succeeded: 2, skipped: 0, duration: 1.7s
Build succeeded in 4.9s
```

With SDK 10.0.202 the unit test passes:


```sh
docker compose run --build --rm test-202
```

Output:

```sh
Restore complete (0.5s)
  LinqToDbQueryTests net10.0 succeeded (2.3s) → bin/Debug/net10.0/LinqToDbQueryTests.dll
NUnit Adapter 5.0.0.0: Test execution started
Running all tests in /src/bin/Debug/net10.0/LinqToDbQueryTests.dll
   NUnit3TestExecutor discovered 2 of 2 NUnit test cases using Current Discovery mode, Non-Explicit run
NUnit Adapter 5.0.0.0: Test execution complete
  LinqToDbQueryTests test net10.0 succeeded (1.8s)

Test summary: total: 2, failed: 0, succeeded: 2, skipped: 0, duration: 1.8s
Build succeeded in 5.1s
```