using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using VillaApp.Models.Data;
using VillaApp.Models.Repository.IRepository;

namespace VillaApp.Models.Repository
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly AppDbContext _db;
		private DbSet<T> _dbSet;
        public Repository(AppDbContext db)
        {
            this._db = db;
			this._dbSet = db.Set<T>();
        }
        public void Add(T entity)
		{
			this._dbSet.Add(entity);
		}

		public void Delete(T entity)
		{
			this._dbSet.Remove(entity);
		}

		public IEnumerable<T> GetAll(string? IncludeProperties = null)
		{
			IQueryable<T> query = this._dbSet;
			if (!string.IsNullOrEmpty(IncludeProperties))
			{
				foreach(var property in IncludeProperties.Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(property);
				}
			}
			return query.ToList();
		}

		public IEnumerable<T> GetAllWithCondition(Expression<Func<T, bool>> filter, string? IncludeProperties = null)
		{
			IQueryable<T> query = this._dbSet.Where(filter);
			if (!string.IsNullOrEmpty(IncludeProperties))
			{
				foreach (var property in IncludeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(property);
				}
			}
			return query.ToList();
		}

		public T GetOrDefault(Expression<Func<T, bool>> filter, string? IncludeProperties = null)
		{
			IQueryable<T> query = this._dbSet.Where(filter);
			if (!string.IsNullOrEmpty(IncludeProperties))
			{
				foreach(var property in IncludeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(property);
				}
			}
			return query.FirstOrDefault();
		}

		public void RemoveRange(IEnumerable<T> entities)
		{
			this._dbSet.RemoveRange(entities);
		}
	}
}
