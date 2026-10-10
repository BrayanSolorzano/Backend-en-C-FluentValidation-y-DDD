using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MarcasAutos.Domain.Entities;
using MarcasAutos.Domain.Repositories;
using MarcasAutos.Infrastructure.Data;

namespace MarcasAutos.Infrastructure.Repositories;

public class MarcaAutoRepository : IMarcaAutoRepository
{
    private readonly AppDbContext _context;

    public MarcaAutoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MarcaAuto>> GetAllAsync()
    {
        return await _context.MarcasAutos.ToListAsync();
    }
}
