using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToyotaProject.Models.MyModels;

namespace ToyotaProject.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public DbSet<ToyotaModel> ToyotaModels { get; set; }
    public DbSet<ComplectationModel> ComplectationModels { get; set; }
    public DbSet<ComplectationColorModel> ComplectationColorModels { get; set; }
    public DbSet<ColorModel> ColorModels { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}