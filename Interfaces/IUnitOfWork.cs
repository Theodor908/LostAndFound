using System;

namespace LostAndFound.Interfaces;

public interface IUnitOfWork
{
    IPostRepository PostRepository { get; }
    ICategoryRepository CategoryRepository { get; }
    IItemRepository ItemRepository { get; }
    IUserRepository UserRepository { get; }
    IPhotoRepository PhotoRepository { get; }
    IRoleRepository RoleRepository { get; }
    IReportRepository ReportRepository { get; }
    IBanRepository BanRepository { get; }
    Task<bool> Complete();
    bool HasChanges();
}
