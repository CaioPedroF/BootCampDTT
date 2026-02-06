using System;

namespace MinhaApi.Dtos
{
    public class CreateLoteMinerioDto
    {
        public string CodigoLote { get; set; }
        public string MinaOrigem { get; set; }
        public double TeorFe { get; set; }
        public double Umidade { get; set; }
        public double? SiO2 { get; set; }  // Torna SiO2 opcional
        public double? P { get; set; }     // Torna P opcional
        public double Toneladas { get; set; }
        public DateTime? DataProducao { get; set; }
        public int Status { get; set; }  // Usado para mapear para o enum StatusLote
        public string LocalizacaoAtual { get; set; }
    }
}
