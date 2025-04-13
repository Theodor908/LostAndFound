using System.ComponentModel.DataAnnotations;
namespace LostAndFound.Models;
using LostAndFound.Helpers.Validators;

public class PostDTO : IValidatableObject
{
    public int Id { get; set; }
    public string? UserName { get; set; }
    [Required(ErrorMessage = "Title is required")]
    [StringLength(200, MinimumLength = 5, ErrorMessage = "Title must be between 5 and 200 characters")]
    [Display(Name = "Title")]
    public required string Title { get; set; }
    
    [Required(ErrorMessage = "Description is required")]
    [StringLength(2000, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 2000 characters")]
    [Display(Name = "Description")]
    public required string Description { get; set; }
    
    [Display(Name = "Created Date")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    [Display(Name = "Last Updated")]
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    
    public int NumberOfItems { get; set; } = 0;
    [Display(Name = "Items")]
    [MinLength(1, ErrorMessage = "At least one item must be added to the post")]
    [Required, ValidateComplexType]
    public List<ItemDTO> Items { get; set; } = [];
    
    [Display(Name = "Comments")]
    public List<CommentDTO> Comments { get; set; } = [];
    
    [Display(Name = "User ID")]
    public int AppUserId { get; set; }
    
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Items == null || Items.Count == 0)
        {
            yield return new ValidationResult(
                "At least one item must be included in your post", 
                [nameof(Items)]);
        }
    }
}