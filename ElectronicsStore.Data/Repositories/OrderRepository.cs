using ElectronicsStore.Data.Entities;
using ElectronicsStore.Data.Interfaces;
using ElectronicsStore.Data.Queries;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Diagnostics.CodeAnalysis;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ElectronicsStore.Data.Repositories;

public class OrderRepository : IOrderRepository
{
	private readonly ElectronicsStoreDbContext _context;

	public OrderRepository(ElectronicsStoreDbContext context)
	{
		_context = context;
	}

	public async Task AddAsync(Order order)
	{
		await _context.Orders.AddAsync(order);
	}

    public async Task<IEnumerable<Order>> GetAllAsync(OrderQuery query)
    {
        var baseQuery = WhereNotAccepted(_context.Orders, query.ShowNotAccepted);
        baseQuery = SearchPhrase(baseQuery, query.SearchPhrase);
        baseQuery = Paginate(baseQuery, query.PageSize, query.PageNumber);
        baseQuery = IncludeUserAndOrderProducts(baseQuery);
        return await baseQuery.ToListAsync();
    }

    public async Task<IEnumerable<Order>> GetByUserIdAsync(OrderQuery query, int userId)
    {
        var baseQuery = _context.Orders.Where(o => o.UserId == userId);
        baseQuery = WhereNotAccepted(baseQuery, query.ShowNotAccepted);
        baseQuery = Paginate(baseQuery, query.PageSize, query.PageNumber);
        baseQuery = IncludeUserAndOrderProducts(baseQuery);
        return await baseQuery.ToListAsync();
    }

    public async Task<Order?> GetByIdAsync(int id)
    {
		return await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<int> CountAsync(OrderQuery query)
    {
        var baseQuery = WhereNotAccepted(_context.Orders, query.ShowNotAccepted);
        baseQuery = SearchPhrase(baseQuery, query.SearchPhrase);
        return await baseQuery.CountAsync();
    }

    public async Task<int> CountByUserIdAsync(OrderQuery query, int userId)
    {
        var baseQuery = WhereNotAccepted(_context.Orders, query.ShowNotAccepted);
        return await baseQuery.CountAsync(o => o.UserId == userId);
    }

    public void Update(Order order)
    {
        _context.Orders.Update(order);
    }

    public void Remove(Order order)
    {
        _context.Orders.Remove(order);
    }

    private IQueryable<Order> IncludeUserAndOrderProducts(IQueryable<Order> query)
    {
        return query
            .Include(o => o.User)
            .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
                .ThenInclude(op => op.Brand)
            .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
                .ThenInclude(op => op.Category);
    }

    private IQueryable<Order> SearchPhrase(IQueryable<Order> baseQuery, string? searchPhrase)
    {
        return baseQuery
            .Where(o => searchPhrase == null
                || o.User.Email.ToLower().Contains(searchPhrase.ToLower()));
    }

    private IQueryable<Order> Paginate(IQueryable<Order> query, int pageSize, int pageNumber)
    {
        return query
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize);
    }

    private IQueryable<Order> WhereNotAccepted(IQueryable<Order> query, bool showNotAccepted)
    {
        return showNotAccepted
            ? query.Where(o => o.TimeAccepted == null)
            : query;
    }
}
