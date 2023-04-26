using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FormsWebApp.Models
{
    public class Form
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; } = null!;

        public string description { get; set; } = null!;

        [DataType(DataType.Date)]
        public DateTime created_at { get; set; }

        public ICollection<Field> fields { get; set; }

        [ForeignKey("Users")]
        public string user_e_mail { get; set; }

        [NotMapped]
        public CLogin user { get; set; }
    }
}
