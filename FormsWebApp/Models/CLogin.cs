using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FormsWebApp.Models
{
    public class CLogin
    {
        [Key]
        public string e_mail { get; set; }
        public string password { get; set; } = null!;

        [NotMapped]
        public bool keep_logged_in { get; set; } = false;
    }
}
