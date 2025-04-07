using Microsoft.EntityFrameworkCore;
using MyProject.Core.Models;

namespace MyProject.DAL.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
}