using System;
using AutoMapper;
using LostAndFound.Entities;
using LostAndFound.Models;

namespace LostAndFound.Helpers;

public class AutoMapperProfiles: Profile
{
    public AutoMapperProfiles()
    {

        CreateMap<AppUser, UserDTO>();
        CreateMap<UserDTO, AppUser>();

        CreateMap<Post, PostDTO>();
        CreateMap<PostDTO, Post>();

        CreateMap<Photo, PhotoDTO>();
        CreateMap<PhotoDTO, Photo>();

        CreateMap<Item, ItemDTO>()
            .ForMember(dest => dest.PhotosDTO, opt => opt.MapFrom(src => src.Photos))
            .ForMember(dest => dest.Photos, opt => opt.Ignore()); 

        CreateMap<ItemDTO, Item>()
            .ForMember(dest => dest.Photos, opt => opt.Ignore()); 

        
        CreateMap<Category, CategoryDTO>();
        CreateMap<CategoryDTO, Category>();

    }
}
