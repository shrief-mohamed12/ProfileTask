using System.ComponentModel.DataAnnotations;

namespace ProfileTask.Models
{
    public class OtherEx
    {
        [Key]

        int Id { get; set; }
        int EmployeeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
