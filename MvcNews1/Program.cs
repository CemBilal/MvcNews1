using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MvcNews1.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();



//class in icinde var tanimlayamazsiniz

builder.Services.AddDbContext<AppDbContext>(Options => {

    Options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});//nudget package tan sql server i indirmeden UseSqlServer kodunu calistiramazsin


builder.Services.AddIdentity<User, Role>(config =>
{
    config.Password.RequireDigit = false;
    config.Password.RequireLowercase = false;
    config.Password.RequireUppercase = false;
    config.Password.RequiredLength = 1;
    config.Password.RequireNonAlphanumeric = false;
    //burada passwordumun nasil olmasi gerektigini belirtiyorum 
    //belirtmezsem kendinden var olan kurallar ile bu islemi yapar
})

    .AddEntityFrameworkStores<AppDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();//kullanicinin girebilip giremeyecegine bakar

app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
//yukarida yazilanlari scaffolding ten alarak yazmadan 
//olusturdugun admin sayfasina yonelmez


await CreateDefaultUserandRoles();

app.Run();



async Task CreateDefaultUserandRoles()
//????IserviceProviderlari neden daha sonra sildik?
//videoda bunlara zaten ulasabiliyorsak tekradan tanimlamaya gerek yok demis hoca
{
    using var serviceProvider= builder!.Services.BuildServiceProvider();
    using var scope = serviceProvider.CreateScope();

    var context = scope.ServiceProvider.GetRequiredService<MvcNews1.Models.AppDbContext>();
    context.Database.Migrate();//yukaridaki iki satir database in otomatik olarak migration i
    //calistirir ve update-database yapmadan otomatik olarak database i calistirir


    var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();
    var UserManager = serviceProvider.GetRequiredService<UserManager<User>>();

    var roles = new[]//admin rollerini olutururken tek tek olusturmamak icin once liste yapiyorum...
    {
        new Role{ Name="Administrators"},
        new Role { Name = "Reporters" },
        new Role { Name = "Authors" }
    };

    foreach (var role in roles)//...sonra foreach ile yukaridaki listeyi kullanarak modelleri olusturuyorum
                               // var roleExists = await roleManager.RoleExistsAsync(role.Name); role exist i burada ve asagida ayri ayri
                               // tanimlamak yerine sadece asagida tanimliyorum cunku sadece bir kere kullandim 
        if (!await roleManager.RoleExistsAsync(role.Name))//foreach in icinde sadece bir metod oldugu icin suslu
            //parantezleri kaldirdim
            await roleManager.CreateAsync(role);


    //yukarida tanimlanan konfigurasyona ulasmak icin kullanicinin bilgilerini 
    //appsettings ten bakarak buraya tanimlamam gerekiyor 
    var user = new User
    {
        UserName = builder.Configuration.GetValue<string>("App:DefaultUser:UserName"),
        //GetValue dosya icinde bir deger okumaya yarar
        Name = builder.Configuration.GetValue<string>("App:DefaultUser:Name"),



    };
    var UserExist = await UserManager.FindByNameAsync(user.UserName);
    //burada findbynameasync ile user in var olup olmadigini anliyoruz

    //task donduren butun kodlar async tir var diger kodlar ile paralel calisir 
    //ayni andan birden cok is yapabilmek icin 
    //await bu islerin bitmesini bekletir ve sirali gerceklesmesini saglar
    //async icin kullanilir

    if (UserExist == null)//eger boyle bir kullanici yok ise
    { //olusturuyor
        var result = await UserManager.CreateAsync(user, builder.Configuration.GetValue<string>("App:DefaultUser:Password"));
        //kullaniciyi olusturduktan sonra kendisini administrator rolune ekliyoruz
        if (result.Succeeded) await UserManager.AddToRoleAsync(user, "Administrators");
        //createasync ile boyle bir kullanici yok ise olusturuyorum        
    }
}