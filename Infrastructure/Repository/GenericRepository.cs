using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Application.Common.Model;

namespace infrastructure.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationDbContext _context;
        private DbSet<T> entities;
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            entities = context.Set<T>();
        }
        public void Add(T entity)
        {
            entities.Add(entity);
        }
        public void AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
            _context.SaveChanges();
        }
        public IEnumerable<T> Find(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
        {
            var query = entities.Where(expression);
            //var query = GetAll().Where(expression);
            return includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }
        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }
        public T GetById(int id)
        {
            return entities.Find(id);
        }
        public int Remove(T entity)
        {
            entities.Remove(entity);
            return _context.SaveChanges();
        }
        public int RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
            return _context.SaveChanges();
        }

        public int SaveEntity()
        {
            return _context.SaveChanges();
        }

        public PagingDto<T> GetWithPaging(Expression<Func<T, bool>> expression = null,
            PagingDto<T> pagingParameter = null)
        {
            int skip = (pagingParameter.Page - 1) * pagingParameter.PageSize;
            IEnumerable<T> resultData = null;
            int totalRow = 0;
            if (expression != null)
            {
                resultData = entities.Where(expression).Skip(skip).Take(pagingParameter.PageSize);
                totalRow = entities.Where(expression).Count();
            }
            else
            {
                totalRow = entities.Count();
                resultData = entities.ToList().Skip(skip).Take(pagingParameter.PageSize);
            }
            int totalPage = (int)Math.Ceiling((double)totalRow / pagingParameter.PageSize); ;
            return new PagingDto<T>()
            {
                PageData = resultData.ToList(),
                Page = pagingParameter.Page,
                PageSize = pagingParameter.PageSize,
                TotalCount = totalRow,
                TotalPage = totalPage
            };
        }
        public PagingDto<T> GetWithOrder(Expression<Func<T, bool>> expression = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        PagingDto<T> pagingParameter = null)
        {
            IQueryable<T> query = entities;
            int skip = (pagingParameter.Page - 1) * pagingParameter.PageSize;
            IEnumerable<T> resultData = null;
            int totalRow = 0;
            if (expression != null)
            {
                query = entities.Where(expression);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            resultData = query.Skip(skip).Take(pagingParameter.PageSize);
            totalRow = query.Count();

            int totalPage = (int)Math.Ceiling((double)totalRow / pagingParameter.PageSize); ;
            return new PagingDto<T>()
            {
                PageData = resultData.ToList(),
                Page = pagingParameter.Page,
                PageSize = pagingParameter.PageSize,
                TotalCount = totalRow,
                TotalPage = totalPage
            };
        }

        public IEnumerable<T> FindNoTrack(Expression<Func<T, bool>> expression)
        {
            return entities.Where(expression).AsNoTracking();
        }

        public bool Exist(Expression<Func<T, bool>> expression)
        {
            return entities.Any(expression);
        }

        public int Count()
        {
            return entities.Count();
        }

        public int Count(Expression<Func<T, bool>> expression)
        {
            return entities.Where(expression).Count();
        }
    }
}
