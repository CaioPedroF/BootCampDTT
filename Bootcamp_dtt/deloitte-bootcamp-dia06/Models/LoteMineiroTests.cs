using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MinhaApi.Controllers;
using MinhaApi.Data;
using MinhaApi.Dtos;
using System;

namespace deloitte_bootcamp_dia06.Models
{
    public class LoteMineiroTests
    {
        private AppDbContext CriarContextoEmMemoria()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }

        // =======================
        // CREATE
        // =======================

        [Fact]
        public async Task Create_DeveCriarLoteERetornarCreated()
        {
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

            var result = await controller.Create(dto);

            Assert.IsType<CreatedAtActionResult>(result);
        }

        [Fact]
        public async Task Create_CodigoLoteVazio_DeveRetornarBadRequest()
        {
            var context = CriarContextoEmMemoria();
            var controller = new LotesMinerioController(context);

            var dto = new CreateLoteMinerioDto
            {
                CodigoLote = "",
                MinaOrigem = "Mina A",
                LocalizacaoAtual = "Patio 1",
                TeorFe = 60,
                Umidade = 10,
                Toneladas = 100,
                Status = 1
            };

            var result = await controller.Create(dto);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Create_CodigoLoteDuplicado_DeveRetornarConflict()
        {
            var context = CriarContextoEmMemoria();
            var controller = new LotesMinerioController(context);

            var dto = new CreateLoteMinerioDto
            {
                CodigoLote = "LT-002",
                MinaOrigem = "Mina A",
                LocalizacaoAtual = "Patio",
                TeorFe = 60,
                Umidade = 10,
                Toneladas = 100,
                Status = 1
            };

            await controller.Create(dto);
            var result = await controller.Create(dto);

            Assert.IsType<ConflictObjectResult>(result);
        }

        [Fact]
        public async Task Create_TeorFeInvalido_DeveRetornarBadRequest()
        {
            var context = CriarContextoEmMemoria();
            var controller = new LotesMinerioController(context);

            var dto = new CreateLoteMinerioDto
            {
                CodigoLote = "LT-003",
                MinaOrigem = "Mina X",
                LocalizacaoAtual = "Patio",
                TeorFe = 120,
                Umidade = 10,
                Toneladas = 100,
                Status = 1
            };

            var result = await controller.Create(dto);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Create_UmidadeInvalida_DeveRetornarBadRequest()
        {
            var context = CriarContextoEmMemoria();
            var controller = new LotesMinerioController(context);

            var dto = new CreateLoteMinerioDto
            {
                CodigoLote = "LT-004",
                MinaOrigem = "Mina X",
                LocalizacaoAtual = "Patio",
                TeorFe = 60,
                Umidade = -1,
                Toneladas = 100,
                Status = 1
            };

            var result = await controller.Create(dto);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Create_ToneladasInvalidas_DeveRetornarBadRequest()
        {
            var context = CriarContextoEmMemoria();
            var controller = new LotesMinerioController(context);

            var dto = new CreateLoteMinerioDto
            {
                CodigoLote = "LT-005",
                MinaOrigem = "Mina X",
                LocalizacaoAtual = "Patio",
                TeorFe = 60,
                Umidade = 10,
                Toneladas = 0,
                Status = 1
            };

            var result = await controller.Create(dto);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Create_StatusInvalido_DeveRetornarBadRequest()
        {
            var context = CriarContextoEmMemoria();
            var controller = new LotesMinerioController(context);

            var dto = new CreateLoteMinerioDto
            {
                CodigoLote = "LT-006",
                MinaOrigem = "Mina X",
                LocalizacaoAtual = "Patio",
                TeorFe = 60,
                Umidade = 10,
                Toneladas = 100,
                Status = 99
            };

            var result = await controller.Create(dto);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        // =======================
        // GET BY ID
        // =======================

        [Fact]
        public async Task GetById_LoteExistente_DeveRetornarOk()
        {
            var context = CriarContextoEmMemoria();
            var controller = new LotesMinerioController(context);

            var create = await controller.Create(new CreateLoteMinerioDto
            {
                CodigoLote = "LT-007",
                MinaOrigem = "Mina B",
                LocalizacaoAtual = "Patio",
                TeorFe = 60,
                Umidade = 10,
                Toneladas = 100,
                Status = 1
            }) as CreatedAtActionResult;

            dynamic lote = create!.Value!;
            int id = lote.Id;

            var result = await controller.GetById(id);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetById_LoteInexistente_DeveRetornarNotFound()
        {
            var context = CriarContextoEmMemoria();
            var controller = new LotesMinerioController(context);

            var result = await controller.GetById(999);

            Assert.IsType<NotFoundResult>(result);
        }

        // =======================
        // GET ALL
        // =======================

        [Fact]
        public async Task GetAll_SemLotes_DeveRetornarNotFound()
        {
            var context = CriarContextoEmMemoria();
            var controller = new LotesMinerioController(context);

            var result = await controller.GetAll();

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task GetAll_ComLotes_DeveRetornarOk()
        {
            var context = CriarContextoEmMemoria();
            var controller = new LotesMinerioController(context);

            await controller.Create(new CreateLoteMinerioDto
            {
                CodigoLote = "LT-008",
                MinaOrigem = "Mina C",
                LocalizacaoAtual = "Patio",
                TeorFe = 60,
                Umidade = 10,
                Toneladas = 100,
                Status = 1
            });

            var result = await controller.GetAll();

            Assert.IsType<OkObjectResult>(result);
        }

        // =======================
        // DELETE
        // =======================

        [Fact]
        public async Task Delete_LoteExistente_DeveRetornarNoContent()
        {
            var context = CriarContextoEmMemoria();
            var controller = new LotesMinerioController(context);

            var create = await controller.Create(new CreateLoteMinerioDto
            {
                CodigoLote = "LT-009",
                MinaOrigem = "Mina D",
                LocalizacaoAtual = "Patio",
                TeorFe = 60,
                Umidade = 10,
                Toneladas = 100,
                Status = 1
            }) as CreatedAtActionResult;

            dynamic lote = create!.Value!;
            int id = lote.Id;

            var result = await controller.Delete(id);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_LoteInexistente_DeveRetornarNotFound()
        {
            var context = CriarContextoEmMemoria();
            var controller = new LotesMinerioController(context);

            var result = await controller.Delete(999);

            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}
