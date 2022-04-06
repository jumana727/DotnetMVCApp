using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCApp.Models
{
    public class Subject
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Subject_Name { get; set; }

        public string syllabus { get; set; }

        [Range(1, 5)]
        public int credits { get; set; }
    }
}    