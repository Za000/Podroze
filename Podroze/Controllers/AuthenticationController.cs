using Podroze.Pages;
using BCrypt.Net;
using Podroze.Models;

namespace Podroze.Controllers;

public interface IAuthenticationController
{
    bool AuthenticateUser(string username, string password);
    User? GetUserByUsername(string username);
    User? GetUserByEmail(string email);
}

public class AuthenticationController : IAuthenticationController
{
    private readonly TravelDBContext _dbContext;

    public AuthenticationController(TravelDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public bool AuthenticateUser(string username, string password)
    {
        var user = _dbContext.Users.SingleOrDefault(u => u.Username == username);
        if (user == null)
        {
            return false;
        }

        return BCrypt.Net.BCrypt.Verify(password, user.Password);
    }

    public User? GetUserByUsername(string username)
    {
        var user = _dbContext.Users.SingleOrDefault(u => u.Username == username);

        return user;
    }

    public User? GetUserByEmail(string email)
    {
        var user = _dbContext.Users.SingleOrDefault(u => u.Email == email);

        return user;
    }
}