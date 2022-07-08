namespace RealLifeExample
{
    public class BusinessLogger : IBusinessLogger
    {
        public void LogFailureRegister(Guid id, Exception exception)
        {
            Console.WriteLine("We failed to register the user : " + id);
            Console.WriteLine("Here is why : " + exception.Message);
            Console.WriteLine("Stack trace : " + exception.StackTrace);
        }

        public void LogSuccessRegister(Guid id)
            => Console.WriteLine("We successfully registered the user : " + id);
    }
}
