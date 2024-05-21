using System.ComponentModel.DataAnnotations;

namespace ProfileTask.Models
{
    public class Note
    {
        [Key]
      public  int Id { get; set; }
      public  int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
    }
}
