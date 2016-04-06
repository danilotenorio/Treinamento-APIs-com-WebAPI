using DevStore.Domain;
using DevStore.Infra.Mappings;
using System;
using System.Data.Entity;

namespace DevStore.Infra.DataContexts
{
    public class DevStoreDataContext : DbContext
    {
        public DevStoreDataContext()
            : base("DevStoreConnectionString")
            {
                Database.SetInitializer<DevStoreDataContext>(new DevStoreDataContextInitializer());
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            }
        public IDbSet<Product> Products { get; set; }
        public IDbSet<Category> Categories { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new CategoryMap());
            base.OnModelCreating(modelBuilder);
        }
    }

    public class DevStoreDataContextInitializer : DropCreateDatabaseAlways<DevStoreDataContext>
    {
        protected override void Seed(DevStoreDataContext context)
        {
            var categoryInformatica = new Category { Id = 1, Title = "Informatica" };
            var categoryGames = new Category { Id = 2, Title = "Games" };
            var categoryPapelaria = new Category { Id = 3, Title = "Papelaria" };

            context.Categories.Add(categoryInformatica);
            context.Categories.Add(categoryGames);
            context.Categories.Add(categoryPapelaria);            

            context.Products.Add(new Product { Id = 1, CategoryId = 1, IsActive = true, Price = "1000", AcquireDate = DateTime.Now, Title = "Computador de Mesa"});
            context.Products.Add(new Product { Id = 2, CategoryId = 1, IsActive = true, Price = "2000", AcquireDate = DateTime.Now, Title = "NootBook" });
            context.Products.Add(new Product { Id = 3, CategoryId = 1, IsActive = true, Price = "500", AcquireDate = DateTime.Now, Title = "Monitor LCD" });

            context.Products.Add(new Product { Id = 4, CategoryId = 2, IsActive = true, Price = "120", AcquireDate = DateTime.Now, Title = "God of War 4" });
            context.Products.Add(new Product { Id = 5, CategoryId = 2, IsActive = true, Price = "100", AcquireDate = DateTime.Now, Title = "Resident Evil 3 Nemesis" });

            context.Products.Add(new Product { Id = 6, CategoryId = 3, IsActive = true, Price = "12", AcquireDate = DateTime.Now, Title = "Caderno 10 Matérias" });
            context.Products.Add(new Product { Id = 7, CategoryId = 3, IsActive = true, Price = "8", AcquireDate = DateTime.Now, Title = "Lapiseira Faber Castel" });

            context.SaveChanges();
            base.Seed(context);
        }
    }
}
