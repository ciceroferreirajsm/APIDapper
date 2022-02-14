using Dapper;
using Microsoft.Data.SqlClient;
using Slapper;
using StackExchange.Profiling;
using StackExchange.Profiling.Data;
using System.Data;
using WebApplication1.Data;

namespace WebApplication1.Repository
{
    public class RegioesRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;

        public RegioesRepository(IConfiguration configuration,
            IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        private IDbConnection GetConnection()
        {
            var connection = new SqlConnection(_configuration.GetConnectionString("BaseDadosGeograficos"));

            if (_environment.IsDevelopment())
                return new ProfiledDbConnection(connection, MiniProfiler.Current);

            return connection;
        }

        public IEnumerable<Regiao> Get(string? codRegiao = null)
        {
            var conexao = GetConnection();

            bool queryWithParameter = !String.IsNullOrWhiteSpace(codRegiao);
            var sqlCmd =
                "SELECT * " +
                "FROM dbo.Regioes ";

            object? paramQuery = null;
            if (queryWithParameter)
                paramQuery = new { CodigoRegiao = codRegiao };
            var dados = conexao.Query<dynamic>(sqlCmd, paramQuery);

            AutoMapper.Configuration.AddIdentifier(
                typeof(Regiao), "IdRegiao");
            AutoMapper.Configuration.AddIdentifier(
                typeof(Estado), "SiglaEstado");

            return (AutoMapper.MapDynamic<Regiao>(dados)
                as IEnumerable<Regiao>).ToArray();
        }
    }
}