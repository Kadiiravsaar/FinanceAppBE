using Finance.API.Migrations;
using Finance.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Finance.API.Data
{
	public class AppDbContext : IdentityDbContext<AppUser>
	{
		public AppDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
		{

		}
		public DbSet<Comment> Comments { get; set; }
		public DbSet<Stock> Stocks { get; set; }
	}
}
