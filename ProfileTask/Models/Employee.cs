using System.ComponentModel.DataAnnotations;

namespace ProfileTask.Models
{
    public class Employee
    {
        [Key]
        int Id { get; set; }
        public string employeeName { get; set; }
        public string employeeJop { get; set; }
        public string backgroundPicture { get; set; }
        public string bmployeePicture { get; set; }
        public string about  { get; set; }
        List<Contacts> contacts { get; set;}
        List<Backgound> backgounds { get; set; }
        List<Education> educations { get; set; }

    }
}
