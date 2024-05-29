using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Podroze.Models;

public class User
{
    public int Id { get; set; }
    public string FirtsName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}