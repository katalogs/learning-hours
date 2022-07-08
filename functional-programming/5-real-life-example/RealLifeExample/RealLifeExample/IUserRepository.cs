namespace RealLifeExample
{
    public interface IUserRepository
    {
        User FindById(Guid id);
    }
}