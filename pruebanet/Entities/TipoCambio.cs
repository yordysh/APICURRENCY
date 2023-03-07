namespace pruebanet.Entities
{
    public class TipoCambio
    {
        public int Id { get; set; }
        public string Moneda { get; set; }
        public decimal Compra { get; set; }
        public decimal Venta { get; set; }
        public DateTime Fecha { get; set; }

    }
}
