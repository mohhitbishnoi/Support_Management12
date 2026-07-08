using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Repository;

public interface IGenericRepository<T> where T : class
{
    IQueryable<T> Entities { get; }
    Task<T> PostAsync(T entity);
    Task<T>PutAsync(int id,T entity);
    Task <T> DeleteAsync(int id );
    Task<T?> GetByIdAsync(int id);
    Task<List<T>> GetAllAsync();
}
