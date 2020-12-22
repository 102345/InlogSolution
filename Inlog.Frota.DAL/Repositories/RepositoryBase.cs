using Inlog.Frota.DAL.Context;
using Inlog.Frota.DAL.Interface.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Inlog.Frota.DAL.Repositories
{
    public class RepositoryBase<TEntity> : IDisposable, IRepositoryBase<TEntity> where TEntity : class
    {
        protected FrotaContext Db = new FrotaContext();

        public bool Add(TEntity obj)
        {
            try
            {
                Db.Set<TEntity>().Add(obj);
                Db.SaveChanges();

                return true;

            }
            catch (Exception)
            {

                return false;
            }
          
        }

        public TEntity GetById(int id)
        {
            return Db.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Db.Set<TEntity>().ToList();
        }

        public bool Update(TEntity obj)
        {

            try
            {

                Db.Entry(obj).State = EntityState.Modified;
                Db.SaveChanges();

                return true;

            }
            catch (Exception)
            {

                return false;
            }

            
        }

        public bool Remove(TEntity obj)
        {

            try
            {
                Db.Set<TEntity>().Remove(obj);
                Db.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
               
            }

          
        }

        public void Dispose()
        {

            Db.Dispose();
        }
    }
}
