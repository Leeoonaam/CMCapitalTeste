using CMCapital.Application.Dtos.Adicionar;
using CMCapital.Application.Services;
using CMCapital.Application.Utils;
using CMCapital.Domain.Entities;
using CMCapital.Domain.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using System.Security.Claims;
using Xunit;

namespace CMCapital.Tests
{
    public class ClienteServiceTests
    {
        private readonly Mock<IClienteRepository> _repoMock;
        private readonly Mock<ILogger<ClienteService>> _loggerMock;
        private readonly SessaoUsuario _sessaoUsuario;
        private readonly ClienteService _service;

        public ClienteServiceTests()
        {
            _repoMock = new Mock<IClienteRepository>();
            _loggerMock = new Mock<ILogger<ClienteService>>();
            _sessaoUsuario = new SessaoUsuarioFake(1);
            _service = new ClienteService(_repoMock.Object, _sessaoUsuario, _loggerMock.Object);
        }

        [Fact]
        public async Task Incluir_DeveFalhar_QuandoSaldoMenorQueZero()
        {
            var request = new AdicionarClienteRequest { Nome = "Teste", SaldoDisponivel = -10 };

            var result = await _service.Incluir(request);

            result.Status.Should().BeFalse();
            result.Mensagem.Should().Be("Não é permitido cadastro de clientes com saldo menor que zero.");
            _repoMock.Verify(r => r.BuscarPorNome(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task Incluir_DeveFalhar_QuandoClienteJaExisteAtivo()
        {
            var clienteExistente = new TblCliente { Nome = "João", DthDelete = null };
            _repoMock.Setup(r => r.BuscarPorNome("João")).ReturnsAsync(clienteExistente);

            var request = new AdicionarClienteRequest { Nome = "João", SaldoDisponivel = 100 };
            var result = await _service.Incluir(request);

            result.Status.Should().BeFalse();
            result.Mensagem.Should().Be("Já existe um cliente cadastrado com esse nome!");
            _repoMock.Verify(r => r.Add(It.IsAny<TblCliente>()), Times.Never);
        }

        [Fact]
        public async Task Incluir_DeveReativarCliente_QuandoExisteExcluido()
        {
            var clienteExcluido = new TblCliente { Nome = "Maria", DthDelete = DateTime.Now };
            _repoMock.Setup(r => r.BuscarPorNome("Maria")).ReturnsAsync(clienteExcluido);
            _repoMock.Setup(r => r.Update(It.IsAny<TblCliente>())).ReturnsAsync(1);

            var request = new AdicionarClienteRequest { Nome = "Maria", SaldoDisponivel = 200 };
            var result = await _service.Incluir(request);

            result.Status.Should().BeTrue();
            result.Mensagem.Should().Be("Cliente reativado com Sucesso.");
            result.Resultado.Should().Be(clienteExcluido);
        }

        [Fact]
        public async Task Incluir_DeveFalhar_QuandoReativacaoRetornaZero()
        {
            var clienteExcluido = new TblCliente { Nome = "José", DthDelete = DateTime.Now };
            _repoMock.Setup(r => r.BuscarPorNome("José")).ReturnsAsync(clienteExcluido);
            _repoMock.Setup(r => r.Update(It.IsAny<TblCliente>())).ReturnsAsync(0);

            var request = new AdicionarClienteRequest { Nome = "José", SaldoDisponivel = 300 };
            var result = await _service.Incluir(request);

            result.Status.Should().BeFalse();
            result.Mensagem.Should().Be("Erro ao reativar Cliente.");
        }

        [Fact]
        public async Task Incluir_DeveCriarNovoCliente_QuandoNaoExiste()
        {
            _repoMock.Setup(r => r.BuscarPorNome("Carlos")).ReturnsAsync((TblCliente)null);
            _repoMock.Setup(r => r.Add(It.IsAny<TblCliente>())).ReturnsAsync(1);

            var request = new AdicionarClienteRequest { Nome = "Carlos", SaldoDisponivel = 500 };
            var result = await _service.Incluir(request);

            result.Status.Should().BeTrue();
            result.Mensagem.Should().Be("Cliente incluído com Sucesso.");
        }

        [Fact]
        public async Task Incluir_DeveFalhar_QuandoAddRetornaZero()
        {
            _repoMock.Setup(r => r.BuscarPorNome("Ana")).ReturnsAsync((TblCliente)null);
            _repoMock.Setup(r => r.Add(It.IsAny<TblCliente>())).ReturnsAsync(0);

            var request = new AdicionarClienteRequest { Nome = "Ana", SaldoDisponivel = 400 };
            var result = await _service.Incluir(request);

            result.Status.Should().BeFalse();
            result.Mensagem.Should().Be("Erro ao incluir novo Cliente.");
        }

        [Fact]
        public async Task Incluir_DeveTratarExcecaoERetornarErro()
        {
            _repoMock.Setup(r => r.BuscarPorNome("Pedro")).ThrowsAsync(new Exception("Falha DB"));

            var request = new AdicionarClienteRequest { Nome = "Pedro", SaldoDisponivel = 600 };
            var result = await _service.Incluir(request);

            result.Status.Should().BeFalse();
            result.Mensagem.Should().Be("Erro ao incluir Cliente.");
            _loggerMock.Verify(l => l.Log(
                It.Is<LogLevel>(ll => ll == LogLevel.Error),
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()),
                Times.Once);
        }
    }

    // Fake simples para simular SessaoUsuario
    public class SessaoUsuarioFake : SessaoUsuario
    {
        private readonly int _id;

        public SessaoUsuarioFake(int id) : base(new HttpContextAccessor())
        {
            _id = id;
        }

        // Métodos/propriedades independentes para testes
        public int FakeId => _id;
        public string FakeUsuarioId => _id.ToString();
        public string FakeCpf => "12345678900";
        public DateTime FakeVencimento => DateTime.Now.AddHours(1);

        public IEnumerable<Claim> FakeClaims => new List<Claim>
        {
            new Claim(ClaimTypes.Sid, _id.ToString()),
            new Claim(ClaimTypes.SerialNumber, "12345678900"),
            new Claim("VencimentoSessao", DateTime.Now.AddHours(1).ToString())
        };
    }
}
