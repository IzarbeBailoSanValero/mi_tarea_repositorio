
/*
Define qué operaciones puede hacer un repositorio, sin decir cómo se hacen. CRUD
Si mañana quieres cambiar de base de datos, solo cambias la clase Repository, sin tocar el controlador.
*/


using Models;

namespace RestauranteAPI.Repositories
{
    public interface IBebidaRepository 
    {
        Task<List<Bebida>> GetAllAsync();
        Task<Bebida?> GetByIdAsync(int id);
        Task AddAsync(Bebida bebida);
        Task UpdateAsync(Bebida bebida);
        Task DeleteAsync(int id);
        Task InicializarDatosAsync();
    }
}
