using LanguageExt;

namespace RealLifeExample;

public static class EitherExtensions
{
    public static EitherAsync<L, R> DoOnFailure<L, R>(this EitherAsync<L, R> either, Action<L> log)
        => either
            .Swap()
            .Do(log)
            .Swap();
}