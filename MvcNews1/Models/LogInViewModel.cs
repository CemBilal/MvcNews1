using System.ComponentModel.DataAnnotations;

namespace MvcNews1.Models

{//login yaparken kullanicidan veri isteyecegimiz icin class olusturuyoruz
    public class LogInViewModel
    {
        

        //[Display(UserName="Kullanici adi")] eger sayfada gorunnen yaziyi html de degistirmediyse 
        //buradan da degistirebilirsin

        [Display(Name = "Kullanıcı Adı")]
        [Required(ErrorMessage ="{0} Alani Bos Birakilamaz!")]//bu sayede bos birakilan alanla oldugu surece uyari verecegiz
        [DataType(DataType.EmailAddress)]//telefonda email yazmaya uygun klavye cikartir
        public string UserName { get; set; }

        [Display(Name = "Parola")]
        [Required(ErrorMessage = "{0} Alani Bos Birakilamaz!")]
        [DataType(DataType.Password)]//bu sekilde kullanicinin yazdigi parola yazarken gorulmez
        //ve telefonda password e uygun klavye cikar
        public string Password { get; set; }


        [Display(Name = "Beni Hatirla")]
        public bool IsPersistent { get; set; }

        public string ReturnURL { get; set; }

    }
}
