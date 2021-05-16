using System.ComponentModel.DataAnnotations;

namespace FastFood.Web.ViewModels.Positions
{
    public class PositionsAllViewModel
    {
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }
    }
}
