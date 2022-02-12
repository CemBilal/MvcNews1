using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcNews1.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string HeadLine { get; set; }//nvarchar(max)=national varying character
        public DateTime Date { get; set; }
        public string Content { get; set; }
        public bool IsArticle { get; set; }
        public string Photo { get; set; }

        [NotMapped]
        public IFormFile photoFile { get; set; }
        public int Hit { get; set; }
        public bool Featured { get; set; }
        public Category Category { get; set; }
        public ICollection<Comment> Comment { get; set; } = new HashSet<Comment>();
        public User User { get; set; }
        public Guid UserId { get; set; }
        public bool Enabled { get; set; } = true;//silinen x kullanicinin sayfasinda gorunmez
        //ama database de enabled i false olarak kalir
    }

    public class PostEntityTypeConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            //throw new NotImplementedException();//IEntityTypeConfiguration interface ini kullanmam icin 
            //bunu eklemem gerekiyor

            builder
                .Property(x => x.HeadLine)
                .IsRequired()
                .HasMaxLength(50);//nvarchar 50
            //burada user paketinde var olup da ozelliklerini degistirmek istedigim
            //seylerin ozelliklerini belirliyorum 
            builder
                .Property(x => x.Content)
                .IsUnicode(false);//nvarchar max
                                  //fotografin icinde tr karakter olmayacagi icin boyle yaptik
                                  //daha az yer kaplar

            builder
                .Property(x => x.Photo)
                .IsUnicode(false);

            builder
                .HasMany(x => x.Comment)//bir haberin birden fazla yorumu var 
                .WithOne(x => x.Post)//bir yorum sadece bir habere yabilabilir
                .OnDelete(DeleteBehavior.Cascade);//post ile birlikte commentlerde silinir
                                                  //asagi basamaklardaki kayitlari da siler
        
                  //replacement constraint
                  //fk constraint
                  //yukaridaki iki kavramin toplamina database integrity denir
                  //kullanim ama saglam bir database olusturmaktir
        }


    }
}
