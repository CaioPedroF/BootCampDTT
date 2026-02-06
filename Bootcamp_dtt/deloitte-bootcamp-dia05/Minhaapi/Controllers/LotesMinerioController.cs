using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinhaApi.Data;
using MinhaApi.Models;
using MinhaApi.Dtos;

namespace MinhaApi.Controllers
{
    [ApiController]
    [Route("api/LotesMinerio")]
    public class LotesMinerioController : ControllerBase
    {
        private readonly AppDbContext _db;

        public LotesMinerioController(AppDbContext db) => _db = db;

        // Método POST para criar um lote
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateLoteMinerioDto input)
        {
            // Validações do lote
            if (string.IsNullOrWhiteSpace(input.CodigoLote))
                return BadRequest("CodigoLote é obrigatório.");
            if (string.IsNullOrWhiteSpace(input.MinaOrigem))
                return BadRequest("MinaOrigem é obrigatória.");
            if (string.IsNullOrWhiteSpace(input.LocalizacaoAtual))
                return BadRequest("LocalizacaoAtual é obrigatória.");
            if (input.TeorFe is < 0 or > 100)
                return BadRequest("TeorFe deve estar entre 0 e 100 (%).");
            if (input.Umidade is < 0 or > 100)
                return BadRequest("Umidade deve estar entre 0 e 100 (%).");
            if (input.Toneladas <= 0)
                return BadRequest("Toneladas deve ser > 0.");
            if (input.Status is < 0 or > 2)
                return BadRequest("Status inválido (use 0, 1 ou 2).");

            var exists = await _db.LotesMinerio.AnyAsync(x => x.CodigoLote == input.CodigoLote);
            if (exists)
                return Conflict($"Já existe um lote com CodigoLote '{input.CodigoLote}'.");

            var lote = new LoteMinerio
            {
                CodigoLote = input.CodigoLote,
                MinaOrigem = input.MinaOrigem,
                TeorFe = input.TeorFe,
                Umidade = input.Umidade,
                SiO2 = input.SiO2,
                P = input.P,
                Toneladas = input.Toneladas,
                DataProducao = input.DataProducao ?? DateTime.UtcNow,
                Status = (StatusLote)input.Status,
                LocalizacaoAtual = input.LocalizacaoAtual
            };

            _db.LotesMinerio.Add(lote);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = lote.Id }, lote);
        }

        // Método GET para buscar um lote por ID
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var lote = await _db.LotesMinerio.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (lote is null) return NotFound();

            var dto = new LoteMinerioResponseDto(
                lote.Id, lote.CodigoLote, lote.MinaOrigem, lote.TeorFe, lote.Umidade, lote.SiO2, lote.P,
                lote.Toneladas, lote.DataProducao, lote.Status, lote.LocalizacaoAtual
            );

            return Ok(dto);
        }

        // Método GET para listar todos os lotes
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var lotes = await _db.LotesMinerio.AsNoTracking().ToListAsync();
            if (lotes == null || !lotes.Any()) return NotFound("Nenhum lote encontrado.");

            var dtos = lotes.Select(l => new LoteMinerioResponseDto(
                l.Id, l.CodigoLote, l.MinaOrigem, l.TeorFe, l.Umidade, l.SiO2, l.P,
                l.Toneladas, l.DataProducao, l.Status, l.LocalizacaoAtual
            )).ToList();

            return Ok(dtos);
        }

        // Método PUT para atualizar um lote existente
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateLoteMinerioDto input)
        {
            // Validações do lote
            if (string.IsNullOrWhiteSpace(input.CodigoLote))
                return BadRequest("CodigoLote é obrigatório.");
            if (string.IsNullOrWhiteSpace(input.MinaOrigem))
                return BadRequest("MinaOrigem é obrigatória.");
            if (string.IsNullOrWhiteSpace(input.LocalizacaoAtual))
                return BadRequest("LocalizacaoAtual é obrigatória.");
            if (input.TeorFe is < 0 or > 100)
                return BadRequest("TeorFe deve estar entre 0 e 100 (%).");
            if (input.Umidade is < 0 or > 100)
                return BadRequest("Umidade deve estar entre 0 e 100 (%).");
            if (input.Toneladas <= 0)
                return BadRequest("Toneladas deve ser > 0.");
            if (input.Status is < 0 or > 2)
                return BadRequest("Status inválido (use 0, 1 ou 2).");

            // Busca o lote no banco de dados
            var lote = await _db.LotesMinerio.FirstOrDefaultAsync(x => x.Id == id);
            if (lote == null)
                return NotFound($"Lote com ID {id} não encontrado.");

            // Atualiza os dados do lote
            lote.CodigoLote = input.CodigoLote;
            lote.MinaOrigem = input.MinaOrigem;
            lote.TeorFe = input.TeorFe;
            lote.Umidade = input.Umidade;
            lote.SiO2 = input.SiO2;
            lote.P = input.P;
            lote.Toneladas = input.Toneladas;
            lote.DataProducao = input.DataProducao ?? DateTime.UtcNow;
            lote.Status = (StatusLote)input.Status;
            lote.LocalizacaoAtual = input.LocalizacaoAtual;

            // Salva as alterações no banco de dados
            await _db.SaveChangesAsync();

            // Retorna o lote atualizado
            var dto = new LoteMinerioResponseDto(
                lote.Id, lote.CodigoLote, lote.MinaOrigem, lote.TeorFe, lote.Umidade, lote.SiO2, lote.P,
                lote.Toneladas, lote.DataProducao, lote.Status, lote.LocalizacaoAtual
            );

            return Ok(dto);
        }

        // Método DELETE para remover um lote por ID
          [HttpDelete("{id:int}")]
          public async Task<IActionResult> Delete(int id)
{
    // Procurando o lote no banco de dados pelo ID
           var lote = await _db.LotesMinerio.FindAsync(id);
    
    // Se o lote não for encontrado, retorna 404
            if (lote == null) 
              return NotFound($"Lote com ID {id} não encontrado.");

    // Remover o lote
           _db.LotesMinerio.Remove(lote);

    // Salvar as alterações no banco de dados
           await _db.SaveChangesAsync();

    // Retornar um status 204 (No Content) para indicar que a remoção foi bem-sucedida
          return NoContent();
}

    }
}
