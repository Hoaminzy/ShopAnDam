using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ShopAnDam.Models.Framework
{
    public partial class AnDamDBContext : DbContext
    {
        public AnDamDBContext()
            : base("name=AnDamDBContext")
        {
        }

        public virtual DbSet<About> Abouts { get; set; }
        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<Good> Goods { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<PaymentStatus> PaymentStatuss { get; set; }
        public virtual DbSet<Order_Detail> Order_Detail { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Slide> Slides { get; set; }
        public virtual DbSet<Supply> Supplies { get; set; }
        public virtual DbSet<SystemConfig> SystemConfigs { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<Topic> Topics { get; set; }
        public virtual DbSet<UserGroup> UserGroups { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role_User> Role_User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<About>()
                .Property(e => e.MetaTitle)
                .IsUnicode(false);

            modelBuilder.Entity<About>()
                .Property(e => e.CreateBy)
                .IsUnicode(false);

            modelBuilder.Entity<Article>()
                .Property(e => e.MetaTitle)
                .IsUnicode(false);

            modelBuilder.Entity<Article>()
                .Property(e => e.CreateBy)
                .IsUnicode(false);

            modelBuilder.Entity<Brand>()
                .Property(e => e.CreateBy)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.MetaTilte)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.CreateBy)
                .IsUnicode(false);

            modelBuilder.Entity<Contact>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Contact>()
                .Property(e => e.SDT)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.CreateBy)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Orders)
                .WithOptional(e => e.Customer)
                .HasForeignKey(e => e.CustomersID);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Reviews)
                .WithOptional(e => e.Customer)
                .HasForeignKey(e => e.CustomersID);

            modelBuilder.Entity<Feedback>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Good>()
                .Property(e => e.Tongtien)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Good>()
                .Property(e => e.CreateBy)
                .IsUnicode(false);

            modelBuilder.Entity<Image>()
                .Property(e => e.CreateBy)
                .IsUnicode(false);

            modelBuilder.Entity<Menu>()
                .Property(e => e.Target)
                .IsUnicode(false);

            modelBuilder.Entity<Menu>()
                .Property(e => e.CreateBy)
                .IsUnicode(false);

            modelBuilder.Entity<Order_Detail>()
                .Property(e => e.Price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Order>()
                .Property(e => e.PhoneShip)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.MailShip)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.CreateBy)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.MetaTitle)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Product>()
                .Property(e => e.MotionPrice)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Product>()
                .Property(e => e.CreateBy)
                .IsUnicode(false);

            modelBuilder.Entity<Review>()
                .Property(e => e.CreateBy)
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
                .Property(e => e.CreateBy)
                .IsUnicode(false);

            modelBuilder.Entity<Slide>()
                .Property(e => e.Link)
                .IsUnicode(false);

            modelBuilder.Entity<Slide>()
                .Property(e => e.CreateBy)
                .IsUnicode(false);

            modelBuilder.Entity<Supply>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Supply>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Supply>()
                .Property(e => e.CreateBy)
                .IsUnicode(false);

            modelBuilder.Entity<SystemConfig>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<SystemConfig>()
                .Property(e => e.Type)
                .IsUnicode(false);

            modelBuilder.Entity<Tag>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<Topic>()
                .Property(e => e.MetaTilte)
                .IsUnicode(false);

            modelBuilder.Entity<Topic>()
                .Property(e => e.CreateBy)
                .IsUnicode(false);

            modelBuilder.Entity<UserGroup>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<UserGroup>()
                .Property(e => e.CreateBy)
                .IsUnicode(false);

            modelBuilder.Entity<UserGroup>()
                .HasMany(e => e.Users)
                .WithOptional(e => e.UserGroup)
                .HasForeignKey(e => e.GroupID);

            modelBuilder.Entity<User>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.PassWord)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.GroupID)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.CreateBy)
                .IsUnicode(false);

            modelBuilder.Entity<Role_User>()
                .Property(e => e.GroupID)
                .IsUnicode(false);

            modelBuilder.Entity<Role_User>()
                .Property(e => e.RoleID)
                .IsUnicode(false);
        }
    }
}
