//EN ESTE CASO (SIN CAPA SERVICES) EL CONTROLADOR: GESTIONA PETICIONES HTTP + CONTIENE LOGICA DE NEGOCIO + LLAMA A REPOSITORIO PARA ACCEDER A LOS DATOS

using Microsoft.AspNetCore.Mvc;             ////// Importa el espacio de nombres para trabajar con controladores MVC (ControllerBase)
using RestauranteAPI.Repositories;

namespace RestauranteAPI.Controllers
{
    // DEFINICIÓN DEL CONTROLADOR
    [ApiController]                          //// Indica que esta clase es un controlador para API, habilitando características automáticas.  
    [Route("api/[controller]")]             ////La ruta será el nombre del controlador (sin "Controller").  [controller], es un placeholder (un marcador de posición) que se sustituye automáticamente por el nombre del controlador sin la palabra “Controller”.



    public class PostreController : ControllerBase
    {
        private static List<Postre> postre = new List<Postre>(); //// variable estatica usada para almacenar todos los postrePrincipales

        private readonly IPostreRepository _repository;                  //declaración de la variable privada que implementa la interfaz ////readonly significa que solo se puede asignar en el constructor


        public PostreController(IPostreRepository repository)   //constructor del controller.  
        {                                                                      ////si dudas mirar apuntes.txt
            _repository = repository;
        }




        //peticiones
        [HttpGet]
        public async Task<ActionResult<List<Postre>>> GetPostres()
        {
            var postre = await _repository.GetAllAsync();
            return Ok(postre);
        }


        
        [HttpGet("{id}")]
        public async Task<ActionResult<Postre>> GetPostre(int id)
        {
            var postre = await _repository.GetByIdAsync(id);
            if (postre == null)
            {
                return NotFound();
            }
            return Ok(postre);
        }

        [HttpPost]
        public async Task<ActionResult<Postre>> CreatePostre(Postre postre)
        {
            await _repository.AddAsync(postre);
            return CreatedAtAction(nameof(GetPostre), new { id = postre.Id }, postre);
        }



        

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePostre(int id, Postre updatedPostre)
        {
            var existingPostre = await _repository.GetByIdAsync(id);
            if (existingPostre == null)
            {
                return NotFound();
            }

            // Actualizar el Postre existente
            existingPostre.Nombre = updatedPostre.Nombre;
            existingPostre.Precio = updatedPostre.Precio;
            existingPostre.Calorias = updatedPostre.Calorias;

            await _repository.UpdateAsync(existingPostre);
            return NoContent();
        }






        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePostre(int id)
        {
            var postre = await _repository.GetByIdAsync(id);
            if (postre == null)
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