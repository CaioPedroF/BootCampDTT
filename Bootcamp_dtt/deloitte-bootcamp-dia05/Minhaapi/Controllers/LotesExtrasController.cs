using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinhaApi.Data;
using MinhaApi.Dtos;
using MinhaApi.Services;

namespace MinhaApi.Controllers
{
    [ApiController]
    [Route("api/lotes-extras")]
    public class LotesExtrasController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly LoteService _service;

        public LotesExtrasController(AppDbContext db, LoteService service)
        {
            _db = db;
            _service = service;
        }

        private async Task<LoteMinerioResponseDto?> BuscarDto(int id)
        {
            var lote = await _db.LotesMinerio.FindAsync(id);
            if (lote == null) return null;

            return new LoteMinerioResponseDto(
                lote.Id,
                lote.CodigoLote,
                lote.MinaOrigem,
                lote.TeorFe,
                lote.Umidade,
                lote.SiO2,
                lote.P,
                lote.Toneladas,
                lote.DataProducao,
                lote.Status,
                lote.LocalizacaoAtual
            );
        }

        // =============================
        // GET CLASSIFICAÇÃO
        // =============================
        [HttpGet("{id}/classificacao")]
        public async Task<IActionResult> Classificacao(int id)
        {
            var dto = await BuscarDto(id);
            if (dto == null) return NotFound();

            return Ok(_service.ClassificarQualidade(dto));
        }

        // =============================
        // GET PREÇO
        // =============================
        [HttpGet("{id}/preco")]
        public async Task<IActionResult> Preco(int id)
        {
            var dto = await BuscarDto(id);
            if (dto == null) return NotFound();

            return Ok(_service.CalcularPreco(dto));
        }

        // =============================
        // GET PENALIDADE
        // =============================
        [HttpGet("{id}/penalidade")]
        public async Task<IActionResult> Penalidade(int id)
        {
            var dto = await BuscarDto(id);
            if (dto == null) return NotFound();

            return Ok(_service.CalcularPenalidadeUmidade(dto));
        }

        // =============================
        // POST HISTÓRICO
        // =============================
        [HttpPost("{id}/movimentar")]
        public async Task<IActionResult> Movimentar(int id)
        {
            var lote = await _db.LotesMinerio.FindAsync(id);
            if (lote == null) return NotFound();

            var registro = _service.RegistrarMovimentacao(
                lote.LocalizacaoAtual,
                lote.Status
            );

            return Ok(registro);
        }

        // =============================
        // POST AVANÇAR STATUS
        // =============================
        [HttpPost("{id}/avancar-status")]
        public async Task<IActionResult> AvancarStatus(int id)
        {
            var lote = await _db.LotesMinerio.FindAsync(id);
            if (lote == null) return NotFound();

            lote.Status = _service.AvancarStatus(lote.Status);
            await _db.SaveChangesAsync();

            return Ok(lote.Status);
        }
    }
}
