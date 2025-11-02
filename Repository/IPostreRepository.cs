
/*
Define qué operaciones puede hacer un repositorio, sin decir cómo se hacen. CRUD
Si mañana quieres cambiar de base de datos, solo cambias la clase Repository, sin tocar el controlador.
*/


using Models;

namespace RestauranteAPI.Repositories
{
    public interface IPostreRepository 
    {
        Task<List<Postre>> GetAllAsync();
        Task<Postre?> GetByIdAsync(int id);
        Task AddAsync(Postre postre);
        Task UpdateAsync(Postre postre);
        Task DeleteAsync(int id);
        Task InicializarDatosAsync();
    }
}
