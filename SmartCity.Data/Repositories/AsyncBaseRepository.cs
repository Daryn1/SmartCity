using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartCity.Data.Entities;

namespace SmartCity.Data.Repositories
{
    public class AsyncBaseRepository<TModel> where TModel : BaseModel
    {
        protected SmartCityDbContext Context;
        protected DbSet<TModel> DbSet;

        public AsyncBaseRepository(SmartCityDbContext context)
        {
            Context = context;
            DbSet = context.Set<TModel>();
        }

        public virtual async Task<TModel> GetByIdAsync(long id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task<List<TModel>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public IQueryable<TModel> GetAllAsQueryable()
        {
            return DbSet.AsQueryable();
        }

        public virtual async Task SaveAsync(TModel model)
        {
            if (model.Id > 0)
            {
                DbSet.Update(model);
                await Context.SaveChangesAsync();
                return;
            }

            await DbSet.AddAsync(model);
            await Context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(long id)
        {
            var model = GetByIdAsync(id).Result;
            DbSet.Remove(model);
            await Context.SaveChangesAsync();
        }

        public virtual async Task<bool> Exists(long id)
        {
            return await DbSet.AnyAsync(m => m.Id == id);
        }
    }
}
