using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinhaApi.Data;
using MinhaApi.Models;
using MinhaApi.Dtos;
using StackExchange.Redis;
using System.Text.Json;

namespace MinhaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LotesMinerioController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IDatabase _cache;

        public LotesMinerioController(AppDbContext db, IConnectionMultiplexer redis)
        {
            _db = db;
            _cache = redis.GetDatabase();
        }

        private string GetLoteKey(int id) => $"lote:{id}";
        private string GetAllKey() => "lotes:all";
        private readonly TimeSpan CacheTime = TimeSpan.FromMinutes(10);

        // ================================
        // HELPER: Adicionar evento na Stream
        // ================================
        private async Task AdicionarEventoStream(string evento, LoteMinerio lote)
{
    string NotNull(string? value) => value ?? "";

    var dados = new NameValueEntry[]
    {
        new NameValueEntry("evento", evento),
        new NameValueEntry("id", lote.Id.ToString()),
        new NameValueEntry("codigoLote", NotNull(lote.CodigoLote)),
        new NameValueEntry("minaOrigem", NotNull(lote.MinaOrigem)),
        new NameValueEntry("teorFe", lote.TeorFe.ToString()),
        new NameValueEntry("umidade", lote.Umidade.ToString()),
        new NameValueEntry("siO2", lote.SiO2.ToString()),
        new NameValueEntry("p", lote.P.ToString()),
        new NameValueEntry("toneladas", lote.Toneladas.ToString()),
        new NameValueEntry("dataProducao", lote.DataProducao.ToString("o")),
        new NameValueEntry("status", lote.Status.ToString()),
        new NameValueEntry("localizacaoAtual", NotNull(lote.LocalizacaoAtual))
    };

    await _cache.StreamAddAsync("lotes_stream", dados);
}


        // ================================
        // GET ALL (COM CACHE)
        // ================================
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cacheKey = GetAllKey();
            var cached = await _cache.StringGetAsync(cacheKey);

            if (!cached.IsNullOrEmpty)
            {
                var dtosCache = JsonSerializer.Deserialize<List<LoteMinerioResponseDto>>(cached.ToString());
                return Ok(dtosCache);
            }

            var lotes = await _db.LotesMinerio.AsNoTracking().ToListAsync();
            if (!lotes.Any()) return NotFound();

            var dtos = lotes.Select(l => new LoteMinerioResponseDto(
                l.Id, l.CodigoLote, l.MinaOrigem, l.TeorFe, l.Umidade,
                l.SiO2, l.P, l.Toneladas, l.DataProducao,
                l.Status, l.LocalizacaoAtual
            )).ToList();

            await _cache.StringSetAsync(cacheKey, JsonSerializer.Serialize(dtos), CacheTime);

            return Ok(dtos);
        }

        // ================================
        // GET BY ID (COM CACHE)
        // ================================
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var cacheKey = GetLoteKey(id);
            var cached = await _cache.StringGetAsync(cacheKey);

            if (!cached.IsNullOrEmpty)
            {
                var dtoCache = JsonSerializer.Deserialize<LoteMinerioResponseDto>(cached.ToString());
                return Ok(dtoCache);
            }

            var lote = await _db.LotesMinerio.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (lote is null) return NotFound();

            var dto = new LoteMinerioResponseDto(
                lote.Id, lote.CodigoLote, lote.MinaOrigem, lote.TeorFe, lote.Umidade,
                lote.SiO2, lote.P, lote.Toneladas, lote.DataProducao,
                lote.Status, lote.LocalizacaoAtual
            );

            await _cache.StringSetAsync(cacheKey, JsonSerializer.Serialize(dto), CacheTime);

            return Ok(dto);
        }

        // ================================
        // CREATE
        // ================================
        [HttpPost]
        public async Task<IActionResult> Create(CreateLoteMinerioDto dto)
        {
            var lote = new LoteMinerio
            {
                CodigoLote = dto.CodigoLote,
                MinaOrigem = dto.MinaOrigem,
                TeorFe = dto.TeorFe,
                Umidade = dto.Umidade,
                SiO2 = dto.SiO2,
                P = dto.P,
                Toneladas = dto.Toneladas,
                DataProducao = dto.DataProducao ?? DateTime.UtcNow,
                Status = (StatusLote)dto.Status,
                LocalizacaoAtual = dto.LocalizacaoAtual
            };

            _db.LotesMinerio.Add(lote);
            await _db.SaveChangesAsync();

            // limpa cache lista
            await _cache.KeyDeleteAsync(GetAllKey());

            // adiciona evento no Redis Stream
            await AdicionarEventoStream("create", lote);

            return CreatedAtAction(nameof(GetById), new { id = lote.Id }, lote);
        }

        // ================================
        // UPDATE
        // ================================
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, CreateLoteMinerioDto dto)
        {
            var lote = await _db.LotesMinerio.FindAsync(id);
            if (lote == null) return NotFound();

            lote.CodigoLote = dto.CodigoLote;
            lote.MinaOrigem = dto.MinaOrigem;
            lote.TeorFe = dto.TeorFe;
            lote.Umidade = dto.Umidade;
            lote.SiO2 = dto.SiO2;
            lote.P = dto.P;
            lote.Toneladas = dto.Toneladas;
            lote.DataProducao = dto.DataProducao ?? DateTime.UtcNow;
            lote.Status = (StatusLote)dto.Status;
            lote.LocalizacaoAtual = dto.LocalizacaoAtual;

            await _db.SaveChangesAsync();

            // limpa cache
            await _cache.KeyDeleteAsync(GetLoteKey(id));
            await _cache.KeyDeleteAsync(GetAllKey());

            // adiciona evento no Redis Stream
            await AdicionarEventoStream("update", lote);

            return NoContent();
        }

        // ================================
        // DELETE
        // ================================
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var lote = await _db.LotesMinerio.FindAsync(id);
            if (lote == null) return NotFound();

            _db.LotesMinerio.Remove(lote);
            await _db.SaveChangesAsync();

            // limpa cache
            await _cache.KeyDeleteAsync(GetLoteKey(id));
            await _cache.KeyDeleteAsync(GetAllKey());

            // adiciona evento no Redis Stream
            await AdicionarEventoStream("delete", lote);

            return NoContent();
        }
    }
}
