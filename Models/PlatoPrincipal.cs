using System.Diagnostics;
using Models;

class InvalidIngredientesException: Exception {
 public InvalidIngredientesException(string message = ""):base(message) {

 }
}

public class PlatoPrincipal : Producto {

    public string Ingredientes {get;set;} = "";

public PlatoPrincipal() {} //parameterless constructor

public PlatoPrincipal(string nombre, double precio, string ingredientes): base(nombre, precio) {
   Ingredientes = ingredientes;
   if (string.IsNullOrEmpty(ingredientes)) {
    throw new InvalidIngredientesException("Los ingredientes no pueden estar vacíos");
   }
}

    public override void MostrarDetalles() {
        Console.WriteLine($"Plato principal: {Nombre}, Precio {Precio:C}, Ingredientes {Ingredientes} ");
    }

/*
    public override string MostrarDetallesGuardado() {
        var message = $"PlatoPrincipal|{Nombre}|{Precio}|{Ingredientes}";
        return message;

    }
*/
}