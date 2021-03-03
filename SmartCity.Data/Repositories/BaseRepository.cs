using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SmartCity.Data.Entities;

namespace SmartCity.Data.Repositories
{
    public abstract class BaseRepository<Model> where Model : BaseModel
    {
        protected SmartCityDbContext context;
        protected DbSet<Model> dbSet;

        public BaseRepository(SmartCityDbContext context)
        {
            this.context = context;
            dbSet = context.Set<Model>();
        }

        public Model Get(long id)
        {
            return dbSet.SingleOrDefault(x => x.Id == id);
        }

        public List<Model> GetAll()
        {
            return dbSet.ToList();
        }

        public void Save(Model model)
        {
            if (model.Id > 0) {
                dbSet.Update(model);
                context.SaveChanges();
                return;
            }

            dbSet.Add(model);
            context.SaveChanges();
        }

        public void Delete(long id)
        {
            var model = Get(id);
            dbSet.Remove(model);
            context.SaveChanges();
        }
    }
}
