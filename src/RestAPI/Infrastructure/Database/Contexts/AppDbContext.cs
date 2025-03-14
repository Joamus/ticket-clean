using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure.Database;
public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{ }

	public DbSet<User> Users { get; set; }

	public DbSet<Ticket> Tickets { get; set; }

	public DbSet<TicketComment> TicketComments { get; set; }

}