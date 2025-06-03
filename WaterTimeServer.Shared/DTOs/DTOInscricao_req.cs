using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace WaterTimeServer.Shared.DTOs
{
    public class DTOInscricao_req
    {
        public Guid IdUsuario { get; set; } = Guid.Empty;
        public string Endpoint { get; set; } = "";
        public DateTime? ExpirationTime { get; set; }      
        
        [NotMapped]
        public Dictionary<string, string> Keys { get; set; } = [];

        public string KeysDicionary
        {
            get => JsonSerializer.Serialize(Keys);
            set => Keys = string.IsNullOrWhiteSpace(value)
                ? []
                : JsonSerializer.Deserialize<Dictionary<string, string>>(value)!;
        }
    }
}
