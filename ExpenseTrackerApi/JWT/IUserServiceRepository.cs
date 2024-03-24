using ExpenseTrackerApi.Model;

namespace ExpenseTrackerApi.JWT
{
    public interface IUserServiceRepository
    {
        Task<bool> IsValidUserAsync(UserLogin users);

        Task<UserRefreshTokens> AddUserRefreshTokens(UserRefreshTokens user);

        UserRefreshTokens GetSavedRefreshTokens(string username, string refreshtoken);

        Task<UserRefreshTokens> DeleteUserRefreshTokens(string username, string refreshToken);
    }
}
