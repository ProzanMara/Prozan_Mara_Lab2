using System.ComponentModel.DataAnnotations;

namespace Prozan_Mara_Lab2.Models
{
    public class Author
    {
        public int ID { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public ICollection<Book>? Books { get; set; }
    }
}
