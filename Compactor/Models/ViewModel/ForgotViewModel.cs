using System.ComponentModel.DataAnnotations;

namespace Compactor.Models
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
