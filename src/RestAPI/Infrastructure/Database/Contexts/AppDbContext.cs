using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Application.Auth;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Database;
public class AppDbContext : IdentityDbContext<User, Role, Guid>
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{ }

	// protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	// {
	// 	optionsBuilder.UseSqlite();
	// 	// base.OnConfiguring(optionsBuilder);
	// }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		// modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
		base.OnModelCreating(modelBuilder);
	}

	public DbSet<User> Users { get; set; }

	public DbSet<Ticket> Tickets { get; set; }

	public DbSet<TicketComment> TicketComments { get; set; }



}