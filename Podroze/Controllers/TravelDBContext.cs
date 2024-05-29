using Microsoft.EntityFrameworkCore;
using System;
using Podroze.Models;

namespace Podroze.Controllers;

public class TravelDBContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public TravelDBContext(DbContextOptions<TravelDBContext> options) : base(options)
    {
    }

}