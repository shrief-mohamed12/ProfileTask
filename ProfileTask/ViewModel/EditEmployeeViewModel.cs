using System.ComponentModel.DataAnnotations.Schema;

namespace ProfileTask.ViewModel
{
    public class EditEmployeeViewModel
    {
        public int Id { get; set; }
        public string employeeName { get; set; }
        public string employeeJop { get; set; }
        [NotMapped]
        public IFormFile backgroundPicture { get; set; }
        [NotMapped]
        public IFormFile employeePicture { get; set; }
        public string backgroundPicturePath { get; set; }
        public string employeePicturePath { get; set; }
    }
}
