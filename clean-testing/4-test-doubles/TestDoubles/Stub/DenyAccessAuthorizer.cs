namespace TestDoubles.Stub
{
    public class DenyAccessAuthorizer : IAuthorizer
    {
        public bool Authorize() => false;
    }
}