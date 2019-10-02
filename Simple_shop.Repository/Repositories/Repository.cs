using Microsoft.EntityFrameworkCore;
using Simple_shop.EF;
using Simple_shop.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Simple_shop.Repository.Repositories
{
	public class Repository<TEntity> : IRepository<TEntity> where TEntity : class 
	{
		#region Fields

		protected readonly DbSet<TEntity> _dbSet;

		#endregion

		#region Constructor

		public Repository(AppDbContext appDbContext)
		{
			_dbSet = appDbContext.Set<TEntity>();
		}

		#endregion

		#region Public Methods

		public void Add(TEntity entity)
		{
			_dbSet.Add(entity);
		}

		public void AddRange(IEnumerable<TEntity> entities)
		{
			_dbSet.AddRange(entities);
		}

		public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
		{
			return _dbSet.Where(predicate);
		}

		public TEntity Get(object id)
		{
			return _dbSet.Find(id);
		}

		public async Task<TEntity> GetAsync(object id)
		{
			return await _dbSet.FindAsync(id);
		}

		public IEnumerable<TEntity> GetAll()
		{
			return _dbSet.ToList();
		}

		public async Task<IEnumerable<TEntity>> GetAllAsync()
		{
			return await _dbSet.ToListAsync();
		}

		public void Update(TEntity entity)
		{
			_dbSet.Update(entity);
		}

		public void UpdateRange(IEnumerable<TEntity> entities)
		{
			_dbSet.UpdateRange(entities);
		}

		public void Remove(object id)
		{
			var entity = _dbSet.Find(id);
			_dbSet.Remove(entity);
		}

		public void Remove(TEntity entity)
		{
			_dbSet.Remove(entity);
		}

		public void RemoveRange(params TEntity[] objects)
		{
			_dbSet.RemoveRange(objects);
		}

		#endregion
	}
}
