using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ProfileTask.Models
{
    public class Education
    {
        [Key]

         
        public int Id { get; set; }
        public  int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        [NotMapped]
        public IFormFile Picture { get; set; }
        public string EducPicturePath { get; set; }
        public DateTime dateFrom { get; set; }
        public DateTime dateTo { get; set; }
    }
}
