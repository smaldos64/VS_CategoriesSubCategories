namespace CategoriesSubCategories.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;

    public class Test : DbContext
    {
        // Your context has been configured to use a 'Test' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'CategoriesSubCategories.Models.Test' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Test' 
        // connection string in the application configuration file.
        public Test()
            : base("name=Test")
        {
        }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    //modelBuilder.Entity<Category>().HasOptional(x => x.ParentCategory).WithMany(x => x.ChildCategories).HasForeignKey(x => x.ParentCategoryID).WillCascadeOnDelete(true);
        //    //modelBuilder.Entity<Category>().HasRequired(x => x.ParentCategory).WithMany(x => x.ChildCategories).HasForeignKey(x => x.ParentCategoryID).WillCascadeOnDelete(true);

        //    //modelBuilder.Conventions.Add<OneToManyCascadeDeleteConvention>();
        //    //modelBuilder.Conventions.Add<ManyToManyCascadeDeleteConvention>();
        //}

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<Product> Products { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}