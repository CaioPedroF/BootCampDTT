using MinhaApi.Dtos;
using MinhaApi.Models;

namespace MinhaApi.Services
{
    public class LoteService
    {
        // 1️⃣ CLASSIFICAÇÃO
        public string ClassificarQualidade(LoteMinerioResponseDto lote)
        {
            double sio2 = lote.SiO2 ?? 0;

            if (lote.TeorFe >= 65 && lote.Umidade <= 8 && sio2 <= 4)
                return "Premium";

            if (lote.TeorFe >= 58 && lote.Umidade <= 12 && sio2 <= 6)
                return "Padrão";

            return "Baixa";
        }

        // 2️⃣ PREÇO
        public object CalcularPreco(LoteMinerioResponseDto lote)
        {
            var classificacao = ClassificarQualidade(lote);

            double precoTon = classificacao switch
            {
                "Premium" => 120,
                "Padrão" => 95,
                _ => 70
            };

            return new
            {
                classificacao,
                precoTonelada = precoTon,
                valorTotal = precoTon * lote.Toneladas
            };
        }

        // 5️⃣ PENALIDADE UMIDADE
        public double CalcularPenalidadeUmidade(LoteMinerioResponseDto lote)
        {
            double limite = 10;

            if (lote.Umidade <= limite)
                return 0;

            return (lote.Umidade - limite) * 2.5;
        }

        // 3️⃣ HISTÓRICO MOVIMENTAÇÃO
        public string RegistrarMovimentacao(string local, StatusLote status)
        {
            return $"Movimentado para {local} | Status: {status} | Data: {DateTime.UtcNow}";
        }

        // 4️⃣ AVANÇAR STATUS 
        public StatusLote AvancarStatus(StatusLote statusAtual)
        {
            return statusAtual switch
            {
                StatusLote.Pendente => StatusLote.EmProcessamento,
                StatusLote.EmProcessamento => StatusLote.Finalizado,
                _ => statusAtual
            };
        }
    }
}
