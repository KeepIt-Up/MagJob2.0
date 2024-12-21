//using Organizations.API.Common.Models;
//using Organizations.API.Common.Repositories;

//namespace Organizations.API.Common.Services;

//public interface IBaseService<T> where T : BaseEntity<int>
//{
//    Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default);
//    Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default);
//    Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default);
//    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);

//    Task<PaginatedList<T>> GetAllAsync(PaginationOptions paginationOptions, CancellationToken cancellationToken = default);
//}

//public class BaseService<T> : IBaseService<T> where T : BaseEntity<int>
//{
//    protected readonly IAsyncRepository<T> _repository;

//    public BaseService(IAsyncRepository<T> repository)
//    {
//        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
//    }

//    public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default)
//    {
//        return await _repository.CreateAsync(entity, cancellationToken);
//    }

//    public async Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default)
//    {
//        return await _repository.GetByIdAsync(id, cancellationToken);
//    }

//    public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default)
//    {
//        return await _repository.UpdateAsync(entity, cancellationToken);
//    }

//    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
//    {
//        return await _repository.DeleteAsync(id, cancellationToken) != null;
//    }

//    public async Task<PaginatedList<T>> GetAllAsync(PaginationOptions paginationOptions, CancellationToken cancellationToken = default)
//    {
//        var query = _repository.AsQueryable(cancellationToken);
//        return await PaginatedList<T>.CreateAsync(query, paginationOptions);
//    }
//}
