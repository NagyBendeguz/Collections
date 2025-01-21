using Collections.Entities.Helpers;

namespace Collections.Data
{
    public class Repository<T> where T : class, IIdEntity
    {
        CollectionsContext ctx;

        public Repository(CollectionsContext ctx)
        {
            this.ctx = ctx;
        }

        public IQueryable<T> GetAll()
        {
            return ctx.Set<T>();
        }

        public T GetById(string id)
        {
            return ctx.Set<T>().First(t => t.Id == id);
        }

        public void Create(T entity)
        {
            ctx.Set<T>().Add(entity);
            ctx.SaveChanges();
        }

        public void Update(T entity)
        {
            ctx.Set<T>().Update(entity);
            ctx.SaveChanges();
        }

        public void Delete(T entity)
        {
            ctx.Set<T>().Remove(entity);
            ctx.SaveChanges();
        }

        public void DeleteById(string id)
        {
            var entity = GetById(id);
            Delete(entity);
        }
    }
}
