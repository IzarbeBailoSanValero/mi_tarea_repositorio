//codigo de jaime //jaime no guarda el precio en la base de datos. por eso no le da problemas el post etc. 

using Microsoft.AspNetCore.Mvc;
using RestauranteAPI.Repositories;

namespace RestauranteAPI.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class ComboController : ControllerBase
   {
    private static List<Combo> combos = new List<Combo>();

    private readonly IComboRepository _repository;

    public ComboController(IComboRepository repository)
        {
            _repository = repository;
        }
    
        [HttpGet]
        public async Task<ActionResult<List<Combo>>> GetCombos()
        {
            var combos = await _repository.GetAllAsync();
            return Ok(combos);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Combo>> GetCombo(int id)
        {
            var combo = await _repository.GetByIdAsync(id);
            if (combo == null)
            {
                return NotFound();
            }
            return Ok(combo);
        }

        [HttpPost] //➡️ Esto significa que el controlador recibe solo los datos necesarios para crear el combo, no un objeto Combo completo — ✅ buena práctica.
        public async Task<ActionResult<Combo>> CreateCombo(CreateComboDTO combo)
        {
            await _repository.AddAsync(combo);
            return CreatedAtAction(nameof(GetCombo), new { id = 2 }, combo);
        }



        //chatgpt utiliza el dto aquí
        /*
        [HttpPost]
public async Task<IActionResult> CreateCombo(CreateComboDTO dto)
{
    var combo = new Combo
    {
        Nombre = dto.Nombre,
        PlatoPrincipalId = dto.IdPlatoPrincipal,
        BebidaId = dto.IdBebida,
        PostreId = dto.IdPostre,
        Descuento = dto.Descuento
    };

    await _repository.AddAsync(combo);
    return Ok("Combo creado correctamente");
}
        */

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCombo(int id, Combo updatedCombo)
        {
            var existingCombo = await _repository.GetByIdAsync(id);
            if (existingCombo == null)
            {
                return NotFound();
            }

            // Actualizar el combo existente
            existingCombo.PlatoPrincipal = updatedCombo.PlatoPrincipal;
            existingCombo.Bebida = updatedCombo.Bebida;
            existingCombo.Postre = updatedCombo.Postre;
            existingCombo.Descuento = updatedCombo.Descuento;

            await _repository.UpdateAsync(existingCombo);
            return NoContent();
        }

        ///Cambio necesario///
  
       [HttpDelete("{id}")]
       public async Task<IActionResult> DeleteCombo(int id)
       {
           var combo = await _repository.GetByIdAsync(id);
           if (combo == null)
           {
               return NotFound();
           }
           await _repository.DeleteAsync(id);
           return NoContent();
       }

   }
}