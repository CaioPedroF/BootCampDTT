namespace MinhaApi.Models
{
    public enum StatusLote
    {
        Pendente = 0,
        EmProcessamento = 1,
        Finalizado = 2
    }

    public class LoteMinerio
    {
        public int Id { get; set; }
        public string CodigoLote { get; set; }
        public string MinaOrigem { get; set; }
        public double TeorFe { get; set; }
        public double Umidade { get; set; }
        public double? SiO2 { get; set; }
        public double? P { get; set; }
        public double Toneladas { get; set; }
        public DateTime DataProducao { get; set; }
        public StatusLote Status { get; set; }
        public string LocalizacaoAtual { get; set; }
    }
}
