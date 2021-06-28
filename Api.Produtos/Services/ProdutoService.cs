using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Produtos.Models;
using Api.Produtos.Repositories;
using Api.Produtos.ViewModel;

namespace Api.Produtos.Services
{

    public interface IProdutoService
    {

        Task<IEnumerable<Models.ProdutoModel>> GetProdutos();

        Task<Models.ProdutoModel> GetProduto(int id);

        Task<bool> PostProduto(ViewModel.ProdutoViewModel model);

        Task<bool> PutProduto(ViewModel.ProdutoViewModel model, int id);

        Task<bool> DeleteProduto(int id);

    }


    public class ProdutoService : IProdutoService
    {

        public ProdutoService(ISQLRepository sQLRepository)
        {
            _sQLRepository = sQLRepository;
        }

        public ISQLRepository _sQLRepository { get; private set; }

        public async Task<bool> DeleteProduto(int id)
        {
             if (id == 0)
                throw new System.ArgumentException("Id não pode ser nulo");

            return await _sQLRepository.DeleteProduto(id);
        }

        public async Task<ProdutoModel> GetProduto(int id)
        {

            if (id == 0)
                throw new System.ArgumentException("Id não pode ser nulo");

            return await _sQLRepository.GetProduto(id);

        }

        public async Task<IEnumerable<ProdutoModel>> GetProdutos()
        {
            return await _sQLRepository.GetProdutos();
        }

        public async Task<bool> PostProduto(ProdutoViewModel model)
        {
            return await _sQLRepository.PostProduto(model);
        }

        public async Task<bool> PutProduto(ProdutoViewModel model, int id)
        {
            return await _sQLRepository.PutProduto(model, id);
        }
    }
}