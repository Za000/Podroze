using Podroze.Pages;
using BCrypt.Net;
using Podroze.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Podroze.Controllers;

public interface IAuthenticationController
{
    bool AuthenticateUser(string username, string password);
    User? GetUserByUsername(string username);
    User? GetUserByEmail(string email);
    Task<bool> AutoLoginAuthentication();
    Task<bool> LogOut();
}

public class AuthenticationController : IAuthenticationController
{
    private readonly TravelDBContext _dbContext;
    private UserController user;

    public AuthenticationController(TravelDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public bool AuthenticateUser(string username, string password)
    {
        User? user = _dbContext.Users.SingleOrDefault(u => u.Username == username);
        if (user == null)
        {
            return false;
        }

        this.user = new UserController(user);
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

    public async Task<bool> AutoLoginAuthentication()
    {
        string usrname = await SecureStorage.GetAsync("username");
        string pass = await SecureStorage.GetAsync("password");

        if (usrname != null && pass != null)
        {
            bool isAuthenticated = AuthenticateUser(usrname, pass);

            if (isAuthenticated)
            {
                User user = GetUserByUsername(usrname);
                this.user = new UserController(user);
            }

            return isAuthenticated;
        }

        return false;
    }

    public async Task<bool> LogOut()
    {
        SecureStorage.Remove("username");
        SecureStorage.Remove("password");

        this.user = null;

        return true;
    }
}