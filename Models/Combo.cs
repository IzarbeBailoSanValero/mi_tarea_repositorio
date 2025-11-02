using Models;

public class Combo : Producto {

    public PlatoPrincipal PlatoPrincipal {get;set;}
    public Bebida Bebida {get;set;}
    public Postre Postre {get;set;}
    public double Descuento {get;set;} //0.10 para 10% descuento

public Combo(PlatoPrincipal platoPrincipal, Bebida bebida, Postre postre, double descuento): base("Combo especial", 0) {
   PlatoPrincipal = platoPrincipal;
   Bebida = bebida;
   Postre = postre;
   Descuento = descuento;
   Precio = CalcularPrecio();
}

 private double CalcularPrecio() {
    double precio = PlatoPrincipal.Precio + Bebida.Precio + Postre.Precio;
    double precioConDescuento =  precio * (1 - Descuento);
    return precioConDescuento;
 }

    public override void MostrarDetalles() {
         Console.WriteLine("\n-------Combo------");
         PlatoPrincipal.MostrarDetalles();
         Bebida.MostrarDetalles();
         Postre.MostrarDetalles();
         Console.WriteLine($"Descuento aplicado: {Descuento*100} %");
         Console.WriteLine($"Precio total: {Precio:C}");
        //Console.WriteLine($"Plato principal: {Nombre}, Precio {Precio:C}, Ingredientes {} ");
    }
}