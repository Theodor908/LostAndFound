using System;

namespace LostAndFound.Interfaces;

public interface IUnitOfWork
{
    Task<bool> Complete();
    bool HasChanges();
}
