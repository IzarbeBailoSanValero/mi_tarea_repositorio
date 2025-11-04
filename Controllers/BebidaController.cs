//EN ESTE CASO (SIN CAPA SERVICES) EL CONTROLADOR: GESTIONA PETICIONES HTTP + CONTIENE LOGICA DE NEGOCIO + LLAMA A REPOSITORIO PARA ACCEDER A LOS DATOS --> expone al exterior

using Microsoft.AspNetCore.Mvc;             ////// Importa el espacio de nombres para trabajar con controladores MVC (ControllerBase)
using RestauranteAPI.Repositories;

namespace RestauranteAPI.Controllers
{
    // DEFINICIÓN DEL CONTROLADOR
    [ApiController]                          //// Indica que esta clase es un controlador para API, habilitando características automáticas.  
    [Route("api/[controller]")]             ////La ruta será el nombre del controlador (sin "Controller").  [controller], es un placeholder (un marcador de posición) que se sustituye automáticamente por el nombre del controlador sin la palabra “Controller”.



    public class BebidaController : ControllerBase
    {
        private static List<Bebida> bebidas = new List<Bebida>(); //// variable estatica usada para almacenar todos los bebidasPrincipales

        private readonly IBebidaRepository _repository;                  //declaración de la variable privada que implementa la interfaz ////readonly significa que solo se puede asignar en el constructor


        public BebidaController(IBebidaRepository repository)   //constructor del controller.  
        {                                                                      ////si dudas mirar apuntes.txt
            _repository = repository;
        }




        //peticiones
        [HttpGet]
        public async Task<ActionResult<List<Bebida>>> GetBebidas()                    //task es para que sea asíncrona
        {
            var bebidas = await _repository.GetAllAsync();
            return Ok(bebidas);
        }


        
        [HttpGet("{id}")]
        public async Task<ActionResult<Bebida>> GetBebida(int id)
        {
            var bebida = await _repository.GetByIdAsync(id);
            if (bebida == null)
            {
                return NotFound();
            }
            return Ok(bebida);
        }

        [HttpPost]
        public async Task<ActionResult<Bebida>> Createbebida(Bebida bebida)
        {
            await _repository.AddAsync(bebida);
            return CreatedAtAction(nameof(GetBebida), new { id = bebida.Id }, bebida);
        }



        

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBebida(int id, Bebida updatedBebida)
        {
            var existingBebida = await _repository.GetByIdAsync(id);
            if (existingBebida == null)
            {
                return NotFound();
            }

            // Actualizar el Bebida existente
            existingBebida.Nombre = updatedBebida.Nombre;
            existingBebida.Precio = updatedBebida.Precio;
            existingBebida.EsAlcoholica = updatedBebida.EsAlcoholica;

            await _repository.UpdateAsync(existingBebida);
            return NoContent();
        }






        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBebida(int id)
        {
            var bebida = await _repository.GetByIdAsync(id);
            if (bebida == null)
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