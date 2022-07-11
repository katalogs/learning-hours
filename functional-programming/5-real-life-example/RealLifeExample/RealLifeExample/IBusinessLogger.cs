namespace RealLifeExample
{
    public interface IBusinessLogger
    {
        Task LogSuccessAsync(Guid id);
        Task LogFailureAsync(Guid id, Exception exception);
        Task LogAsync(string message);
    }
}