using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DAL
{
    public partial class BookContext : DbContext
    {
        public BookContext()
            : base("name=BookContext")
        {
        }

        public virtual DbSet<Author> Author { get; set; }
        public virtual DbSet<AuthorsSpecification> AuthorsSpecification { get; set; }
        public virtual DbSet<Book> Book { get; set; }
        public virtual DbSet<BookPublishingHouse> BookPublishingHouse { get; set; }
        public virtual DbSet<Boook> Boook { get; set; }
        public virtual DbSet<BoookPublishingHousee> BoookPublishingHousee { get; set; }
        public virtual DbSet<proba> proba { get; set; }
        public virtual DbSet<PublishingHouse> PublishingHouse { get; set; }
        public virtual DbSet<PublishingHousee> PublishingHousee { get; set; }
        public virtual DbSet<State> State { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Author>()
                .Property(e => e.Surname)
                .IsUnicode(false);

            modelBuilder.Entity<Author>()
                .HasMany(e => e.AuthorsSpecification)
                .WithRequired(e => e.Author)
                .HasForeignKey(e => e.Author_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Author>()
                .HasMany(e => e.Book)
                .WithRequired(e => e.Author)
                .HasForeignKey(e => e.Author_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Author>()
                .HasMany(e => e.Boook)
                .WithRequired(e => e.Author)
                .HasForeignKey(e => e.Author_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AuthorsSpecification>()
                .Property(e => e.First_Book)
                .IsUnicode(false);

            modelBuilder.Entity<AuthorsSpecification>()
                .Property(e => e.Prizes)
                .IsUnicode(false);

            modelBuilder.Entity<Book>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Book>()
                .Property(e => e.Genre)
                .IsUnicode(false);

            modelBuilder.Entity<Book>()
                .HasMany(e => e.BookPublishingHouse)
                .WithRequired(e => e.Book)
                .HasForeignKey(e => e.Book_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Boook>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Boook>()
                .Property(e => e.Genre)
                .IsUnicode(false);

            modelBuilder.Entity<Boook>()
                .HasMany(e => e.BoookPublishingHousee)
                .WithRequired(e => e.Boook)
                .HasForeignKey(e => e.Boook_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<proba>()
                .Property(e => e.Ime)
                .IsUnicode(false);

            modelBuilder.Entity<proba>()
                .Property(e => e.PRICE)
                .HasPrecision(19, 4);

            modelBuilder.Entity<PublishingHouse>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<PublishingHouse>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<PublishingHouse>()
                .Property(e => e.Director)
                .IsUnicode(false);

            modelBuilder.Entity<PublishingHouse>()
                .HasMany(e => e.BookPublishingHouse)
                .WithRequired(e => e.PublishingHouse)
                .HasForeignKey(e => e.PublishingHouse_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PublishingHousee>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<PublishingHousee>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<PublishingHousee>()
                .Property(e => e.Director)
                .IsUnicode(false);

            modelBuilder.Entity<PublishingHousee>()
                .HasMany(e => e.BoookPublishingHousee)
                .WithRequired(e => e.PublishingHousee)
                .HasForeignKey(e => e.PublishingHousee_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<State>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<State>()
                .Property(e => e.Continent)
                .IsUnicode(false);

            modelBuilder.Entity<State>()
                .Property(e => e.Size)
                .HasPrecision(18, 0);

            modelBuilder.Entity<State>()
                .HasMany(e => e.Author)
                .WithRequired(e => e.State)
                .HasForeignKey(e => e.State_Id)
                .WillCascadeOnDelete(false);
        }
    }
}
