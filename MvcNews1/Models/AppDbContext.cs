using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace MvcNews1.Models
{//data ztipleri ikiye ayrilir value type ve reference type 
    //value type:
    public class AppDbContext : IdentityDbContext<User, Role, Guid>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)//bu neden var?
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            //IEntityTypeCongiguration ile calisan her seyi bul ve sirayla calistir

            base.OnModelCreating(builder);  //database tablolari olustururken 
            //once bizim builder ile yaptigimiz degisiklikleri varsayip tablo hazirlasin diye
            //bu kodu yaziyoruz. bu sekilde bizim yaptigimiz degisiklikler entitycore ile gelen 
            //ozellikleri override ediyor 
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Post> Posts { get; set; }

    }
}
