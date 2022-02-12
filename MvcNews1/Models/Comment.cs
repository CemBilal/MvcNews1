using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MvcNews1.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool Enabled { get; set; }
        public string Author { get; set; }
        public Post Post { get; set; }
    }

    public class CommentEntityTypeConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            //throw new NotImplementedException();//IEntityTypeConfiguration interface ini kullanmam icin 
            //bunu eklemem gerekiyor

            builder
                .Property(x => x.Content)
                .IsRequired();
            //burada user paketinde var olup da ozelliklerini degistirmek istedigim
            //seylerin ozelliklerini belirliyorum 

            builder
                .Property(x => x.Author)
                .IsRequired();

        }


    }
}
