using System;
using LanguageExt;

namespace Mutation.Tests
{
    public static class LanguageExtExtensions
    {
        public static TLeft LeftUnsafe<TLeft, TRight>(this Either<TLeft, TRight> either)
            => either.LeftToSeq().Single();

        public static TRight RightUnsafe<TLeft, TRight>(this Either<TLeft, TRight> either)
            => either.RightToSeq().Single();

        public static TResult SuccessUnsafe<TResult>(this Try<TResult> @try)
            => @try.Match(r => r, exception => throw exception);

        public static Exception FailureUnsafe<TResult>(this Try<TResult> @try)
            => @try.Match(r => throw new ArgumentException("Try is not in failure state"),
                exception => exception);
    }
}