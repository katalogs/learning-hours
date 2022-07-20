# Fuzzing test data builders

This repository uses [Bogus](https://github.com/bchavez/Bogus) as fuzzing library.

The class [FuzzingTestCases](./BookInvoicing.Tests/FuzzingTestCases.cs) is a base class for test classes with:

- faker class initialization with random seed for each test
- random seed logging at the end of each test execution

## Demo

- Make the [ReportGeneratorTests class](./BookInvoicing.Tests/ReportGeneratorTests.cs) extend FuzzingTestCases
- Make a presentation of the [FuzzingTestCases](./BookInvoicing.Tests/FuzzingTestCases.cs) class
- In the the [ReportGeneratorTests class](./BookInvoicing.Tests/ReportGeneratorTests.cs), replace the existing
  EducationalBookBuilder usages with extension methods for the Faker class

Eg:

```cs
// Before
var book = BookBuilder.AnEducationalBook().Costing(25).Build();

// After
var book = Fuzzer.AnEducationalBook().Costing(25).Build();
```

## Hint

Some test may fail over executions as values are set implicit in existing builders...