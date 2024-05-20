using System.ComponentModel.DataAnnotations;

namespace ProfileTask.Models
{
    public class Licenses
    {
        [Key]

        int Id { get; set; }
        int EmployeeId { get; set; }
        public string Title { get; set; }
    }
}
