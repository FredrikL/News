namespace News.Repository
{
    public class UserRepository : IUserRepository
    {
        public void Add(string name)
        {
            throw new System.NotImplementedException();
        }

        public int GetCurrentUserId()
        {
            return 1;
        }

        public bool IsLoggedIn()
        {
            return GetCurrentUserId() > 0;
        }
    }
}