using System;

namespace LostAndFound.Models;

public class PostDetailsDTO
{
    public PostDTO Post { get; set; } = null!;
    public UserDTO User { get; set; } = null!;

}
