using System.Collections.Generic;
using System.Threading.Tasks;
using MarcasAutos.Domain.Entities;

namespace MarcasAutos.Domain.Repositories;

public interface IMarcaAutoRepository
{
    Task<IEnumerable<MarcaAuto>> GetAllAsync();
}
