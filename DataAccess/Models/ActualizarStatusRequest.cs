namespace AutoVentas_Back.DataAccess.Models
{
    public class ActualizarStatusRequest
    {
        public decimal CodCliente { get; set; } // El código del cliente
        public decimal Status { get; set; }     // El nuevo estado que quieres asignar
    }
}
