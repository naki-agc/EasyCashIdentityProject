using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCashIdentityProject.DtoLayer.Dtos.AppUserDtos
{
    public class AppUserRegisterDto
    {
        //böyle attributeler eklenip yapılır ama biz böyle yapmayacağız.
        //[Required(ErrorMessage = "Ad alanı zorunludur")]
        //[Display(Name = "İsim")]
        //[MaxLength(30,ErrorMessage = "Enfazla 30 karakter girebilirsiniz")]
        public string Name { get; set; }
        public string Surname { get; set; }
        public string email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

    //ad,soyad,username,email,şifre,confirmpassword lazım burda bize
}
