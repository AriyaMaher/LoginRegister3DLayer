using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace LoginRegister3DLayer.Core.ViewModels;

public class RegisterViewModel
{

    [Display(Name = "Phone Number")]
    [MaxLength(11)]
    [MinLength(11)]
    public string Mobile { get; set; }

    [Display(Name = "Password", Prompt = "Password at least 8 characters")]
    [MinLength(8, ErrorMessage = "Password at least 8 characters")]
    [MaxLength(25, ErrorMessage = "Password maximum 25 characters")]
    [DataType(DataType.Password)] //hide your caracter
    public string Password { get; set; }

    [Display(Name = "Password", Prompt = "Password at least 8 characters")]
    [MinLength(8, ErrorMessage = "Password at least 8 characters")]
    [MaxLength(25, ErrorMessage = "Password maximum 25 characters")]
    [DataType(DataType.Password)]
    public string RePassword { get; set; }
}
