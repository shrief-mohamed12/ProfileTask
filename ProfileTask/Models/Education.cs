using System.ComponentModel.DataAnnotations;

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
        public string Picture { get; set; }
        public DateTime dateFrom { get; set; }
        public DateTime dateTo { get; set; }
    }
}
