using System.ComponentModel.DataAnnotations;

namespace ProfileTask.Models
{
    public class Contacts
    {
        [Key]

        int Id { get; set; }
        int EmployeeId { get; set; }
        public string Phone  { get; set; }
        public string Adddress  { get; set; }
        public string Email  { get; set; }
    }
}
