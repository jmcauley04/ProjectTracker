namespace ProjectTracker.Services
{
    public class UserService
    {
        public async Task<string> GetUsername()
        {
            return await Task.FromResult("Michael Cauley");
        }
    }
}
