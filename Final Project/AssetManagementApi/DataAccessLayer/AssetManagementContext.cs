using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace DataAccessLayer
{
	public partial class AssetManagmentContext : DbContext
	{
		public AssetManagmentContext(DbContextOptions<AssetManagmentContext> options) : base(options)
		{
		}

		public DbSet<AssetCategory> AssetCategories { get; set; }
		public DbSet<UserDetail> UserDetails { get; set; }

		public DbSet<Asset> Assets { get; set; }

		public DbSet<AssetAssignment> AssetAssignments { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

			OnModelCreatingPartial(modelBuilder);
		}
		partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


	}
}
