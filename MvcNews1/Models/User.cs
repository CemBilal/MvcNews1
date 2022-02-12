using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MvcNews1.Models
{
    public class User : IdentityUser<Guid>
    {
        public string Name { get; set; }
        public string Photo { get; set; }
        public ICollection<Post> Posts { get; set; } = new HashSet<Post>();// 1 user in 1 den fazla post u olabilir

    }

    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //throw new NotImplementedException();//IEntityTypeConfiguration interface ini kullanmam icin 
            //bunu eklemem gerekiyor

            builder
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);//nvarchar 50
            //burada user paketinde var olup da ozelliklerini degistirmek istedigim
            //seylerin ozelliklerini belirliyorum 
            builder
                .Property(x => x.Photo)
                .IsUnicode(false)
                .IsRequired(false);
            //nvarchar max
            //fotografin icinde tr karakter olmayacagi icin boyle yaptik
            //daha az yer kaplar

        }
    }
}
