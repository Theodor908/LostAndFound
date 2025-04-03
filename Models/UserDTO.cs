namespace LostAndFound.Models;

public class UserDTO
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public List<ItemDTO> Items { get; set; } = [];
    public List<PostDTO> Posts { get; set; } = [];
    public List<CommentDTO> Comments { get; set; } = [];

}
