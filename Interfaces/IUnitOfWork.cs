using System;

namespace LostAndFound.Interfaces;

public interface IUnitOfWork
{
    IPostRepository PostRepository { get; }
    ICategoryRepository CategoryRepository { get; }
    IItemRepository ItemRepository { get; }
    IUserRepository UserRepository { get; }
    IPhotoRepository PhotoRepository { get; }
    Task<bool> Complete();
    bool HasChanges();
}
