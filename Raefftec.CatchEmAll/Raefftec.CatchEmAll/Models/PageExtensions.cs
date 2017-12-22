using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Raefftec.CatchEmAll.Models
{
    internal static class PageExtensions
    {/*
        internal static async Task<Page<TDestination>> ToPageAsync<TSource, TDestination>(this IQueryable<TSource> query, int currentPage, int pageSize, Func<TSource, TDestination> mapping)
        {
            return new Page<TDestination>
            {
                TotalItemCount = await query.CountAsync(),
                CurrentPage = currentPage,
                PageSize = pageSize,
                Items = (await query.Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync()).Select(mapping).ToList()
            };
        }*/

        internal static async Task<Page<TSource>> ToPageAsync<TSource>(this IQueryable<TSource> query, int? currentPage = null, int? pageSize = null)
        {
            var page = currentPage ?? 1;
            var size = Math.Min(pageSize ?? 10, 100);

            return new Page<TSource>
            {
                TotalItemCount = await query.CountAsync(),
                CurrentPage = page,
                PageSize = size,
                Items = await query.Skip((page - 1) * size).Take(size).ToListAsync()
            };
        }
        /*
        internal static Page<TDestination> ToPage<TSource, TDestination>(this IQueryable<TSource> query, int currentPage, int pageSize, Func<TSource, TDestination> mapping)
        {
            return new Page<TDestination>
            {
                TotalItemCount = query.Count(),
                CurrentPage = currentPage,
                PageSize = pageSize,
                Items = query.Skip((currentPage - 1) * pageSize).Take(pageSize).Select(mapping).ToList()
            };
        }*/
    }
}