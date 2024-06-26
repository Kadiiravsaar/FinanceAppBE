﻿using Finance.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Repository.Repositories
{
	public class AppDbContext : IdentityDbContext<AppUser>
	{
		public AppDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
		{

		}
		public DbSet<Comment> Comments { get; set; }
		public DbSet<Stock> Stocks { get; set; }
		public DbSet<Portfolio> Portfolios { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<Stock>().HasQueryFilter(s => !s.IsDeleted);
			builder.Entity<Comment>().HasQueryFilter(c=> !c.IsDeleted);

			builder.Entity<Portfolio>(x => x.HasKey(p => new { p.AppUserId, p.StockId }));

			builder.Entity<Portfolio>()
				.HasOne(u => u.AppUser)
				.WithMany(u => u.Portfolios)
				.HasForeignKey(p => p.AppUserId);

			builder.Entity<Portfolio>()
				.HasOne(u => u.Stock)
				.WithMany(u => u.Portfolios)
				.HasForeignKey(p => p.StockId);

			List<IdentityRole> roles = new List<IdentityRole>
			{
				new IdentityRole
				{
					Name = "Admin",
					NormalizedName = "ADMIN"
				},
				new IdentityRole
				{
					Name = "User",
					NormalizedName = "USER"
				},
			};
			builder.Entity<IdentityRole>().HasData(roles);
		}
	}
}
