namespace News.Repository
{
    public interface IUserRepository
    {
        void Add(string name);
        int GetCurrentUserId();
        bool IsLoggedIn();
    }
}