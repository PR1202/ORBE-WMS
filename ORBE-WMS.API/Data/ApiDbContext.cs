using Microsoft.EntityFrameworkCore;

namespace ORBE_WMS.API.Data;

public class ApiDbContext(DbContextOptions<ApiDbContext> options) : DbContext(options)
{
    // Adicionar DbSets das entidades conforme necessidade
}
