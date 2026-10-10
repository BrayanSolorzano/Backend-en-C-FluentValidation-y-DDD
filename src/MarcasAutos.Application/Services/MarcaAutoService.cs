using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarcasAutos.Application.DTOs;
using MarcasAutos.Domain.Repositories;

namespace MarcasAutos.Application.Services;

public class MarcaAutoService : IMarcaAutoService
{
    private readonly IMarcaAutoRepository _repository;

    public MarcaAutoService(IMarcaAutoRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<MarcaAutoDto>> GetAllMarcasAsync()
    {
        var marcas = await _repository.GetAllAsync();
        
        return marcas.Select(m => new MarcaAutoDto
        {
            Id = m.Id,
            Nombre = m.Nombre,
            PaisOrigen = m.PaisOrigen
        });
    }
}
