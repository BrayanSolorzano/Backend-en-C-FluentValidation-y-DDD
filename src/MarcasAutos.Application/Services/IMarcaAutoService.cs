using System.Collections.Generic;
using System.Threading.Tasks;
using MarcasAutos.Application.DTOs;

namespace MarcasAutos.Application.Services;

public interface IMarcaAutoService
{
    Task<IEnumerable<MarcaAutoDto>> GetAllMarcasAsync();
}
