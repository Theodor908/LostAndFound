using System;
using LostAndFound.Interfaces;

namespace LostAndFound.Data;

public class UnitOfWork(DataContext dataContext, IPostRepository postRepository, ICategoryRepository categoryRepository, IItemRepository itemRepository, IUserRepository userRepository, IPhotoRepository photoRepository) : IUnitOfWork
{
    public IPostRepository PostRepository => postRepository;
    public ICategoryRepository CategoryRepository => categoryRepository;
    public IItemRepository ItemRepository => itemRepository;
    public IUserRepository UserRepository => userRepository;
    public IPhotoRepository PhotoRepository => photoRepository;
    
    public async Task<bool> Complete()
    {
        return await dataContext.SaveChangesAsync() > 0;
    }

    public bool HasChanges()
    {
        return dataContext.ChangeTracker.HasChanges();
    }
}
