using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using WebAppDms.Models;

namespace WebAppDms.Controllers
{
    public class DBHelper<T> where T : class
    {
        webDmsEntities db = new webDmsEntities();

        /// <summary>
        /// 新增一个实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Add(T entity)
        {
            db.Entry<T>(entity).State = EntityState.Added;
            return db.SaveChanges();
        }
        /// <summary>
        /// 删除一个实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Remove(T entity)
        {
            db.Entry<T>(entity).State = EntityState.Deleted;
            return db.SaveChanges();
        }
        /// <summary>
        /// 修改一个实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Update(T entity)
        {
            db.Entry<T>(entity).State = EntityState.Modified;
            return db.SaveChanges();
        }
        /// <summary>
        /// 批量新增实体
        /// </summary>
        /// <param name="dbdb"></param>
        /// <returns></returns>
        public int AddList(params T[] entities)
        {
            int result = 0;
            for (int i = 0; i < entities.Count(); i++)
            {
                if (entities[i] == null)
                    continue;
                db.Entry<T>(entities[i]).State = EntityState.Added;
                if (i != 0 && i % 20 == 0)
                {
                    result += db.SaveChanges();
                }
            }
            if (entities.Count() > 0)
                result += db.SaveChanges();
            return result;
        }
        /// <summary>
        /// 批量删除实体
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public int RemoveList(Expression<Func<T, bool>> where)
        {
            var temp = db.Set<T>().Where(where);
            foreach (var item in temp)
            {
                db.Entry<T>(item).State = EntityState.Deleted;
            }
            return db.SaveChanges();
        }
        /// <summary>
        /// 按条件查询
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public IQueryable<T> FindList(Expression<Func<T, bool>> where)
        {
            var temp = db.Set<T>().Where(where);
            return temp;
        }
        /// <summary>
        /// 按条件查询，排序
        /// </summary>
        /// <typeparam name="S"><peparam>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public IQueryable<T> FindList<S>(Expression<Func<T, bool>> where, Expression<Func<T, S>> orderBy, bool isAsc)
        {

            var list = db.Set<T>().Where(where);
            if (isAsc)
                list = list.OrderBy<T, S>(orderBy);
            else
                list = list.OrderByDescending<T, S>(orderBy);
            return list;
        }
        /// <summary>
        /// 按条件查询，分页
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="rowCount"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public IQueryable<T> FindPagedList(int pageIndex, int pageSize, out int rowCount, Expression<Func<T, bool>> where)
        {
            var list = db.Set<T>().Where(where);
            rowCount = list.Count();
            list = list.Skip(pageSize * (pageIndex - 1)).Take(pageSize);
            return list;
        }
        /// <summary>
        /// 按条件查询，分页，排序
        /// </summary>
        /// <typeparam name="S"><peparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="rowCount"></param>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public IQueryable<T> FindPagedList<S>(int pageIndex, int pageSize, out int rowCount, Expression<Func<T, bool>> where, Expression<Func<T, S>> orderBy, bool isAsc)
        {
            var list = db.Set<T>().Where(where);
            rowCount = list.Count();
            if (isAsc)
                list = list.OrderBy<T, S>(orderBy).Skip(pageSize * (pageIndex - 1)).Take(pageSize);
            else
                list = list.OrderByDescending<T, S>(orderBy).Skip(pageSize * (pageIndex - 1)).Take(pageSize);
            return list;
        }

    }
}