namespace TestDoubles.Stub
{
    public class AllowAccessAuthorizer : IAuthorizer
    {
        public bool Authorize() => true;
    }
}