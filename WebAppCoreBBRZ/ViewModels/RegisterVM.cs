using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppCoreBBRZ.ViewModels
{
    public class RegisterVM
    {
        [Required]
        [MaxLength(25)]
        [MinLength(3,ErrorMessage ="Minimum 3 Zeichen.")]
        public string Name { get; set; } = null;

        [Required]
        [MaxLength(25)]
        [MinLength(3, ErrorMessage = "Minimum 3 Zeichen.")]
        [EmailAddress]
        public string Email { get; set; } = null;

        [Required]
        [MaxLength(25)]
        [MinLength(3, ErrorMessage = "Minimum 3 Zeichen.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null;

        [Required]
        [Range(12,200,ErrorMessage ="Alter nur erlaubt ab 12 Jahren")]
        public int Alter { get; set; } = default;
    }
}
