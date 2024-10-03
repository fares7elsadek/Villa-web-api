using System.Linq.Expressions;

namespace VillaApp.Models.Repository.IRepository
{
	public interface IRepository<T> where T : class
	{
		 Task<IEnumerable<T>> GetAllAsync(string? IncludeProperties = null);

		Task<IEnumerable<T>> GetAllWithConditionAsync(Expression<Func<T,bool>> filter,string? IncludeProperties = null);

		 Task<T> GetOrDefaultAsync(Expression<Func<T,bool>> filter,string? IncludeProperties = null);

		 void Delete(T entity);

		 Task AddAsync(T entity);
		 void RemoveRange(IEnumerable<T> entities);
		 
	}
}
