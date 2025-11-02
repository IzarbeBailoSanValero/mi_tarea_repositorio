/*
Define qué operaciones puede hacer un repositorio, sin decir cómo se hacen. CRUD
Si mañana quieres cambiar de base de datos, solo cambias la clase Repository, sin tocar el controlador.
*/


using Models;

namespace RestauranteAPI.Repositories
{
    public interface IComboRepository 
    {
        Task<List<Combo>> GetAllAsync();
        Task<Combo?> GetByIdAsync(int id);
        Task AddAsync(Combo combo);
        Task UpdateAsync(Combo combo);
        Task DeleteAsync(int id);
        Task InicializarDatosAsync();
    }
}