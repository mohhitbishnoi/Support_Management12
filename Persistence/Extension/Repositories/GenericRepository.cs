using Application.Interfaces.Repository;
using Persistence.DataContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text;
using Domain.Commons;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace Persistence.Extension.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseAuditableEntity
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public GenericRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public IQueryable<T> Entities => _applicationDbContext.Set<T>();

        public async Task<T> DeleteAsync(int id)
        {
            var exist = await _applicationDbContext.Set<T>().FirstOrDefaultAsync(x => x.Id==id);
            if (exist == null)
            {
                throw new Exception($"{typeof(T).Name} not found.");
            }
            exist.IsDeleted = true;
            exist.UpdateDate = DateTime.UtcNow;
            _applicationDbContext.Set<T>().Update(exist);
            return exist;
        }

        public async Task<List<T>> GetAllAsync()
        {    
            var exist = await _applicationDbContext.Set<T>().Where(x=> !x.IsDeleted).ToListAsync() ;
            return exist;
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            var exist = await _applicationDbContext.Set<T>().FirstOrDefaultAsync(x => x.Id!.Equals(id) && !x.IsDeleted);
            return exist;
        }

        public async Task<T> PostAsync(T entity)
        {
            entity.CreateDate = DateTime.Now;
            entity.IsDeleted = false;
            entity.IsActive=true;
            await _applicationDbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public Task<T> PutAsync(int id, T entity)
        {
           var exist = _applicationDbContext.Set<T>().FirstOrDefault(x => x.Id!.Equals(id) && !x.IsDeleted);
            if (exist == null)
            {
                throw new InvalidOperationException($"{typeof(T).Name} not found.");
            }
            entity.UpdateDate = DateTime.UtcNow;
            _applicationDbContext.Entry(exist).CurrentValues.SetValues(entity);
            return Task.FromResult(entity);
        }
    }
}
