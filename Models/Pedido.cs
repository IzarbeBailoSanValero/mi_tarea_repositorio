namespace Models;

class Pedido {
//private List<Producto> productos;
private List<(Producto, int)> productos; //tupla de Producto (item1) y cantidad (item2)
//Investiga las diferencias entre tupla y diccionario
public double Descuento {get;set;} = 0.0;
public double Impuesto {get;set;} = 0.21;

public DateTime FechaPedido {get; private set;}

private int ID {get; set;} = 0;
public static int Identificador {get; private set;} = 0;

public Pedido() {
    productos = new List<(Producto,int)>();
   // Descuento = 0;
   // Impuesto = 0.21;
    FechaPedido = DateTime.Now;
    Identificador++;
    ID = Identificador;
}

public void AnadirProductos(Producto producto, int cantidad = 1) {
    productos.Add((producto,cantidad));
    Console.WriteLine($"Producto añadido: {producto.Nombre}");
}

public void MostrarPedido() {
    Console.WriteLine("\n-------Pedido------");
    foreach (var producto in productos) {
        producto.Item1.MostrarDetalles();
        Console.WriteLine($"Cantidad: {producto.Item2}");
    }

    var dateFormatterstring = "dd/MM/yy HH:mm";
    Console.WriteLine($"Fecha pedido: {FechaPedido.ToString(dateFormatterstring)}");
    var fechaEntrega = FechaPedido.AddMinutes(30);
    var dateFormatterstringEntrega = "dd/MM/yyyy HH:mm:ss";
    Console.WriteLine($"Tu pedido se entregará a las : {fechaEntrega.ToString(dateFormatterstringEntrega)}");
}

public double CalcularTotal() {
    double total = 0;
    foreach (var (producto,cantidad) in productos) {
        //total += producto.Item1.Precio * producto.Item2;
        total += producto.Precio * cantidad;
    }
    double totalConDescuento = total*(1-Descuento);
    double totalConImpuesto = totalConDescuento*(1+Impuesto);
    return totalConImpuesto;
}

    public void GuardarPedido(string filePath)
    {
        using (StreamWriter sw = new StreamWriter(filePath))
        {
            foreach (var (producto,cantidad) in productos)
            {
                //Producto producto = item.Item1;
                //int cantidad = item.Item2;

                   
                if (producto is PlatoPrincipal plato)
                {
                    sw.WriteLine($"PlatoPrincipal|{plato.Nombre}|{plato.Precio}|{plato.Ingredientes}|{cantidad}");

                }
                else if (producto is Bebida bebida)
                {
                    sw.WriteLine($"Bebida|{bebida.Nombre}|{bebida.Precio}|{bebida.EsAlcoholica}|{cantidad}");
                }
                else if (producto is Postre postre)
                {
                    sw.WriteLine($"Postre|{postre.Nombre}|{postre.Precio}|{postre.Calorias}|{cantidad}");
                }  else if (producto is Combo combo)
                {
                    sw.WriteLine($"PlatoPrincipal|{combo.PlatoPrincipal.Nombre}|{combo.PlatoPrincipal.Precio}|{combo.PlatoPrincipal.Ingredientes}|{cantidad}");
                    sw.WriteLine($"Bebida|{combo.Bebida.Nombre}|{combo.Bebida.Precio}|{combo.Bebida.EsAlcoholica}|{cantidad}");
                    sw.WriteLine($"Postre|{combo.Postre.Nombre}|{combo.Postre.Precio}|{combo.Postre.Calorias}|{cantidad}");
                } 
            }
        }
    }

    public void CargarPedido(string filePath)
    {
        if (File.Exists(filePath))
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                string linea;
                while ((linea = sr.ReadLine()) != null)
                {
                    string[] datos = linea.Split('|');
                    string tipoProducto = datos[0];
                    string nombre = datos[1];
                    double precio = double.Parse(datos[2]);
                    int cantidad = int.Parse(datos[4]);

                    if (tipoProducto == "PlatoPrincipal")
                    {
                        string ingredientes = datos[3];
                        PlatoPrincipal plato = new PlatoPrincipal(nombre, precio, ingredientes);
                        AnadirProductos(plato, cantidad);
                    }
                    else if (tipoProducto == "Bebida")
                    {
                        bool esAlcoholica = bool.Parse(datos[3]);
                        Bebida bebida = new Bebida(nombre, precio, esAlcoholica);
                        AnadirProductos(bebida, cantidad);
                    }
                    else if (tipoProducto == "Postre")
                    {
                        int calorias = int.Parse(datos[3]);
                        Postre postre = new Postre(nombre, precio, calorias);
                        AnadirProductos(postre, cantidad);
                    }
                }
            }
        }
        else
        {
            Console.WriteLine("No se encontró el archivo.");
        }
    }

}