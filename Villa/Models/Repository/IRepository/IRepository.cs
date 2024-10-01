using System.Linq.Expressions;

namespace VillaApp.Models.Repository.IRepository
{
	public interface IRepository<T> where T : class
	{
		 IEnumerable<T> GetAll(string? IncludeProperties = null);

		 IEnumerable<T> GetAllWithCondition(Expression<Func<T,bool>> filter,string? IncludeProperties = null);

		 T GetOrDefault(Expression<Func<T,bool>> filter,string? IncludeProperties = null);

		 void Delete(T entity);

		 void Add (T entity);
		 void RemoveRange(IEnumerable<T> entities);
		 
	}
}
