/*
Implemente IRepository usando la conexión concreta a la base de datos. 
REPOSITORY = CAPA DE ACCESO A DATOS IMPLEMENTADA --> LÓGICA CRUD DE ACCESO A DATOS USANDO LA CONNECTIONSTRING
*/


using Microsoft.Data.SqlClient;

namespace RestauranteAPI.Repositories
{

    public class PostreRepository : IPostreRepository
    {
        private readonly string _connectionString;




        public PostreRepository(string connectionString)
        {
            _connectionString = connectionString;
        }



        public async Task<List<Postre>> GetAllAsync()                                       //primera tarea de la interfaz
        {
            var postres = new List<Postre>();                                     //defino respuesta

            using (var connection = new SqlConnection(_connectionString))                           //creamos conexión a bbdd
                                                                                                    ////using solo se ejecuta en sus corchetes. sirve para que las conexiones entren y salgan al teminal el contexto
                                                                                                    // //new sqlconnection: mira las credenciales bbdd --> configura la conexión
            {
                await connection.OpenAsync();                                                        //la abrimos

                string query = "SELECT Id, Nombre, Precio, Calorias FROM Postre";        //sql --> consullta sql a ejecutar

                using (var command = new SqlCommand(query, connection))                              //configuramos el comando: consulta sql a nuestra bbdd
                {
                    using (var reader = await command.ExecuteReaderAsync())                         //ejecuta la query y lee el resultado
                    {
                        while (await reader.ReadAsync())                                            ////va a air creando cada obejtopostre de la lista con el nombre de postre, mientras encuentre objetos para leer, numeros por columnas
                        {
                            var postre = new Postre                                     //primero se llama a un constructor vacío y luego se mete los valores, osea que tiene que existir el constructor vacío
                            {
                                Id = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Precio = (double)reader.GetDecimal(2),
                                Calorias = reader.GetInt32(3)
                            }
                            ;

                            postres.Add(postre);
                        }
                    }
                }
            }
            return postres;
        }                                                                                           //////como hemos usado el using no sdesprecupamos de cerrar la conexión




        public async Task<Postre> GetByIdAsync(int id)
        {
            Postre postre = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Nombre, Precio, Calorias FROM Postre WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            postre = new Postre
                            {
                                Id = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Precio = reader.GetDouble(2),
                                Calorias = reader.GetInt32(3)
                            };
                        }
                    }
                }
            }
            return postre;
        }

        public async Task AddAsync(Postre postre)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Postre (Nombre, Precio, Calorias) VALUES (@Nombre, @Precio, @Calorias)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre",postre.Nombre);
                    command.Parameters.AddWithValue("@Precio",postre.Precio);
                    command.Parameters.AddWithValue("@Calorias",postre.Calorias);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Postre postre)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Postre SET Nombre = @Nombre, Precio = @Precio, Calorias = @Calorias WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id",postre.Id);
                    command.Parameters.AddWithValue("@Nombre",postre.Nombre);
                    command.Parameters.AddWithValue("@Precio",postre.Precio);
                    command.Parameters.AddWithValue("@Calorias",postre.Calorias);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Postre WHERE Id = @Id";
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
                    INSERT INTO Postre (Nombre, Precio, Calorias)
                    VALUES 
                    (@Nombre1, @Precio1, @Calorias1),
                    (@Nombre2, @Precio2, @Calorias2)";

                using (var command = new SqlCommand(query, connection))
                {
                    // Parámetros para el primer postre
                    command.Parameters.AddWithValue("@Nombre1", "crep chocolate");
                    command.Parameters.AddWithValue("@Precio1", 12.50);
                    command.Parameters.AddWithValue("@Calorias1", 1000);

                    // Parámetros para el segundo postre
                    command.Parameters.AddWithValue("@Nombre2", "fresas");
                    command.Parameters.AddWithValue("@Precio2", 14.00);
                    command.Parameters.AddWithValue("@Calorias2", 300);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }


    }
}