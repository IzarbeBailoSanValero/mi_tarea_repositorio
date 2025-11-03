using Models;

public class Combo : Producto {
   //ATRIBUTOS///////////////////////////////////////////////////////////////////////////////////
   public double Descuento {get;set;} //0.10 para 10% descuento

    //atributos para trabajar con la base de datos
    public int PlatoPrincipalId { get; set; }
    public int BebidaId { get; set; }
    public int PostreId { get; set; }
 

   
//atributos para trabajar en el porgrama
    public PlatoPrincipal PlatoPrincipal {get;set;}
    public Bebida Bebida {get;set;}
    public Postre Postre {get;set;}

//CONSTRUCTORES///////////////////////////////////////////////////////////////////////////////////
//constructor de objetos normales
public Combo(PlatoPrincipal platoPrincipal, Bebida bebida, Postre postre, double descuento): base("Combo especial", 0) {
   PlatoPrincipal = platoPrincipal;
   Bebida = bebida;
   Postre = postre;
   Descuento = descuento;
   Precio = CalcularPrecio();
}

//! IMP!
//? 🔍 NO ENTIENDO COMO SE HACE. SI UTILIZASE AQUÍ LOS ID PARA CLACULAR EL PRECIO TEND´RIA QUE HACER LLAMADAS GET A LA BASE DE DATO SPARA OBTENER LOS OBJETOS Y ESO ROMPERÍA LA SEPARACIÓN DE REPSONSABILIDADES
//constructor con Ids --> foreign keys de la bbdd
public Combo(int platoPrincipalId, int bebidaId, int postreId, double descuento): base("Combo especial", 0) {
   PlatoPrincipalId = platoPrincipalId;
   BebidaId = bebidaId;
   PostreId = postreId;
   Descuento = descuento;
   ////no meto cáculo de precio porque no se cmoo hacerlo con los IDS 
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