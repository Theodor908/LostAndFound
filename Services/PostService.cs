using AutoMapper;
using LostAndFound.Entities;
using LostAndFound.Interfaces;
using LostAndFound.Models;

namespace LostAndFound.Services;

public class PostService(IUnitOfWork unitOfWork, IPhotoService photoService, IMapper mapper) : IPostService
{
    public async Task<List<PostDTO>?> GetAllPostsAsync()
    {
        List<Post>? posts = await unitOfWork.PostRepository.GetAllPostsAsync();
        List<PostDTO> postsDTO = mapper.Map<List<PostDTO>>(posts);
        return postsDTO;
    }

    public async Task<PostCreationDTO?> CreatePostAsync(string username, PostDTO postDto, string postType, bool isValid)
    {
        if (!isValid)
        {
            return null;
        }

        // Get the current user. Note: await the asynchronous call.
        var user = await unitOfWork.UserRepository.GetUserByUsernameAsync(username);
        if (user == null)
        {
            return null;
        }

        var post = new Post
        {
            Title = postDto.Title,
            Description = postDto.Description,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            AppUserId = user.Id,
            AppUser = user,
            Items = [],
        };

        foreach (var itemDTO in postDto.Items)
        {
            var category = await unitOfWork.CategoryRepository.GetCategoryByIdAsync(itemDTO.CategoryId);
            var item = new Item
            {
                Name = itemDTO.Name,
                Description = itemDTO.Description,
                Location = itemDTO.Location,
                Country = itemDTO.Country,
                City = itemDTO.City,
                IsFound = postType == "Found",
                CategoryId = itemDTO.CategoryId,
                Category = category!,
                AppUserId = user.Id,
                AppUser = user
            };

            if (postType == "Found")
            {
                item.FoundAt = itemDTO.FoundAt ?? DateTime.UtcNow;
            }
            else
            {
                item.LostAt = itemDTO.LostAt ?? DateTime.UtcNow;
            }

            if (itemDTO.Photos != null && itemDTO.Photos.Count > 0)
            {
                item.Photos = new List<Photo>();
                foreach (var photoFile in itemDTO.Photos)
                {
                    if (photoFile.Length > 0)
                    {
                        var photoResult = await photoService.UploadPhotoAsync(photoFile);
                        if (photoResult.Error != null)
                        {
                            return null;
                        }
                        else
                        {
                            item.Photos.Add(new Photo
                            {
                                Url = photoResult.SecureUrl.AbsoluteUri,
                                PublicId = photoResult.PublicId
                            });
                        }
                    }
                }
            }

            post.Items.Add(item);
        }

        unitOfWork.PostRepository.AddPost(post);

        var isSaved = await unitOfWork.Complete();
        if (!isSaved)
        {
            return null;
        }

        return new PostCreationDTO { Id = post.Id};
    }

    public async Task<PostDTO?> GetPostDetailsByIdAsync(int id)
    {
        var post = await unitOfWork.PostRepository.GetPostWithItemsAndCommentsByIdAsync(id);
        if (post == null)
        {
            return null;
        }

        PostDTO postDTO = mapper.Map<PostDTO>(post);
        return postDTO;
    }
}
