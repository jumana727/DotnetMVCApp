using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCApp.Models
{
     public class Teacher
    {

        [Key]
        public int TeacherId { get; set; }

        [Required]
        public string Name { get; set; }

    }
}