using System;
using AutoMapper;
using LostAndFound.Entities;
using LostAndFound.Models;

namespace LostAndFound.Helpers;

public class AutoMapperProfiles: Profile
{
    public AutoMapperProfiles()
    {

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

        CreateMap<AppUser, UserDTO>();
        CreateMap<UserDTO, AppUser>();
        
        CreateMap<Category, CategoryDTO>();
        CreateMap<CategoryDTO, Category>();

        CreateMap<ReportPost, ReportPostDTO>();
        CreateMap<ReportPostDTO, ReportPost>();

        CreateMap<ReportUser, ReportUserDTO>();
        CreateMap<ReportUserDTO, ReportUser>();
        
        CreateMap<ReportBug, ReportBugDTO>();
        CreateMap<ReportBugDTO, ReportBug>();

        CreateMap<MemberDTO, AppUser>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Username))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
            .ForMember(dest => dest.Photo, opt => opt.MapFrom(src => src.PhotoDTO))
            .ForMember(dest => dest.Items, opt => opt.Ignore())
            .ForMember(dest => dest.Posts, opt => opt.Ignore());
        CreateMap<AppUser, MemberDTO>()
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
            .ForMember(dest => dest.PhotoDTO, opt => opt.MapFrom(src => src.Photo))
            .ForMember(dest => dest.Photo, opt => opt.Ignore());

        CreateMap<AppRole, RoleDTO>();
        CreateMap<RoleDTO, AppRole>();

        CreateMap<Ban, BanDTO>();
        CreateMap<BanDTO, Ban>();

        CreateMap<PagedList<Item>, PagedList<ItemDTO>>()
            .ConvertUsing<PagedListConverter<Item, ItemDTO>>();

        CreateMap<PagedList<AppUser>, PagedList<MemberDTO>>()
            .ConvertUsing<PagedListConverter<AppUser, MemberDTO>>();

        CreateMap<PagedList<Post>, PagedList<PostDTO>>()
            .ConvertUsing<PagedListConverter<Post, PostDTO>>();

        CreateMap<PagedList<ReportPost>, PagedList<ReportPostDTO>>()
            .ConvertUsing<PagedListConverter<ReportPost, ReportPostDTO>>();

        CreateMap<PagedList<ReportUser>, PagedList<ReportUserDTO>>()
            .ConvertUsing<PagedListConverter<ReportUser, ReportUserDTO>>();

        CreateMap<PagedList<ReportBug>, PagedList<ReportBugDTO>>()
            .ConvertUsing<PagedListConverter<ReportBug, ReportBugDTO>>();

        CreateMap<PagedList<Category>, PagedList<CategoryDTO>>()
            .ConvertUsing<PagedListConverter<Category, CategoryDTO>>();

        CreateMap<PagedList<AppRole>, PagedList<RoleDTO>>()
            .ConvertUsing<PagedListConverter<AppRole, RoleDTO>>();

        CreateMap<PagedList<Ban>, PagedList<BanDTO>>()
            .ConvertUsing<PagedListConverter<Ban, BanDTO>>();
            

        

    }
}
