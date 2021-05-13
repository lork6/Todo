using backend;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace backend.test
{
    /// <summary>
    /// Provies a WebApi web server for testing.
    /// </summary>
    public class TestRestHelper : WebApplicationFactory<Startup>
    {
        private readonly SqliteConnection sqliteConnection;

        private TestRestHelper()
        {
            this.sqliteConnection = new SqliteConnection(@"DataSource=:memory:");
            this.sqliteConnection.CreateCollation("BINARY", (x, y) => string.Compare(x, y, ignoreCase: false));
            this.sqliteConnection.Open();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Replace DB configuration
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<Models.TodoContext>));
                if (descriptor != null)
                    services.Remove(descriptor);
                services.AddDbContext<Models.TodoContext>(options => options.UseSqlite(sqliteConnection));

                // Build the service provider.
                var sp = services.BuildServiceProvider();

                // Ensure db is created (required for creating tables)
                using (var scope = sp.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<Models.TodoContext>();
                    db.Database.EnsureCreated();
                }
            });
        }

        public static TestRestHelper Create()
            => new TestRestHelper();

        public void AddSeedEntities<T>(T[] entities)
        {
            using (var serviceScope = this.Services.CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetRequiredService<Models.TodoContext>();
                foreach (var e in entities)
                    db.Add(e);
                db.SaveChanges();
            }
        }

        public IReadOnlyCollection<T> GetDbTableContent<T>()
            where T : class
        {
            using (var serviceScope = this.Services.CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetRequiredService<Models.TodoContext>();
                return db.Set<T>().ToList();
            }
        }
    }

}
