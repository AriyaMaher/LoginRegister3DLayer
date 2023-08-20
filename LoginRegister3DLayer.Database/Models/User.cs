using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginRegister3DLayer.Database.Models;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Display(Name ="UserCode")]
    public Guid Id { get; set; }

    [Required]
    [Display(Name ="RoleCode")]
    public Guid RoleId { get; set; }

    [Required]
    [Display(Name = "شماره موبایل")]
    [MaxLength(11)]
    [MinLength(11)]
    public string Mobile { get; set; }

    [Required]
    public string Password { get; set; }

    [ForeignKey("RoleId")]
    public virtual Role Role { get; set; }
}
