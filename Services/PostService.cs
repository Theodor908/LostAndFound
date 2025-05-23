using AutoMapper;
using LostAndFound.Entities;
using LostAndFound.Helpers;
using LostAndFound.Interfaces;
using LostAndFound.Models;

namespace LostAndFound.Services;

public class PostService(IUnitOfWork unitOfWork, IPhotoService photoService, IMapper mapper) : IPostService
{
    public async Task<PostListDTO> GetAllPostsAsync(PostFilterParams postFilterParams)
    {
        var posts = await unitOfWork.PostRepository.GetAllPostsAsync(postFilterParams);
        var postsDTO = mapper.Map<PagedList<PostDTO>>(posts);
        PostListDTO postListDTO = new()
        {
            SearchTerm = postFilterParams.SearchTerm,
            PostType = postFilterParams.PostType,
            IsActive = postFilterParams.IsActive,
            PostList = postsDTO,
        };
        return postListDTO;
    }

    public async Task<string> GetPostTitleByIdAsync(int id)
    {
        var title = await unitOfWork.PostRepository.GetPostTitleByIdAsync(id);
        return title ?? string.Empty;
    }

    public async Task<int> CreatePostAsync(string username, PostDTO postDto, bool isValid)
    {
        if (!isValid) return -1;

        var user = await unitOfWork.UserRepository.GetUserByUsernameAsync(username);
        if (user == null) return -1;

        var post = new Post
        {
            PostType = postDto.PostType,
            Title = postDto.Title,
            Description = postDto.Description,
            AppUserId = user.Id,
            IsClosed = false,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsActive = true,
            AppUser = user,
            Items = [],
        };

        unitOfWork.PostRepository.AddPost(post);

        var postSaved = await unitOfWork.Complete();
        if (!postSaved) return -1;
        user.Posts.Add(post);
        unitOfWork.UserRepository.UpdateUser(user);
        List<Item> items = [];
        foreach (var itemDto in postDto.Items)
        {
            var item = new Item
            {
                Name = itemDto.Name,
                Description = itemDto.Description,
                Country = itemDto.Country,
                City = itemDto.City,
                Location = itemDto.Location,
                SpecificLocation = itemDto.SpecificLocation,
                PostedAt = DateTime.UtcNow,
                FoundAt = itemDto.FoundAt,
                LostAt = itemDto.LostAt,
                IsFound = itemDto.IsFound,
                IsClaimed = itemDto.IsClaimed,
                CategoryId = itemDto.CategoryId,
                AppUserId = user.Id,
                PostId = post.Id, 
                Photos = []
            };
            items.Add(item);
            unitOfWork.ItemRepository.AddItem(item);
        }

        var itemsSaved = await unitOfWork.Complete();
        if(!itemsSaved)
        {
            await unitOfWork.PostRepository.DeletePostAsync(post.Id);
            return -1;
        }
        foreach (var item in items)
        {
            var itemDto = postDto.Items.FirstOrDefault(i => i.Name == item.Name);
            if (itemDto != null && itemDto.Photos != null && itemDto.Photos.Count != 0)
            {
                foreach (var formFile in itemDto.Photos)
                {
                    if (formFile.Length > 0)
                    {
                        var uploadResult = await photoService.UploadPhotoAsync(formFile);
                        if (uploadResult != null)
                        {
                            var photo = new Photo
                            {
                                Name = formFile.Name,
                                Url = uploadResult.SecureUrl.AbsoluteUri,
                                PublicId = uploadResult.PublicId,
                                Item = item,
                                ItemId = item.Id,
                            };
                            unitOfWork.PhotoRepository.AddPhoto(photo);
                            item.Photos.Add(photo);
                        }
                    }
                }
            }
        }
        post.Items.AddRange(items);
        user.Items.AddRange(items);
        var result = await unitOfWork.Complete();
        if (!result)
        {
            await unitOfWork.PostRepository.DeletePostAsync(post.Id);
            return -1;
        }
        return post.Id;
    }

