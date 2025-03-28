using System.ComponentModel.DataAnnotations;

namespace LostAndFound.Models;

public class ItemDTO
{
	[Required(ErrorMessage = "Item name is required")]
	[Display(Name = "Item Name")]
    public required string Name { get; set; }
	[Required(ErrorMessage = "Category is required")]
	[Display(Name = "Category")]
	public required CategoryDTO Category { get; set; }
	[Required(ErrorMessage = "Item description is required")]
	[Display(Name = "Item Description")]
    public required string Description { get; set; }
	[Required(ErrorMessage = "Location area is required")]
	[Display(Name = "Location Area")]
    public required string LocationArea { get; set; }
	[Display(Name = "Specific Location")]
	public string? SpecificLocation { get; set; }
	// contact data for non-registered users
	[Display(Name = "Contact Name")]
	public string? ContactName { get; set; }
	[Display(Name = "Contact Email")]
	public string? ContactEmail { get; set; }
	public PhotoDTO? Photo { get; set; }
    public DateTime DateLost { get; set; }
    public DateTime DateFound { get; set; }
    public DateTime PostedDate { get; set; } = DateTime.Now;
}
