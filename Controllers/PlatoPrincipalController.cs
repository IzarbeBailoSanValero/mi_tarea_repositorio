//EN ESTE CASO (SIN CAPA SERVICES) EL CONTROLADOR: GESTIONA PETICIONES HTTP + CONTIENE LOGICA DE NEGOCIO + LLAMA A REPOSITORIO PARA ACCEDER A LOS DATOS

using Microsoft.AspNetCore.Mvc;             ////// Importa el espacio de nombres para trabajar con controladores MVC (ControllerBase)
using RestauranteAPI.Repositories;

namespace RestauranteAPI.Controllers
{
    // DEFINICIÓN DEL CONTROLADOR
    [ApiController]                          //// Indica que esta clase es un controlador para API, habilitando características automáticas.  
    [Route("api/[controller]")]             ////La ruta será el nombre del controlador (sin "Controller").  [controller], es un placeholder (un marcador de posición) que se sustituye automáticamente por el nombre del controlador sin la palabra “Controller”.



    public class PlatoPrincipalController : ControllerBase
    {
        private static List<PlatoPrincipal> platos = new List<PlatoPrincipal>(); //// variable estatica usada para almacenar todos los platosPrincipales

        private readonly IPlatoPrincipalRepository _repository;                  //declaración de la variable privada que implementa la interfaz ////readonly significa que solo se puede asignar en el constructor


        public PlatoPrincipalController(IPlatoPrincipalRepository repository)   //constructor del controller.  
        {                                                                      ////si dudas mirar apuntes.txt
            _repository = repository;
        }




//peticiones
        [HttpGet]
        public async Task<ActionResult<List<PlatoPrincipal>>> GetPlatos()
        {
            var platos = await _repository.GetAllAsync();
            return Ok(platos);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<PlatoPrincipal>> GetPlato(int id)
        {
            var plato = await _repository.GetByIdAsync(id);
            if (plato == null)
            {
                return NotFound();
            }
            return Ok(plato);
        }

        [HttpPost]
        public async Task<ActionResult<PlatoPrincipal>> CreatePlato(PlatoPrincipal plato)
        {
            await _repository.AddAsync(plato);
            return CreatedAtAction(nameof(GetPlato), new { id = plato.Id }, plato);
        }



        

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlato(int id, PlatoPrincipal updatedPlato)
        {
            var existingPlato = await _repository.GetByIdAsync(id);
            if (existingPlato == null)
            {
                return NotFound();
            }

            // Actualizar el plato existente
            existingPlato.Nombre = updatedPlato.Nombre;
            existingPlato.Precio = updatedPlato.Precio;
            existingPlato.Ingredientes = updatedPlato.Ingredientes;

            await _repository.UpdateAsync(existingPlato);
            return NoContent();
        }






        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlato(int id)
        {
            var plato = await _repository.GetByIdAsync(id);
            if (plato == null)
            {
                return NotFound();
            }
            await _repository.DeleteAsync(id);
            return NoContent();
        }





        [HttpPost("inicializar")]
        public async Task<IActionResult> InicializarDatos()
        {
            await _repository.InicializarDatosAsync();
            return Ok("Datos inicializados correctamente.");
        }

    }
}