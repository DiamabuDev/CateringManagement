using System.ComponentModel.DataAnnotations;

namespace CateringManagement.Models
{
    public class FunctionDocument : UploadedFile
    {
        [Display(Name = "Function")]
        public int FunctionID { get; set; }

        public Function Function { get; set; }
    }
}
