using BlogSystem.IDAL;
using BlogSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.DAL
{
    public class BaseService<T> : IBaseService<T> where T : BaseEntity,new()
    {
        public BlogContext db { get; set; }
        public BaseService(BlogContext db)
        {
            this.db = db;   
            
        }

        public async Task CreateAsync(T model, bool saved = true)
        {
            db.Set<T>().Add(model);
            if (saved)
            {
                await db.SaveChangesAsync();
            }
        }

        public async Task EditAsync(T model, bool saved = true)
        {
            //关闭校验
            db.Configuration.ValidateOnSaveEnabled = false;
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            if (saved)
            {
                await db.SaveChangesAsync();
                //改完后再启用
                db.Configuration.ValidateOnSaveEnabled = true;
            }


        }

        public async Task<T> FindAsync(Guid id)
        {
            return await GetAllAsync().FirstAsync(a=>a.Id == id);
        }
        /// <summary>
        /// 返回没有被删除的数据（并没有真的执行Sql语句）
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetAllAsync()
        {
            return db.Set<T>().Where(a=>!a.IsRemoved).AsNoTracking();
        }

        public IQueryable<T> GetAllByPageAsync(int pageSize = 10, int pageIndex = 0)
        {
            return GetAllAsync().Skip(pageSize * pageIndex).Take(pageSize);
        }

        public IQueryable<T> GetAllOrderAsync(bool asc = true)
        {
            var datas = GetAllAsync();
            return asc ? datas.OrderBy(a=>a.CreateTime) : datas.OrderByDescending(a=>a.CreateTime);
        }

        public IQueryable<T> GetAllByPageOrderAsync(int pageSize = 10, int pageIndex = 0, bool asc = true)
        {
            return GetAllOrderAsync(asc)
                    .Skip(pageSize * pageIndex)
                    .Take(pageSize);
        }


        public async Task RemoveAsync(Guid id, bool saved = true)
        {
            db.Configuration.ValidateOnSaveEnabled = false;
            var t = new T() { Id = id};
            db.Entry(t).State =  System.Data.Entity.EntityState.Unchanged;
            t.IsRemoved = true;
            if (saved)
            {
                await db.SaveChangesAsync();
                db.Configuration.ValidateOnSaveEnabled = true;
            }
        }

        public async Task RemoveAsync(T model, bool saved = true)
        {
            await RemoveAsync(model.Id,saved);
        }

        public async Task SaveChangesAsync()
        {
            await db.SaveChangesAsync();
            db.Configuration.ValidateOnSaveEnabled = true;
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
