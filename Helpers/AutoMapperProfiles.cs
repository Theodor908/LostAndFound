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

        CreateMap<Photo, PhotoDTO>()
            .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url))
            .ForMember(dest => dest.PublicId, opt => opt.MapFrom(src => src.PublicId));
        CreateMap<PhotoDTO, Photo>()
            .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url))
            .ForMember(dest => dest.PublicId, opt => opt.MapFrom(src => src.PublicId));

        CreateMap<Item, ItemDTO>()
            .ForMember(dest => dest.PhotosDTO, opt => opt.MapFrom(src => src.Photos))
            .ForMember(dest => dest.Photos, opt => opt.Ignore()); 

        CreateMap<ItemDTO, Item>()
            .ForMember(dest => dest.Photos, opt => opt.Ignore()); 

        
        CreateMap<Category, CategoryDTO>();
        CreateMap<CategoryDTO, Category>();

        CreateMap<PagedList<Item>, PagedList<ItemDTO>>()
            .ConvertUsing<PagedListConverter<Item, ItemDTO>>();

    }
}
