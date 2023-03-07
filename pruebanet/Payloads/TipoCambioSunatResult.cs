using System.Text.Json.Serialization;

namespace pruebanet.Payloads
{
    public class TipoCambioSunatResult
    {
        [JsonPropertyName("compra")]
        public decimal Compra { get; set; }

        [JsonPropertyName("venta")]
        public decimal Venta { get; set; }

        [JsonPropertyName("origen")]
        public string Origen { get; set; }

        [JsonPropertyName("moneda")]
        public string Moneda { get; set; }

        [JsonPropertyName("fecha")]
        public string Fecha { get; set; }
    }
}
