/*
Implemente IRepository usando la conexión concreta a la base de datos. 
REPOSITORY = CAPA DE ACCESO A DATOS IMPLEMENTADA --> LÓGICA CRUD DE ACCESO A DATOS USANDO LA CONNECTIONSTRING
*/


using Microsoft.Data.SqlClient;

namespace RestauranteAPI.Repositories
{
    public class PlatoPrincipalRepository : IPlatoPrincipalRepository
    {
        private readonly string _connectionString;

        public PlatoPrincipalRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<PlatoPrincipal>> GetAllAsync()                                       //primera tarea de la interfaz
        {
            var platosPrincipales = new List<PlatoPrincipal>();                                     //defino respuesta

            using (var connection = new SqlConnection(_connectionString))                           //creamos conexión a bbdd
                                                                                                            ////using solo se ejecuta en sus corchetes. sirve para que las conexiones entren y salgan al teminal el contexto
                                                                                                            // //new sqlconnection: mira las credenciales bbdd --> configura la conexión
            {
                await connection.OpenAsync();                                                        //la abrimos

                string query = "SELECT Id, Nombre, Precio, Ingredientes FROM PlatoPrincipal";        //sql --> consullta sql a ejecutar
                using (var command = new SqlCommand(query, connection))                              //configuramos el comando: consulta sql a nuestra bbdd
                {
                    using (var reader = await command.ExecuteReaderAsync())                         //ejecuta la query y lee el resultado
                    {
                        while (await reader.ReadAsync())                                            ////va a air creando cada obejto platoPrincipal de la lista con el nombre de plato, mientras encuentre objetos para leer, numeros por columnas
                        {
                            var plato = new PlatoPrincipal                  //primero se llama a un constructor vacío y luego se mete los valores, osea que tiene que existir el constructor vacío
                            {
                                Id = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Precio = (double)reader.GetDecimal(2),
                                Ingredientes = reader.GetString(3)
                            };

                            platosPrincipales.Add(plato);
                        }
                    }
                }
            }
            return platosPrincipales;
        }                                                                                           //////como hemos usado el using no sdesprecupamos de cerrar la conexión

        public async Task<PlatoPrincipal> GetByIdAsync(int id)
        {
            PlatoPrincipal platoPrincipal = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Nombre, Precio, Ingredientes FROM PlatoPrincipal WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            platoPrincipal = new PlatoPrincipal
                            {
                                Id = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Precio = reader.GetDouble(2),
                                Ingredientes = reader.GetString(3)
                            };
                        }
                    }
                }
            }
            return platoPrincipal;
        }

        public async Task AddAsync(PlatoPrincipal platoPrincipal)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO PlatoPrincipal (Nombre, Precio, Ingredientes) VALUES (@Nombre, @Precio, @Ingredientes)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", platoPrincipal.Nombre);
                    command.Parameters.AddWithValue("@Precio", platoPrincipal.Precio);
                    command.Parameters.AddWithValue("@Ingredientes", platoPrincipal.Ingredientes);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(PlatoPrincipal platoPrincipal)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE PlatoPrincipal SET Nombre = @Nombre, Precio = @Precio, Ingredientes = @Ingredientes WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", platoPrincipal.Id);
                    command.Parameters.AddWithValue("@Nombre", platoPrincipal.Nombre);
                    command.Parameters.AddWithValue("@Precio", platoPrincipal.Precio);
                    command.Parameters.AddWithValue("@Ingredientes", platoPrincipal.Ingredientes);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM PlatoPrincipal WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task InicializarDatosAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // Comando SQL para insertar datos iniciales
                var query = @"                                                              ////el @ antes de la query no e sparamtrización. --> Permite que escribas strings en varias líneas y que no tengas que escapar caracteres especiales como \.
                    INSERT INTO PlatoPrincipal (Nombre, Precio, Ingredientes)
                    VALUES 
                    (@Nombre1, @Precio1, @Ingredientes1),
                    (@Nombre2, @Precio2, @Ingredientes2)";

                using (var command = new SqlCommand(query, connection))
                {
                    // Parámetros para el primer plato
                    command.Parameters.AddWithValue("@Nombre1", "Plato combinado");
                    command.Parameters.AddWithValue("@Precio1", 12.50);
                    command.Parameters.AddWithValue("@Ingredientes1", "Pollo, patatas, tomate");

                    // Parámetros para el segundo plato
                    command.Parameters.AddWithValue("@Nombre2", "Plato vegetariano");
                    command.Parameters.AddWithValue("@Precio2", 10.00);
                    command.Parameters.AddWithValue("@Ingredientes2", "Tofu, verduras, arroz");

                    await command.ExecuteNonQueryAsync();
                }
            }
        }


    }

}