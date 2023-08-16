using System;
using Bogus;
using BookInvoicing.Tests.Builders;
using Xunit.Abstractions;

namespace BookInvoicing.Tests
{
    public class FuzzingTestCases: IDisposable
    {
        protected readonly Faker Fuzzer;
        private readonly ITestOutputHelper _output;
        protected int _seed;

        public FuzzingTestCases(ITestOutputHelper outputHelper, int? seed = null)
        {
            _seed = seed ?? new Random().Next();
            Fuzzer = new Faker();
            Fuzzer.Random = new Randomizer(_seed);
            _output = outputHelper;
        }

        public virtual void Dispose()
        {
            _output.WriteLine($"--- Fuzzer instantiated with the seed {_seed}");
            _output.WriteLine("--- Note: you can instantiate another Fuzzer with that very same seed in order to reproduce the exact test conditions");
        }
    }
}
