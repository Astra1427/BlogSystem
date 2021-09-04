using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace BlogSystem.Models
{
    public class BlogContext : DbContext
    {
        // Your context has been configured to use a 'BlogContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'BlogSystem.Models.BlogContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'BlogContext' 
        // connection string in the application configuration file.
        public BlogContext()
            : base("name=con")
        {
            Database.SetInitializer<BlogContext>(null);
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<BlogCategory> Categories { get; set; }
        public DbSet<ArticleToCategory> ArticleToCategories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Fans> Fans { get; set; }

    }

}