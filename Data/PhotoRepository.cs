using System;
using LostAndFound.Entities;
using LostAndFound.Interfaces;

namespace LostAndFound.Data;

public class PhotoRepository(DataContext dataContext) : IPhotoRepository
{
    public void AddPhoto(Photo photo)
    {
        dataContext.Photos.Add(photo);
    }

    public void DeletePhotoAsync(Photo photo)
    {
        dataContext.Photos.Remove(photo);
    }
}
