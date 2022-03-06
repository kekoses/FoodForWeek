using FoodForWeekApp.DAL.Helpers;
using FoodForWeekApp.DAL.AppData.Models;
using FoodForWeekApp.DAL.AppData.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodForWeekApp.DAL.AppData.Repositories.Implementations
{
    public abstract class Repository<T> : IRepository<T> where T: BaseType
    {
        protected readonly DbSet<T> _currentSet;
        private readonly AppContext _context;
        public Repository(AppContext context)
        {
            _context = context;
            _currentSet = context.Set<T>();
        }

        public async Task<T> Create(T newEntity)
        {
            newEntity.CheckNull(nameof(newEntity), $"{typeof(T).Name} instance cannot be null!");
            EntityEntry<T> generalEntry=await _currentSet.AddAsync(newEntity);
            await _context.SaveChangesAsync();
            return generalEntry.Entity;
        }

        public async Task<T> Get(int idEntity)
        {
            var searchedEntity = await _currentSet.AsNoTracking().SingleOrDefaultAsync(u => u.Id == idEntity);
            if (searchedEntity is not null)
            {
                return searchedEntity;
            }
            throw new InvalidOperationException($"Requested {typeof(T).Name} doesn`t exist!");
        }

        public async Task<IEnumerable<T>> GetList()
        {
            return await _currentSet.AsNoTracking().ToListAsync();
        }

        public async Task<bool> Remove(int idEntity)
        {
            var removingEntity = await _currentSet.SingleOrDefaultAsync(u => u.Id == idEntity);
            if (removingEntity is not null)
            {
                _currentSet.Remove(removingEntity);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<T> Update(int idEntity, T editedEntity)
        {
            editedEntity.CheckNull(nameof(idEntity), $"{typeof(T)} instance cannot be null!");
            var updatedEntity = await _currentSet.SingleOrDefaultAsync(u => u.Id == idEntity);
            if (updatedEntity is not null)
            {
                if (updatedEntity == editedEntity)
                {
                    return null;
                }
                editedEntity.Id = idEntity;
                var entryEntity=_currentSet.Update(updatedEntity);
                await _context.SaveChangesAsync();
                return entryEntity.Entity;
            }
            throw new InvalidOperationException($"Requested {typeof(T).Name} doesn`t exist!");
        }
    }
}
