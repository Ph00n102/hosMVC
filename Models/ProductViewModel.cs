using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ConsumeWebAPI.Models
{
    public class ProductViewModel
    {
        public string hn { get; set; }
        public string pname { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string vstdate { get; set; }
    }

}
    