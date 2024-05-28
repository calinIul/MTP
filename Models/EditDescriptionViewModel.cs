using System.ComponentModel.DataAnnotations;

namespace ProductManagementApp.Models;
public class EditDescriptionViewModel
{
    public int Id { get; set; }
    
    public string Name { get; set; } // For display purposes

    [Required]
    public string Description { get; set; }
}