    public async Task<bool> UpdatePostAsync(int id, PostDTO postDto, bool isValid)
    {
        if (!isValid) return false;

        var post = await unitOfWork.PostRepository.GetPostByIdAsync(id);
        if (post == null) 
        {
            return false;
        }

        var user = await unitOfWork.UserRepository.GetUserByIdAsync(post.AppUserId);
        if (user == null) 
        {
            return false;
        }

        post.Title = postDto.Title;
        post.Description = postDto.Description;
        post.UpdatedAt = DateTime.UtcNow;
        post.PostType = postDto.PostType;
        post.IsActive = postDto.IsActive;

        unitOfWork.PostRepository.UpdatePost(post);
        List<Item> deletedItems = [.. post.Items];
        List<Item> newItems = [];
        List<Photo> deletedPhotos = [];
        foreach (var item in post.Items)
        {
            var photos = item.Photos.ToList();
            foreach (var photo in photos)
            {
                deletedPhotos.Add(photo);
            }
        }
        foreach (var itemDto in postDto.Items)
        {
            var item = await unitOfWork.ItemRepository.GetItemByIdAsync(itemDto.Id);
            var itemPhotos = item?.Photos.ToList();
            if (item != null)
            {
                item.Name = itemDto.Name;
                item.Description = itemDto.Description;
                item.Country = itemDto.Country;
                item.City = itemDto.City;
                item.Location = itemDto.Location;
                item.SpecificLocation = itemDto.SpecificLocation;
                item.UpdatedAt = DateTime.UtcNow;
                item.FoundAt = itemDto.FoundAt;
                item.LostAt = itemDto.LostAt;
                item.IsFound = itemDto.IsFound;
                item.IsClaimed = itemDto.IsClaimed;  
                item.Category = await unitOfWork.CategoryRepository.GetCategoryByIdAsync(itemDto.CategoryId);
                item.CategoryId = itemDto.CategoryId;
                deletedItems.Remove(item);
                unitOfWork.ItemRepository.UpdateItemAsync(item);
            }
            else
            {
                var newItem = new Item
                {
                    Name = itemDto.Name,
                    Description = itemDto.Description,
                    Country = itemDto.Country,
                    City = itemDto.City,
                    Location = itemDto.Location,
                    SpecificLocation = itemDto.SpecificLocation,
                    PostedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    FoundAt = itemDto.FoundAt,
                    LostAt = itemDto.LostAt,
                    IsFound = itemDto.IsFound,
                    IsClaimed = itemDto.IsClaimed,
                    Category = await unitOfWork.CategoryRepository.GetCategoryByIdAsync(itemDto.CategoryId),
                    CategoryId = itemDto.CategoryId,
                    AppUserId = user.Id,
                    PostId = post.Id, 
                    Photos = []
                };
                unitOfWork.ItemRepository.AddItem(newItem);
                newItems.Add(newItem);
                post.Items.Add(newItem);
                user.Items.Add(newItem);
            }

        
        }

        foreach(var item in newItems)
        {
            var itemDto = postDto.Items.FirstOrDefault(i => i.Name == item.Name);
            if (itemDto != null && itemDto.Photos != null && itemDto.Photos.Count != 0)
            {
                foreach (var formFile in itemDto.Photos)
                {
                    if (formFile.Length > 0)
                    {
                        var uploadResult = await photoService.UploadPhotoAsync(formFile);
                        if (uploadResult != null)
                        {
                            var photo = new Photo
                            {
                                Name = formFile.Name,
                                Url = uploadResult.SecureUrl.AbsoluteUri,
                                PublicId = uploadResult.PublicId,
                                Item = item,
                                ItemId = item.Id,
                            };
                            unitOfWork.PhotoRepository.AddPhoto(photo);
                            item.Photos.Add(photo);
                        }
                    }
                }
            }
        }

        foreach (var item in deletedItems)
        {
            List<Photo> photos = [.. item.Photos];
            foreach (var photo in photos)
            {
                var result = await photoService.DeletePhotoAsync(photo.PublicId!);
                if (result == null) return false;
                unitOfWork.PhotoRepository.DeletePhotoAsync(photo);
            }
            user.Items.Remove(item);
            post.Items.Remove(item);
            await unitOfWork.ItemRepository.DeleteItemAsync(item);
        }

        foreach (var itemDto in postDto.Items)
        {
            var item = await unitOfWork.ItemRepository.GetItemByIdAsync(itemDto.Id);
            if (item != null && itemDto.Photos != null && itemDto.Photos.Count != 0)
            {
                foreach (var photo in itemDto.PhotosDTO)
                {
                    var photoToDelete = item.Photos.FirstOrDefault(p => p.Id == photo.Id);
                    if (photoToDelete != null)
                    {
                        deletedPhotos.Remove(photoToDelete);
                        item.Photos.Remove(photoToDelete);
                        unitOfWork.PhotoRepository.DeletePhotoAsync(photoToDelete);
                    }
                }
            }
        }

        foreach (var itemDto in postDto.Items)
        {
            var item = await unitOfWork.ItemRepository.GetItemByIdAsync(itemDto.Id);
            if (item != null && itemDto.Photos != null && itemDto.Photos.Count != 0)
            {
                foreach (var formFile in itemDto.Photos)
                {
                    if (formFile.Length > 0)
                    {
                        var uploadResult = await photoService.UploadPhotoAsync(formFile);
                        if (uploadResult != null)
                        {
                            var photo = new Photo
                            {
                                Name = formFile.Name,
                                Url = uploadResult.SecureUrl.AbsoluteUri,
                                PublicId = uploadResult.PublicId,
                                Item = item,
                                ItemId = item.Id,
                            };
                            unitOfWork.PhotoRepository.AddPhoto(photo);
                            item.Photos.Add(photo);
                        }
                    }
                }
            }
        }



        var postUpdateResult = await unitOfWork.Complete();
        if (!postUpdateResult) return false;

        return true;
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

    public async Task<bool> DeletePostAsync(int id)
    {

        var post = await unitOfWork.PostRepository.GetPostByIdAsync(id);
        if (post == null) return false;

        var user = await unitOfWork.UserRepository.GetUserByIdAsync(post.AppUserId);
        if (user == null) return false;

        List<Item> items = [.. post.Items];
        foreach (var item in items)
        {
            List<Photo> photos = [.. item.Photos];
            foreach (var photo in photos)
            {
                var result = await photoService.DeletePhotoAsync(photo.PublicId!);
                if (result == null) return false;
                unitOfWork.PhotoRepository.DeletePhotoAsync(photo);
            }
            user.Items.Remove(item);
            post.Items.Remove(item);
            await unitOfWork.ItemRepository.DeleteItemAsync(item);
        }

        await unitOfWork.PostRepository.DeletePostAsync(id);
        var saveResult = await unitOfWork.Complete();
        if (!saveResult) return false;
        
        return true;

    }

    public async Task<int> GetPostCountAsync()
    {
        return await unitOfWork.PostRepository.GetPostCountAsync();
    }

    public async Task<PostDTO> ReducePostDTOItemListAsync(int itemIndex, int postId)
    {
        var post = await GetPostDetailsByIdAsync(postId);
        if (post == null)
        {
            return null!;
        }

        if (itemIndex < 0 || itemIndex >= post.Items.Count)
        {
            return post;
        }


        for (int i = itemIndex; i < post.Items.Count - 1; i++)
        {
            post.Items[i] = post.Items[i + 1];
        }

        post.Items.RemoveAt(post.Items.Count - 1);
        if (post.Items.Count == 0)
        {
            post.Items.Add(new ItemDTO());
        }
        return post;
        
    }
}
