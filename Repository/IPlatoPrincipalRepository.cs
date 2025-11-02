
/*
Define qué operaciones puede hacer un repositorio, sin decir cómo se hacen. CRUD
Si mañana quieres cambiar de base de datos, solo cambias la clase Repository, sin tocar el controlador.
*/


using Models;

namespace RestauranteAPI.Repositories
{
    public interface IPlatoPrincipalRepository 
    {
        Task<List<PlatoPrincipal>> GetAllAsync();
        Task<PlatoPrincipal?> GetByIdAsync(int id);
        Task AddAsync(PlatoPrincipal plato);
        Task UpdateAsync(PlatoPrincipal plato);
        Task DeleteAsync(int id);
        Task InicializarDatosAsync();
    }
}
