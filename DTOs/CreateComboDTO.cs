// DTOs/CreateComboDTO.cs
namespace DTOs
{
    public class CreateComboDTO
    {
        // IDs de los productos seleccionados
        public int PlatoPrincipalId { get; set; }
        public int BebidaId { get; set; }
        public int PostreId { get; set; }

        public double Descuento { get; set; }
    }
}
