using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace MvcNews1.Models
{
    

        public class Category
        {
             public int Id { get; set; }


            [Display(Name="Kategori Adi")]
            [Required(ErrorMessage = "{0} Alani Bos Birakilamaz!")]
            public string Name { get; set; }
            public ICollection<Post> Posts { get; set; } = new HashSet<Post>();

            [Display(Name = "Etkin")]
            public bool Enabled { get; set; } = true;//silinen x kullanicinin sayfasinda gorunmez
                                                 //ama database de enabled i false olarak kalir

        }

    public class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
    {
            public void Configure(EntityTypeBuilder<Category> builder)
            {
            //throw new NotImplementedException();//IEntityTypeConfiguration interface ini kullanmam icin 
            builder
               .HasIndex(x => x.Name)
               .IsUnique();//bunu eklemem gerekiyor

                builder
                    .Property(x => x.Name)
                    .IsRequired()
                    .HasMaxLength(50);//nvarchar 50
                                      //burada user paketinde var olup da ozelliklerini degistirmek istedigim
                                      //seylerin ozelliklerini belirliyorum 

                builder
                    .HasMany(x => x.Posts)//1 category nin birden fazla postu var
                    .WithOne(x => x.Category)//1 postun 1 tane kategorisi var
                    .OnDelete(DeleteBehavior.Restrict);//bu sekilde categorilerin silinmesi
                                                       //yasaklanmis oluyor

                //category kisminda olusturulacak olan yukarida belirtilmis listenin 
                //detaylarini yaziyoruz

                //replication constraint + fake key constraint= database integrity
                //database in veri butunlugunun saglamligini sagliyor
                //database ten veri silmek yanlistir

            }
    }
    
}
