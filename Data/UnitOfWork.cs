using System;
using LostAndFound.Interfaces;

namespace LostAndFound.Data;

public class UnitOfWork(DataContext dataContext, ICategoryRepository categoryRepository, IItemRepository itemRepository, IUserRepository userRepository) : IUnitOfWork
{
    public ICategoryRepository CategoryRepository => categoryRepository;
    public IItemRepository ItemRepository => itemRepository;
    public IUserRepository UserRepository => userRepository;
    
    public async Task<bool> Complete()
    {
        return await dataContext.SaveChangesAsync() > 0;
    }

    public bool HasChanges()
    {
        return dataContext.ChangeTracker.HasChanges();
    }
}
