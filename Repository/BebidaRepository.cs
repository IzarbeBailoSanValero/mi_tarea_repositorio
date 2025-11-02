/*
Implemente IRepository usando la conexión concreta a la base de datos. 
REPOSITORY = CAPA DE ACCESO A DATOS IMPLEMENTADA --> LÓGICA CRUD DE ACCESO A DATOS USANDO LA CONNECTIONSTRING
*/


using Microsoft.Data.SqlClient;

namespace RestauranteAPI.Repositories
{

    public class BebidaRepository : IBebidaRepository
    {
        private readonly string _connectionString;




        public BebidaRepository(string connectionString)
        {
            _connectionString = connectionString;
        }



        public async Task<List<Bebida>> GetAllAsync()                                       //primera tarea de la interfaz
        {
            var bebidas = new List<Bebida>();                                     //defino respuesta

            using (var connection = new SqlConnection(_connectionString))                           //creamos conexión a bbdd
                                                                                                    ////using solo se ejecuta en sus corchetes. sirve para que las conexiones entren y salgan al teminal el contexto
                                                                                                    // //new sqlconnection: mira las credenciales bbdd --> configura la conexión
            {
                await connection.OpenAsync();                                                        //la abrimos

                string query = "SELECT Id, Nombre, Precio, EsAlcoholica FROM Bebida";        //sql --> consullta sql a ejecutar

                using (var command = new SqlCommand(query, connection))                              //configuramos el comando: consulta sql a nuestra bbdd
                {
                    using (var reader = await command.ExecuteReaderAsync())                         //ejecuta la query y lee el resultado
                    {
                        while (await reader.ReadAsync())                                            ////va a air creando cada obejtoBebida de la lista con el nombre de bebida, mientras encuentre objetos para leer, numeros por columnas
                        {
                            var bebida = new Bebida                                     //primero se llama a un constructor vacío y luego se mete los valores, osea que tiene que existir el constructor vacío
                            {
                                Id = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Precio = (double)reader.GetDecimal(2),
                                EsAlcoholica = reader.GetBoolean(3)
                            }
                            ;

                            bebidas.Add(bebida);
                        }
                    }
                }
            }
            return bebidas;
        }                                                                                           //////como hemos usado el using no sdesprecupamos de cerrar la conexión




        public async Task<Bebida> GetByIdAsync(int id)
        {
            Bebida bebida = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Nombre, Precio, EsAlcoholica FROM Bebida WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            bebida = new Bebida
                            {
                                Id = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Precio = reader.GetDouble(2),
                                EsAlcoholica = reader.GetBoolean(3)
                            };
                        }
                    }
                }
            }
            return bebida;
        }

        public async Task AddAsync(Bebida bebida)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Bebida (Nombre, Precio, EsAlcoholica) VALUES (@Nombre, @Precio, @EsAlcoholica)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", bebida.Nombre);
                    command.Parameters.AddWithValue("@Precio", bebida.Precio);
                    command.Parameters.AddWithValue("@EsAlcoholica", bebida.EsAlcoholica);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Bebida bebida)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Bebida SET Nombre = @Nombre, Precio = @Precio, EsAlcoholica = @EsAlcoholica WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", bebida.Id);
                    command.Parameters.AddWithValue("@Nombre", bebida.Nombre);
                    command.Parameters.AddWithValue("@Precio", bebida.Precio);
                    command.Parameters.AddWithValue("@EsAlcoholica", bebida.EsAlcoholica);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Bebida WHERE Id = @Id";
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
                    INSERT INTO Bebida (Nombre, Precio, EsAlcoholica)
                    VALUES 
                    (@Nombre1, @Precio1, @EsAlcoholica1),
                    (@Nombre2, @Precio2, @EsAlcoholica2)";

                using (var command = new SqlCommand(query, connection))
                {
                    // Parámetros para el primer bebida
                    command.Parameters.AddWithValue("@Nombre1", "Fanta Naranja");
                    command.Parameters.AddWithValue("@Precio1", 2.50);
                    command.Parameters.AddWithValue("@EsAlcoholica1", false);

                    // Parámetros para el segundo bebida
                    command.Parameters.AddWithValue("@Nombre2", "cerveza");
                    command.Parameters.AddWithValue("@Precio2", 1.00);
                    command.Parameters.AddWithValue("@EsAlcoholica2", true);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }


    }
}