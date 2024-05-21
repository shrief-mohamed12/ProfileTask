using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProfileTask.Models
{
    public class Employee
    {
        [Key]
       public  int Id { get; set; }
        public string employeeName { get; set; }
        public string employeeJop { get; set; }
        [NotMapped]
        public IFormFile backgroundPicture { get; set; }
        [NotMapped]
        public IFormFile employeePicture { get; set; }
        public string backgroundPicturePath { get; set; }
        public string employeePicturePath { get; set; }
        public string about  { get; set; }
        public List<Note> notes { get; set; }
        public List<Contacts> contacts { get; set;}
        public List<Background> backgrounds { get; set; }
        public List<Education> educations { get; set; }
        public List<Licenses> Licenses { get; set; }
        public List<OtherEx> otherExperience { get; set; }

    }
}
