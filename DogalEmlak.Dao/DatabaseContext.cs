using DogalEmlak.Entities;
using Microsoft.EntityFrameworkCore;
using NETCore.Encrypt.Extensions;


namespace DogalEmlak.Dao
{
	public class DatabaseContext : DbContext
	{
		public DbSet<Entities.Property> Properties { get; set; }

		public DbSet<PropertyImage> PropertyImages { get; set; }

		public DbSet<Role> Roles { get; set; }

		public DbSet<User> Users { get; set; }

		//sql e bağlanması için
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB; Database=DogalEmlakFinal201817014; integrated security=True; MultipleActiveResultSets=True;");
			base.OnConfiguring(optionsBuilder);
		}

		//fluent api (direk db de bu veriler yüklü olsun)
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			Guid adminGuidId = Guid.NewGuid();

			modelBuilder.Entity<Role>().HasData(new Role
			{
				Id = adminGuidId,
				Authority = "Admin"
			});
			modelBuilder.Entity<Role>().HasData(new Role
			{
				Id = Guid.NewGuid(),
				Authority = "Staff"
			});
			modelBuilder.Entity<Role>().HasData(new Role
			{
				Id = Guid.NewGuid(),
				Authority = "Member"
			});

			modelBuilder.Entity<User>().HasData(new User
			{
				Id = Guid.NewGuid(),
				FirstName = "Dogal",
				LastName = "Emlak",
				UserName = "DogalEmlak",
				Password = "DogalEmlak".MD5(),
				Email = "dogalemlak@gmail.com",
				Phone = "+905380426061",
				Locked = false,
				DateOfAdded = DateTime.Now,
				//Role = new Role { Id = adminGuidId },
				RoleId = adminGuidId
			});


			base.OnModelCreating(modelBuilder);
		}
	}
}
