using Filmora.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Filmora.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
{
    public DbSet<Movie> Movies => Set<Movie>();
    public DbSet<Genre> Genres => Set<Genre>();
    public DbSet<HoaDon> HoaDons => Set<HoaDon>();
    public DbSet<CTHoaDon> CTHoaDons => Set<CTHoaDon>();
    public DbSet<ShippingDetail> ShippingDetails => Set<ShippingDetail>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Genre>().HasIndex(genre => genre.Name).IsUnique();
        builder.Entity<Movie>().Property(movie => movie.Price).HasPrecision(18, 2);
        builder.Entity<Movie>().HasOne(movie => movie.Genre).WithMany()
            .HasForeignKey(movie => movie.GenreId).OnDelete(DeleteBehavior.Restrict);
        builder.Entity<CTHoaDon>().ToTable("ChiTietHoaDons");
        builder.Entity<CTHoaDon>().HasOne(line => line.MovieObj).WithMany()
            .HasForeignKey(line => line.MaMovie).OnDelete(DeleteBehavior.SetNull);
    }
}
