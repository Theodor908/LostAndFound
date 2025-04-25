using System.ComponentModel.DataAnnotations;
using LostAndFound.Helpers.Validators;
namespace LostAndFound.Models;
public class ItemDTO
{
    [Required(ErrorMessage = "Item name is required")]
    [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
    [Display(Name = "Item Name")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Description is required")]
    [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
    [Display(Name = "Item Description")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Country is required")]
    [Display(Name = "Country")]
    public string Country { get; set; } = string.Empty;

    [Required(ErrorMessage = "City is required")]
    [Display(Name = "City")]
    public string City { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Location is required")]
    [Display(Name = "Location")]
    public string Location { get; set; } = string.Empty;

    [Required(ErrorMessage = "Specific location details are required")]
    [StringLength(200, ErrorMessage = "Location cannot exceed 200 characters")]
    [Display(Name = "Specific Location")]
    public string SpecificLocation { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please select a category")]
    [Range(1, int.MaxValue, ErrorMessage = "Please select a valid category")]
    [Display(Name = "Category")]
    public int CategoryId { get; set; }

    [Display(Name = "When Found")]
    public DateTime? FoundAt { get; set; } = DateTime.UtcNow;

    [Display(Name = "When Lost")]
    public DateTime? LostAt { get; set; } = DateTime.UtcNow;

    [Display(Name = "Found Status")]
    public bool IsFound { get; set; } = false;

    [Display(Name = "Photos")]
    [MaxFileSize(5 * 1024 * 1024, ErrorMessage = "Maximum file size is 5MB")]
    [AllowedExtensions([".jpg", ".jpeg", ".png", ".gif"], ErrorMessage = "Only image files (.jpg, .jpeg, .png, .gif) are allowed")]
    public List<IFormFile> Photos { get; set; } = [];

    public List<PhotoDTO> PhotosDTO { get; set; } = [];
    
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (LostAt == null && FoundAt == null)
        {
            yield return new ValidationResult(
                "Please provide either when the item was lost or when it was found",
                [nameof(LostAt), nameof(FoundAt)]);
        }
    }
}