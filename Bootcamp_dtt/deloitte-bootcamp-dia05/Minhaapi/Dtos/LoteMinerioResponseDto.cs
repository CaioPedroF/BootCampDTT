using MinhaApi.Models; 

namespace MinhaApi.Dtos
{
    public class LoteMinerioResponseDto
    {
        public int Id { get; set; }
        public string CodigoLote { get; set; }
        public string MinaOrigem { get; set; }
        public double TeorFe { get; set; }
        public double Umidade { get; set; }
        public double? SiO2 { get; set; }  // Torna SiO2 opcional
        public double? P { get; set; }     // Torna P opcional
        public double Toneladas { get; set; }
        public DateTime DataProducao { get; set; }
        public StatusLote Status { get; set; }  // Tipo de status que é convertido para enum
        public string LocalizacaoAtual { get; set; }

        // Construtor
        public LoteMinerioResponseDto(int id, string codigoLote, string minaOrigem, double teorFe, double umidade,
                                      double? siO2, double? p, double toneladas, DateTime dataProducao,
                                      StatusLote status, string localizacaoAtual)
        {
            Id = id;
            CodigoLote = codigoLote;
            MinaOrigem = minaOrigem;
            TeorFe = teorFe;
            Umidade = umidade;
            SiO2 = siO2;
            P = p;
            Toneladas = toneladas;
            DataProducao = dataProducao;
            Status = status;
            LocalizacaoAtual = localizacaoAtual;
        }
    }
}
