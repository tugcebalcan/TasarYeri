using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TasarYeri.DAL.Entities;



namespace TasarYeri.DAL.Contexts
{
   public class MyContext : DbContext
    {

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{

        //    optionsBuilder.UseSqlServer("server=DESKTOP-HV3TUCM\\SQLEXPRESS; database=DenemeTY; integrated security=true;");
        //    base.OnConfiguring(optionsBuilder);
        //}

        //public MyContext(DbContextOptions<MyContext> options) : base(options)
        //{ }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=.; database=TAY; integrated security=true;");
            base.OnConfiguring(optionsBuilder);
        }
        public MyContext(DbContextOptions<MyContext> options) : base(options) { }
       
        public class AdressEntityConfiguration : IEntityTypeConfiguration<Address>
        {

            public void Configure(EntityTypeBuilder<Address> builder)
            {
                builder.HasOne(navigationExpression: m => m.Member)
                    .WithMany(navigationExpression: g => g.Addresses)
                    .HasForeignKey(s => s.MemberID)
                    .OnDelete(DeleteBehavior.SetNull)
                    .IsRequired();

                builder.HasOne(navigationExpression: t => t.Town)
                    .WithMany(navigationExpression: g => g.Addresses)
                    .HasForeignKey(s => s.TownID);
            }
        }
        public class CityEntityConfiguration : IEntityTypeConfiguration<City>
        {
            public void Configure(EntityTypeBuilder<City> builder)
            {
                builder.HasOne(navigationExpression: m => m.Country)
                    .WithMany(navigationExpression: s => s.Cities)
                    .HasForeignKey(s => s.CountryID)
                    .OnDelete(DeleteBehavior.SetNull)
                    .IsRequired();
            }
        }
        public class PictureEntityConfiguration : IEntityTypeConfiguration<Picture>
        {
            public void Configure(EntityTypeBuilder<Picture> builder)
            {
                builder.HasOne(navigationExpression: m => m.Product)
                    .WithMany(navigationExpression: c => c.Pictures)
                    .HasForeignKey(c => c.ProductID)
                    .OnDelete(DeleteBehavior.SetNull)
                    .IsRequired();
            }

        }
        public class CommentEntityConfiguration : IEntityTypeConfiguration<Comment>
        {
            public void Configure(EntityTypeBuilder<Comment> builder)
            {
                builder.HasOne(navigationExpression: m => m.Member)
                    .WithMany(navigationExpression: c => c.Comments)
                    .HasForeignKey(s => s.MemberID)
                    .OnDelete(DeleteBehavior.SetNull)
                    .IsRequired();

                builder.HasOne(navigationExpression: l => l.Product)
                   .WithMany(navigationExpression: c => c.Comments)
                   .HasForeignKey(s => s.ProductID)
                   .OnDelete(DeleteBehavior.SetNull);
            }
        }
        public class TownEntityConfiguration : IEntityTypeConfiguration<Town>
        {

            public void Configure(EntityTypeBuilder<Town> builder)
            {
                builder.HasOne(navigationExpression: m => m.City)
                    .WithMany(navigationExpression: g => g.Towns)
                    .HasForeignKey(s => s.CityID)
                    .OnDelete(DeleteBehavior.SetNull)
                    .IsRequired();
            }
        }
        public class OrderEntityConfiguration : IEntityTypeConfiguration<Order>
        {

            public void Configure(EntityTypeBuilder<Order> builder)
            {

                builder.HasOne(navigationExpression: l => l.Member)
                   .WithMany(navigationExpression: c => c.Orders)
                   .HasForeignKey(s => s.MemberID)
                   .OnDelete(DeleteBehavior.SetNull);

            }
        }

        public ICollection<ProductCategory> ProductCategories { get; set; }
        public ICollection<CategoryTag> CategoryTags { get; set; }
        public ICollection<FavoriteMember> FavoriteMembers { get; set; }
        public ICollection<ProductFavorite> ProductFavorites { get; set; }
        public ICollection<ProductOrder> ProductOrders { get; set; }
        public ICollection<OrderBasket> OrderBaskets { get; set; }
        public ICollection<ProductTag> ProductTags { get; set; }
        public ICollection<ProductBasket> ProductBaskets { get; set; }
        public ICollection<ProductMember> ProductMembers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasOne(o => o.ParentCategory).WithMany(m => m.SubCategories).HasForeignKey(h => h.ParentID).OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<ProductCategory>()
       .HasKey(bc => new { bc.ProductID, bc.CategoryID });

            modelBuilder.Entity<ProductTag>()
       .HasKey(bc => new { bc.ProductID, bc.TagID });

            modelBuilder.Entity<ProductOrder>()
       .HasKey(bc => new { bc.ProductID, bc.OrderID });

            modelBuilder.Entity<ProductBasket>()
       .HasKey(bc => new { bc.ProductID, bc.BasketID });

            modelBuilder.Entity<ProductFavorite>()
       .HasKey(bc => new { bc.ProductID, bc.FavoriteID });

            modelBuilder.Entity<ProductMember>()
       .HasKey(bc => new { bc.ProductID, bc.MemberID });

            modelBuilder.Entity<CategoryTag>()
       .HasKey(bc => new { bc.CategoryID, bc.TagID });
        }

        public DbSet<Member> Member { get; set; } 
        public DbSet<Admin> Admin { get; set; } 
        public DbSet<Address> Adresses { get; set; }
        public DbSet<Basket> Baskets { get; set; } 
        public DbSet<Category> Categories { get; set; } 
        public DbSet<City> Cities { get; set; } 
        public DbSet<Comment> Comments { get; set; } 
        public DbSet<Country> Countries { get; set; } 
        public DbSet<Favorite> Favorites { get; set; } 
        public DbSet<Order> Orders { get; set; } 
        public DbSet<Product> Products { get; set; } 
        public DbSet<Tag> Tags { get; set; } 
        public DbSet<Picture> Pictures { get; set; } 
        public DbSet<Image> Images { get; set; } 
        public DbSet<Town> Towns { get; set; } 
        public DbSet<Seller> Seller { get; set; } 
      

           

    }

}

