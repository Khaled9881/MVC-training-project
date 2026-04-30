using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace MVC_Day2.Models.ViewModels
{
    //[Authorize(Roles = "Admin")]
    public class RoleViewModel
    {
        [Display(Name = "Role Name")]
        [Required]
        public string RoleName { get; set; }
    }
}
