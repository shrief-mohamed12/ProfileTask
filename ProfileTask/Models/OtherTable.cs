using System.ComponentModel.DataAnnotations;

namespace ProfileTask.Models
{
    public class OtherTable
    {
        [Key]

        public  int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }



    }
}
