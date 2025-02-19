using App.Domain;
namespace App.Infrastructure.Interfaces
{
    public interface IRepositoryBase
    {
        Task<bool> AddAsync<T>(T entity) where T : BaseEntity;
        Task<bool> DeleteAsync<T>(long Pk_ID) where T : BaseEntity;
        Task<bool> UpdateAsync<T>(T entity) where T : BaseEntity;
        Task<bool> SaveChangeAsync();
        Task<T> GetByIDAsync<T>(long Pk_ID) where T : BaseEntity;
        Task<List<T>> GetAllAsync<T>() where T : BaseEntity;
        IQueryable<T> Queryrable<T>() where T : BaseEntity;
    }
}
