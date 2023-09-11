using System;
using Microsoft.EntityFrameworkCore;

namespace BookStoreUser.Models
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}

		public DbSet<User> Users => Set<User>();
	}
}

