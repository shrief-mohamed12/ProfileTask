using System.ComponentModel.DataAnnotations;

namespace ProfileTask.Models
{
    public class Contacts
    {
        [Key]

        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public string Phone  { get; set; }
        public string Address { get; set; }
        public string Email  { get; set; }
    }
}
