namespace Specification
{
    [Serializable]
    public class InvalidSpecificationException : Exception
    {
        public InvalidSpecificationException(string message)
            : base(message)
        {
        }
    }
}