using System.ComponentModel.DataAnnotations;

namespace ProfileTask.Models
{
    public class Background
    {
        [Key]
        int Id { get; set; }
        int EmployeeId { get; set; }
        public string Title  { get; set; }
        public string Description { get; set; }
        public string OrgnizeName { get; set; }
        public string Picture { get; set; }
        public DateTime dateFrom { get; set; }
        public DateTime dateTo { get; set; }

    }
}
