using System.ComponentModel.DataAnnotations;

namespace Cheddar.ViewModel.Domain
{
    public class OrganizationViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
