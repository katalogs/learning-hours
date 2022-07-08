namespace RealLifeExample
{
    public interface IUserRepository
    {
        Task<User> FindByIdAsync(Guid id);
    }
}