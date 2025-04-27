using System;
using LostAndFound.Entities;

namespace LostAndFound.Interfaces;

public interface IPhotoRepository
{
    void AddPhoto(Photo photo);
    void DeletePhotoAsync(Photo photo);
}
