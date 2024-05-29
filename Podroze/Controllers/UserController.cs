using Podroze.Models;

namespace Podroze.Controllers;

public interface IUserController
{
    
}

public class UserController
{
    private User user;

    public UserController(User u)
    {
        user = u;
    }

    public User GetUser() => user;
}