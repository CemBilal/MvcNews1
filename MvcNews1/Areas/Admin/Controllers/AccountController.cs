using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MvcNews1.Models;

namespace MvcNews1.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<User> signInManager;

        public AccountController(SignInManager<User> signInManager)//diger classlarda da kullanmak istedigin 
        //bir methodu bir ust kademeye tasimak icin method isminin uzerine gelip ctrl . ve create assign
        //field kisayolunu kullanabilirsin
        {
            this.signInManager = signInManager;
        }
        
 
        public IActionResult LogIn()
        {

            return View( new LogInViewModel { IsPersistent=true});//burasi ile kullanicidan veri alacagimiz icin
                          //model kisminda class olusturyoruz

        }

        //ayni isimli iki method vermenin tek yolu parametrelerinin farkli olmasidir


        [HttpPost]//httppost methodu post olan html tablolarini cekmeye yarar
        public async Task<IActionResult> LogIn(LogInViewModel model) //bu sekilde giris kisminda kullanicinin 
            //girdigi bilgiler login e gider 
        {
            var result = await signInManager.PasswordSignInAsync(model.UserName, model.Password, model.IsPersistent, true);
            //butun asyns methodlari task dondurur
            //async methodlarinda await kullanilir
            if (result.Succeeded)
            {
                return Redirect(model.ReturnURL ?? "/");//en son kullandigi yere gonderir
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Gecersiz kullanici girisi");
                return View();
            }
        
        }

        public async Task<IActionResult> Logout()
          {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
           }
        


    }
}
