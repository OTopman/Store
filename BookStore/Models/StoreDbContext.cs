using System;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Models
{
	public class StoreDbContext : DbContext
	{
		public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
		{
		}

		public DbSet<Book> Books => Set<Book>();
		public DbSet<Order> Orders { get; set; } = null!;
	}
}

