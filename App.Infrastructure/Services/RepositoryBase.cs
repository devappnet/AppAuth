using App.Domain;
using App.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace App.Infrastructure.Services
{
    public class RepositoryBase(IAppDbContext dbContext) : IRepositoryBase
    {
        public async Task<bool> AddAsync<T>(T entity) where T : BaseEntity
        {
            if (entity == null) throw new Exception($"{nameof(entity)} is null");
            await dbContext.AddAsync(entity);
            return true;
        }

        public async Task<bool> DeleteAsync<T>(long Pk_ID) where T : BaseEntity
        {
            T data = await dbContext.FindAsync<T>(Pk_ID);
            if (data == null) throw new Exception(ConstantaHelper.Messsage.Data_Not_Find);
            dbContext.Remove(data);
            return true;
        }

        public async Task<bool> SaveChangeAsync()
        {
            var result = await dbContext.SaveChangesAsync();
            return result > 0?true:false;
        }

        public async Task<bool> UpdateAsync<T>(T entity) where T : BaseEntity
        {
            T data = await dbContext.FindAsync<T>(entity.Pk_ID);
            if (data == null) throw new Exception(ConstantaHelper.Messsage.Data_Not_Find);
            dbContext.Update(entity);
            return true;
        }
        public async Task<T> GetByIDAsync<T>(long Pk_ID) where T : BaseEntity
        {
            T data = await dbContext.FindAsync<T>(Pk_ID);
            return data;
        }
        public async Task<List<T>> GetAllAsync<T>() where T : BaseEntity
        {
            var data = await dbContext.Set<T>().ToListAsync();
            return data;
        }
        public IQueryable<T> Queryrable<T>() where T : BaseEntity
        {
            return dbContext.Set<T>(); 
        }
    }
}
