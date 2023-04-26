using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace FormsWebApp.Models
{
    public class Field
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; } = null!;

        public string dataType { get; set; } = null!;

        public bool required { get; set; } = false!;

        [ForeignKey("Form")]
        public int form_id { get; set; }

        [NotMapped]
        public Form form { get; set; }
    }
}
