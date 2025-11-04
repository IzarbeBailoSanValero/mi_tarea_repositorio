
//VERSIÓN DE CLARA PARA RESOLVER COMBO (VISTA CON ALEJANDRO 4 NOV)
//clara no ha guardado en base de datos los combos. Por lo que no se puede hacer le crud de base de datos. Ella crea un combo con todas las combinaciones posibles. Solo hace el metodo getall

/*
using Microsoft.Data.SqlClient;

namespace RestauranteAPI.Repositories
{

    public class ComboRepository : IComboRepository              //inyecto Las interfaces y repositories de los otros objetos porque los voy a usar en el constructor (modelo clara 4nov clase ok) --> tengo que modificar con iconfiguration el porgram.cs para el combo repository
    {                                                           
        private readonly string _connectionString;
        private readonly IPlatoPrincipalRepository _platoPrincipalRepository;
        private readonly IBebidaRepository _bebidaRepository;
        private readonly IPostreRepository _postreRepository;
        




        public ComboRepository(string connectionString, IPlatoPrincipalRepository platoPrincipalRepository, IBebidaRepository bebidaRepository, IPostreRepository postreRepository)
        {
            _connectionString = connectionString;
            _platoPrincipalRepository = platoPrincipalRepository;
            _bebidaRepository = bebidaRepository;
            _postreRepository = postreRepository;

        }



        public async Task<List<Combo>> GetAllAsync()                       //hace un get all de todas las combinaciones. Dice alex que en algun momento se tinene que unir y que la opcion menos mala es llamar alqui al repsto de repositories
        {
            var allPostres = await _postreRepository.GetAllAsync();
            var allBebidas = await _bebidaRepository.GetAllAsync();
            var allPlatosPrincipales = await _platoPrincipalRepository.GetAllAsync();

            var allCombos = new List<Combo>();

            //descuento fijo: 
            const double descuentoFijo = 0.10;

            //generar todos los combos
            foreach (var platoPrincipal in allPlatosPrincipales)
            {
                foreach (var bebida in allBebidas)
                {
                    foreach (var postre in allPostres)
                    {
                        var nuevoCombo = new Combo(platoPrincipal, bebida, postre, descuentoFijo);
                        allCombos.Add(nuevoCombo);
                    }
                }
                
            }
            return allCombos;
        }                                                                                         




        public async Task<Combo> GetByIdAsync(int id)
        {
           
        }

        public async Task AddAsync(Combo combo)
        {
            
        }

        public async Task UpdateAsync(Combo combo)
        {
            
        }

        public async Task DeleteAsync(int id)
        {
           
        }

        public async Task InicializarDatosAsync()
        {
          
        }


    }
}

*/
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



//VERSIÓN DE JAIME (VISTA POR ALEJANDRO 4 NOV)
//ÉL SI QUE GUARDA LA INFO EN BASE DE DATOS. ES LA MEJOR VERSIÓN.

using Microsoft.Data.SqlClient;

namespace RestauranteAPI.Repositories
{

    public class ComboRepository : IComboRepository           
    {                                                           
        private readonly string _connectionString;
        private readonly IPlatoPrincipalRepository _platoPrincipalRepository;
        private readonly IBebidaRepository _bebidaRepository;
        private readonly IPostreRepository _postreRepository;
        




        public ComboRepository(string connectionString, IPlatoPrincipalRepository platoPrincipalRepository, IBebidaRepository bebidaRepository, IPostreRepository postreRepository)
        {
            _connectionString = connectionString;
            _platoPrincipalRepository = platoPrincipalRepository;
            _bebidaRepository = bebidaRepository;
            _postreRepository = postreRepository;

        }







