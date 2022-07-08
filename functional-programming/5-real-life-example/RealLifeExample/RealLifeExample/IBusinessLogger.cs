namespace RealLifeExample
{
    public interface IBusinessLogger
    {
        void LogSuccessRegister(Guid id);
        void LogFailureRegister(Guid id, Exception exception);
    }
}
