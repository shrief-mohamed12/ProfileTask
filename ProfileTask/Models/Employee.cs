using System.ComponentModel.DataAnnotations;

namespace ProfileTask.Models
{
    public class Employee
    {
        [Key]
       public  int Id { get; set; }
        public string employeeName { get; set; }
        public string employeeJop { get; set; }
        public string backgroundPicture { get; set; }
        public string bmployeePicture { get; set; }
        public string about  { get; set; }
        public List<Note> notes { get; set; }
        public List<Contacts> contacts { get; set;}
        public List<Background> backgrounds { get; set; }
        public List<Education> educations { get; set; }
        public List<Licenses> Licenses { get; set; }
        public List<OtherEx> otherExperience { get; set; }

    }
}
