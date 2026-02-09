using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MinhaApi.Controllers;
using MinhaApi.Data;
using MinhaApi.Dtos;


namespace deloitte_bootcamp_dia06.Models
{
    public class LoteMineiroTests
    {
        private AppDbContext CriarContextoEmMemoria()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public async Task Create_DeveCriarLoteERetornarCreated()
        {
            // Arrange
            var context = CriarContextoEmMemoria();
            var controller = new LotesMinerioController(context);

            var dto = new CreateLoteMinerioDto
            {
                CodigoLote = "LT-001",
                MinaOrigem = "Mina A",
                LocalizacaoAtual = "Patio 1",
                TeorFe = 65,
                Umidade = 10,
                SiO2 = 5,
                P = 1,
                Toneladas = 100,
                Status = 1
            };

            // Act
            var result = await controller.Create(dto);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.NotNull(createdResult.Value);
        }
        [Fact]


//quando CodigoLote é vazio
//Esse teste garante que a validação funciona
     public async Task Create_CodigoLoteVazio_DeveRetornarBadRequest()
{
    // Arrange
    var context = CriarContextoEmMemoria();
    var controller = new LotesMinerioController(context);

    var dto = new CreateLoteMinerioDto
    {
        CodigoLote = "", // inválido
        MinaOrigem = "Mina A",
        LocalizacaoAtual = "Patio 1",
        TeorFe = 60,
        Umidade = 10,
        Toneladas = 100,
        Status = 1
    };

    // Act
    var result = await controller.Create(dto);

    // Assert
    Assert.IsType<BadRequestObjectResult>(result);
}




//Conflict – quando CodigoLote já existe
//Aqui testamos duplicidade.

[Fact]
public async Task Create_CodigoLoteDuplicado_DeveRetornarConflict()
{
    // Arrange
    var context = CriarContextoEmMemoria();
    var controller = new LotesMinerioController(context);

    var dto = new CreateLoteMinerioDto
    {
        CodigoLote = "LT-001",
        MinaOrigem = "Mina A",
        LocalizacaoAtual = "Patio 1",
        TeorFe = 60,
        Umidade = 10,
        Toneladas = 100,
        Status = 1
    };

    // cria o primeiro
    await controller.Create(dto);

    // Act - tenta criar novamente
    var result = await controller.Create(dto);

    // Assert
    Assert.IsType<ConflictObjectResult>(result);
}


//GetById – quando o lote existe

[Fact]
public async Task GetById_LoteExistente_DeveRetornarOk()
{
    // Arrange
    var context = CriarContextoEmMemoria();
    var controller = new LotesMinerioController(context);

    var dto = new CreateLoteMinerioDto
    {
        CodigoLote = "LT-002",
        MinaOrigem = "Mina B",
        LocalizacaoAtual = "Patio 2",
        TeorFe = 62,
        Umidade = 12,
        Toneladas = 200,
        Status = 1
    };

    var createResult = await controller.Create(dto) as CreatedAtActionResult;
    dynamic loteCriado = createResult!.Value!;
    int id = loteCriado.Id;

    // Act
    var result = await controller.GetById(id);

    // Assert
    Assert.IsType<OkObjectResult>(result);
}


//Delete – quando o lote existe

[Fact]
public async Task Delete_LoteExistente_DeveRetornarNoContent()
{
    // Arrange
    var context = CriarContextoEmMemoria();
    var controller = new LotesMinerioController(context);

    var dto = new CreateLoteMinerioDto
    {
        CodigoLote = "LT-003",
        MinaOrigem = "Mina C",
        LocalizacaoAtual = "Patio 3",
        TeorFe = 58,
        Umidade = 8,
        Toneladas = 150,
        Status = 1
    };

    var createResult = await controller.Create(dto) as CreatedAtActionResult;
    dynamic loteCriado = createResult!.Value!;
    int id = loteCriado.Id;

    // Act
    var result = await controller.Delete(id);

    // Assert
    Assert.IsType<NoContentResult>(result);
}

 }

    
}



