using EasyCashIdentityProject.DtoLayer.Dtos.AppUserDtos;
using EasyCashIdentityProject.EntityLayer.Concrete;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MimeKit;

namespace EasyCashIdentityProject.PresentationLayer.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public RegisterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(AppUserRegisterDto appUserRegisterDto)
        {
            if (ModelState.IsValid)
            {
                Random rnd = new Random();

                AppUser appUser = new AppUser()
                {
                    UserName = appUserRegisterDto.UserName,
                    Name = appUserRegisterDto.Name,
                    SurName = appUserRegisterDto.Surname,
                    Email = appUserRegisterDto.email,
                    City = "aaaa",
                    District = "bbbb",
                    ImageUrl = "cccc",
                    ConfirmCode = rnd.Next(100000,1000000)
                };

			    var result = await _userManager.CreateAsync(appUser, appUserRegisterDto.Password);

                if (result.Succeeded)
                {
                    MimeMessage mimeMessage = new MimeMessage();
                    MailboxAddress mailboxAddressFrom = new MailboxAddress("Easy Cash Admin-Naki ", "nakiagca10@gmail.com");
                    MailboxAddress mailboxAddressTo = new MailboxAddress("user", appUser.Email);

                    mimeMessage.From.Add(mailboxAddressFrom);
                    mimeMessage.To.Add(mailboxAddressTo);

                    var bodyBuilder = new BodyBuilder();
                    bodyBuilder.TextBody = "Kayıt işleminini gerçekleştirmek için onay kodunuz : " + appUser.ConfirmCode;
                    mimeMessage.Body = bodyBuilder.ToMessageBody();

                    mimeMessage.Subject = "Easy Cash Onay kodu";


                    SmtpClient client = new SmtpClient();
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate("nakiagca10@gmail.com", "jlnbafanqrwkqnzc");
                    client.Send(mimeMessage);
                    client.Disconnect(true);

                    return RedirectToAction("Index", "ConfirmMail");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }

            return View();
        }
    }
}


//bu kurallar kaldırılabilir
//şifre en az 6 karakter 
//en az 1 küçük harf içermeli
//en az 1 büyük harf içermeli
//en az 1 sembol harf içermeli
//en az 1 sayı harf içermeli
