/*
Implemente IRepository usando la conexión concreta a la base de datos. 
REPOSITORY = CAPA DE ACCESO A DATOS IMPLEMENTADA --> LÓGICA CRUD DE ACCESO A DATOS USANDO LA CONNECTIONSTRING
*/


using Microsoft.Data.SqlClient;

namespace RestauranteAPI.Repositories
{

    public class ComboRepository : IComboRepository
    {
        private readonly string _connectionString;




        public ComboRepository(string connectionString)
        {
            _connectionString = connectionString;
        }



        public async Task<List<Combo>> GetAllAsync()                                       //primera tarea de la interfaz
        {
            var combos = new List<Combo>();                                     //defino respuesta

            using (var connection = new SqlConnection(_connectionString))                           //creamos conexión a bbdd
                                                                                                    ////using solo se ejecuta en sus corchetes. sirve para que las conexiones entren y salgan al teminal el contexto
                                                                                                    // //new sqlconnection: mira las credenciales bbdd --> configura la conexión
            {
                await connection.OpenAsync();                                                        //la abrimos

                string query = "SELECT Id, Nombre, Precio, PlatoPrincipal, Bebida, Postre, Descuento FROM Combo";        //sql --> consullta sql a ejecutar

                using (var command = new SqlCommand(query, connection))                              //configuramos el comando: consulta sql a nuestra bbdd
                {
                    using (var reader = await command.ExecuteReaderAsync())                         //ejecuta la query y lee el resultado
                    {
                        while (await reader.ReadAsync())                                            ////va a air creando cada obejtocobo de la lista con el nombre de cobo, mientras encuentre objetos para leer, numeros por columnas
                        {
                            var combo = new Combo                                     //primero se llama a un constructor vacío y luego se mete los valores, osea que tiene que existir el constructor vacío
                            {
                                Id = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Precio = (double)reader.GetDecimal(2),
                                PlatoPrincipal
                                Bebida
                                Postre
                                Descuento
                                //TODO ///////////////PENSAR COMO HACER AQUÍ LOS CMAPOS QUE SON OBJETODS 
                            }
                            ;

                            combos.Add(combo);
                        }
                    }
                }
            }
            return combos;
        }                                                                                           //////como hemos usado el using no sdesprecupamos de cerrar la conexión




        public async Task<Combo> GetByIdAsync(int id)
        {
            Combo combo = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Nombre, Precio, Calorias FROM Combo WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            combo = new Combo
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
            return combo;
        }

        public async Task AddAsync(Combo combo)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Combo (Nombre, Precio, Calorias) VALUES (@Nombre, @Precio, @Calorias)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre",combo.Nombre);
                    command.Parameters.AddWithValue("@Precio",combo.Precio);
                    command.Parameters.AddWithValue("@Calorias",combo.Calorias);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Combo combo)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Combo SET Nombre = @Nombre, Precio = @Precio, Calorias = @Calorias WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id",combo.Id);
                    command.Parameters.AddWithValue("@Nombre",combo.Nombre);
                    command.Parameters.AddWithValue("@Precio",combo.Precio);
                    command.Parameters.AddWithValue("@Calorias",combo.Calorias);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Combo WHERE Id = @Id";
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
                    INSERT INTO Combo (Nombre, Precio, Calorias)
                    VALUES 
                    (@Nombre1, @Precio1, @Calorias1),
                    (@Nombre2, @Precio2, @Calorias2)";

                using (var command = new SqlCommand(query, connection))
                {
                    // Parámetros para el primer combo
                    command.Parameters.AddWithValue("@Nombre1", "crep chocolate");
                    command.Parameters.AddWithValue("@Precio1", 12.50);
                    command.Parameters.AddWithValue("@Calorias1", 1000);

                    // Parámetros para el segundo combo
                    command.Parameters.AddWithValue("@Nombre2", "fresas");
                    command.Parameters.AddWithValue("@Precio2", 14.00);
                    command.Parameters.AddWithValue("@Calorias2", 300);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }


    }
}