﻿using Hotels.DataBase;
using Hotels.Models.Base;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Hotels.Repositories.GenericRepository
{
    public class GenericRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly DataBaseContext _context;
        protected readonly DbSet<TEntity> _table;

        public GenericRepository(DataBaseContext context)
        {
            _context = context;
            _table = _context.Set<TEntity>();
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            var allItems = await _table.AsNoTracking().ToListAsync();
            return allItems;
        }

        public IQueryable<TEntity> GetAllAsQueryable()
        {                   
            return _table.Where(x => x.Id.ToString() != "");     
        }

        // create
        public void Create(TEntity entity)
        {
            _table.Add(entity);
        }

        public async Task CreateAsync(TEntity entity)
        {
            await _table.AddAsync(entity);
        }

        public void CreateRange(IEnumerable<TEntity> entities)
        {
            _table.AddRange(entities);
        }

        public async Task CreateRangeAsync(IEnumerable<TEntity> entities)
        {
            await _table.AddRangeAsync(entities);
        }

        // update
        public void Update(TEntity entity)
        {
            _table.Update(entity);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            _table.UpdateRange(entities);
        }

        // delete

        public void Delete(TEntity entity)
        {
            _table.Remove(entity);
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            _table.RemoveRange(entities);
        }

        // find
        public TEntity FindById(object id)
        {       
            return _table.FirstOrDefault(x => x.Id.Equals(id));
        }

        public async Task<TEntity> FindByIdAsync(object id)
        {          
            return await _table.FirstOrDefaultAsync(x => x.Id.Equals(id));           
        }

        // save

        public bool Save()
        {
            try
            {
                return _context.SaveChanges() > 0;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex);
            }

            return false;
        }

        public async Task<bool> SaveAsync()
        {
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex);
            }

            return false;
        }

    }
}
