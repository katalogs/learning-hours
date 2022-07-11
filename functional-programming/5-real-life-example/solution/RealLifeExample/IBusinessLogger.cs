using LanguageExt;

namespace RealLifeExample
{
    public interface IBusinessLogger
    {
        TryAsync<Unit> LogSuccessAsync(Guid id);
        TryAsync<Unit> LogFailureAsync(Guid id, Exception exception);
        TryAsync<Unit> LogAsync(string message);
    }
}