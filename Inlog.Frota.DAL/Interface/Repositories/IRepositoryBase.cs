using System.Collections.Generic;

namespace Inlog.Frota.DAL.Interface.Repositories
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {   
        //teste
        bool Add(TEntity obj);
        TEntity GetById(int id);
        IEnumerable<TEntity> GetAll();
        bool Update(TEntity obj);
        bool Remove(TEntity obj);
        void Dispose();
    }
}
