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
        Task AddAsync(CreateComboDTO combo);  //aquí lo ha cambiado por el DTO para no tener que meter todos los datos de todos los objetos al llamar a la función
        Task UpdateAsync(Combo combo);
        Task DeleteAsync(int id);
    }
}
