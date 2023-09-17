using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebAppForFile.Models
{
    public class FileAndEmailModel
    {
        [DisplayName("Upload File")]
        public string? FileDetails { get; set; }

        [Required(ErrorMessage = "Please select a file.")]
        [RegularExpression(@"^.*\.docx$", ErrorMessage = "Please upload a .docx file.")]
        public IFormFile? File { get; set; }

        [DisplayName("Your Email")]
        [Required(ErrorMessage = "Please enter your email address.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string? Email { get; set; }
    }
}
