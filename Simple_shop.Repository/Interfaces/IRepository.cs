using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Simple_shop.Repository.Interfaces
{
	public interface IRepository<TEntity> where TEntity : class
	{
		TEntity Get(object id);
		Task<TEntity> GetAsync(object id);
		IEnumerable<TEntity> GetAll();
		Task<IEnumerable<TEntity>> GetAllAsync();
		IEnumerable<TEntity> Find(Expression<Func<TEntity,bool>> predicate);

		void Add(TEntity entity);
		void AddRange(IEnumerable<TEntity> entities);

		void Update(TEntity entity);
		void UpdateRange(IEnumerable<TEntity> entities);

		void Remove(object id);
		void Remove(TEntity entity);
		void RemoveRange(params TEntity[] objects);
	}
}
