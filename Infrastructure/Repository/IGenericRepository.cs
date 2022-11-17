using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Application.Common.Model;
using Domain.Entities;

namespace infrastructure.Repository
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        T GetById(int id);
        IEnumerable<T> GetAll();
        PagingDto<T> GetWithPaging(
        Expression<Func<T, bool>> expression = null,
        PagingDto<T> pagingParameter = null);

        PagingDto<T> GetWithOrder(
        Expression<Func<T, bool>> expression = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        PagingDto<T> pagingParameter = null);

        IEnumerable<T> Find(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes);
        bool Exist(Expression<Func<T, bool>> expression);
        IEnumerable<T> FindNoTrack(Expression<Func<T, bool>> expression);
        void Add(T entity);
        int Count();
        int Count(Expression<Func<T, bool>> expression);
        void AddRange(IEnumerable<T> entities);
        int Remove(T entity);
        int RemoveRange(IEnumerable<T> entities);
        int SaveEntity();
    }
}
