using System;

namespace LostAndFound.Interfaces;

public interface IUnitOfWork
{
    IPostRepository PostRepository { get; }
    ICategoryRepository CategoryRepository { get; }
    IItemRepository ItemRepository { get; }
    IUserRepository UserRepository { get; }
    Task<bool> Complete();
    bool HasChanges();
}
