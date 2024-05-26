using System.ComponentModel.DataAnnotations.Schema;

namespace ProfileTask.ViewModel
{
    public class EditBackgroundViewModel
    {
        public int Id { get; set; } 
        public int EmployeeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string OrgnizeName { get; set; }
        [NotMapped]
        public IFormFile Picture { get; set; }
        public string BackgroundPicture { get; set; }
        public DateTime dateFrom { get; set; }
        public DateTime dateTo { get; set; }
    }
}
