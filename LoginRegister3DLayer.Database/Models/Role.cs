using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginRegister3DLayer.Database.Models;

public class Role
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Display(Name ="RoleCode")]
    public Guid Id { get; set; }

    [Required]
    public string RoleName { get; set; }


    public virtual ICollection<User> Users  { get; set; }
}
