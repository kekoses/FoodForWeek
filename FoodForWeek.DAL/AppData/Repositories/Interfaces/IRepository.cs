using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodForWeek.DAL.AppData.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        public Task<T> Create(T newEntity);
        public Task<T> Update(int idEntity, T editedEntity);
        public Task<T> Get(int idEntity);
        public Task<IEnumerable<T>> GetList();
        public Task<bool> Remove(int idEntity);
    }
}
