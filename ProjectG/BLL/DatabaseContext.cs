namespace BLL
{
  using System;
  using System.Data.Entity;
  using System.ComponentModel.DataAnnotations.Schema;
  using System.Linq;

  public partial class DatabaseContext : DbContext
  {
    public DatabaseContext()
        : base("name=DatabaseContext")
    {
    }

    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<Page> Pages { get; set; }
    public virtual DbSet<Role> Roles { get; set; }
    public virtual DbSet<Seo> Seos { get; set; }
    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Category>()
          .HasMany(e => e.Categories1)
          .WithRequired(e => e.Category1)
          .HasForeignKey(e => e.ParentId);

      modelBuilder.Entity<Category>()
          .HasMany(e => e.Pages)
          .WithRequired(e => e.Category)
          .WillCascadeOnDelete(false);

      modelBuilder.Entity<Role>()
          .HasMany(e => e.Users)
          .WithRequired(e => e.Role)
          .WillCascadeOnDelete(false);

      modelBuilder.Entity<Seo>()
          .HasMany(e => e.Pages)
          .WithRequired(e => e.Seo)
          .HasForeignKey(e => e.UrlId)
          .WillCascadeOnDelete(false);
    }
  }
}
