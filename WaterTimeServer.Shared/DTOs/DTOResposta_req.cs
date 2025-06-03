namespace WaterTimeServer.Shared.DTOs
{
    public class DTOResposta_req
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; } = Guid.Empty;        
        public bool BebeuAgua { get; set; } = false;
        public int QuantidadeEmML { get; set; } = 0;
    }
}
