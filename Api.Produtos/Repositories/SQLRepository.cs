using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Api.Produtos.Models;
using Api.Produtos.ViewModel;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Dapper;

namespace Api.Produtos.Repositories
{

    public interface ISQLRepository
    {
        Task<IEnumerable<Models.ProdutoModel>> GetProdutos();

        Task<Models.ProdutoModel> GetProduto(int id);

        Task<bool> PostProduto(ViewModel.ProdutoViewModel model);

        Task<bool> PutProduto(ViewModel.ProdutoViewModel model, int id);

        Task<bool> DeleteProduto(int id);

    }

    public class SQLRepository : ISQLRepository
    {
        public IConfiguration _configuration { get; }

        public SQLRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection dbConnection
        {
            get
            {
                return new Microsoft.Data.SqlClient.SqlConnection(_configuration["Database:SQL"]);
            }
        }


        public async Task<bool> DeleteProduto(int id)
        {
            using (var connection = dbConnection)
            {
                connection.Open();

                var sql = @"DELETE FROM PRODUTO WHERE ID = @ID";

                var result = await connection.ExecuteAsync(sql, new
                {
                    ID = id
                });

                return result > 0;

            }
        }

        public async Task<ProdutoModel> GetProduto(int id)
        {
            using(var connection = dbConnection)
            {
                connection.Open();

                var sql = "SELECT * FROM PRODUTO WHERE ID = @ID";

                var result = await connection.QuerySingleAsync<ProdutoModel>(sql, new {ID = id});

                return result;
            }

        }

        public async Task<IEnumerable<ProdutoModel>> GetProdutos()
        {
            using (var connection = dbConnection)
            {
                connection.Open();

                var sql = @"SELECT * FROM PRODUTO";

                var result = await connection.QueryAsync<ProdutoModel>(sql);

                return result;

            }
        }

        public async Task<bool> PostProduto(ProdutoViewModel model)
        {
            using (var connection = dbConnection)
            {
                var sql = @"INSERT INTO PRODUTO (NOME, ESTOQUE, PRECO) VALUES (@NOME, @ESTOQUE, @PRECO)";

                var result = await connection.ExecuteAsync(sql, new {
                    NOME = model.Nome,
                    ESTOQUE = model.Estoque,
                    PRECO = model.Preco
                });

                return result == 1;
            }
        }
        public async Task<bool> PutProduto(ProdutoViewModel model, int id)
        {
            using (var connection = dbConnection)
            {
                var sql = @"UPDATE PRODUTO SET NOME = @NOME, PRECO = @PRECO, ESTOQUE = @ESTOQUE WHERE ID = @ID";

                var result = await connection.ExecuteAsync(sql, new {
                    NOME = model.Nome,
                    PRECO = model.Preco,
                    ESTOQUE = model.Estoque,
                    ID = id
                });

                return result == 1;
            }
        }
    }
}