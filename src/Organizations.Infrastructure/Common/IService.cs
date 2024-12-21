using Organizations.API.Common.Models;

namespace Organizations.API.Common.Services;

public interface IService<T> where T : BaseEntity<int>
{
    Task<T> CreateAsync(T entity);
    Task<T> GetByIdAsync(int id);
    Task<T> UpdateAsync(T entity);
    Task<T> DeleteAsync(int id);
}