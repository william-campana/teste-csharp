using System.Diagnostics;
using System.Threading.Tasks;
using Api.Produtos.Services;
using Api.Produtos.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Produtos.Controllers
{

    /// <summary>
    /// EndPoint Regitro de produtos
    /// </summary>
    [ApiController]
    [Route("[Controller]")]
    public class ProdutoController : ControllerBase
    {
        public ILogger<ProdutoController> _logger { get; }
        public IProdutoService _produtoService { get; }

        public ProdutoController(IProdutoService produtoService, ILogger<ProdutoController> logger)
        {
            _logger = logger;
            _produtoService = produtoService;
        }


        /// <summary>
        /// Recuperar Lista de Produtos
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetProdutos")]
        public async Task<IActionResult> GetListAsync()
        {
            try
            {
                return Ok(await _produtoService.GetProdutos());
            }
            catch (System.Exception ex)
            {
                _logger.LogCritical($"#### Erro ao recuperar os produtos {nameof(GetListAsync)}, exception: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Recuperar um produto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetProduto/{id}")]
        public async Task<IActionResult> GetProdutoAsync(int id)
        {
            try
            {
                return Ok(await _produtoService.GetProduto(id));
            }
            catch (System.Exception ex)
            {
                _logger.LogCritical($"#### Erro ao recuperar o produto: {id} - {nameof(GetListAsync)}, exception: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Insere produto
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost()]
        public async Task<IActionResult> PostProdutoAsync([FromBody] ProdutoViewModel model)
        {
            try
            {
                var result = await _produtoService.PostProduto(model);
                if (result)
                {
                    return NoContent();
                }
                else
                {
                    return BadRequest("Ocorreu um erro no Insert");
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogCritical($"#### Erro ao inserir produto {nameof(GetListAsync)}, exception: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Altera Produto
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProdutoAsync([FromBody] ProdutoViewModel model, int id)
        {
            try
            {
                await _produtoService.PutProduto(model, id);
                return NoContent();

            }
            catch (System.Exception ex)
            {
                _logger.LogCritical($"#### Erro ao atualizar o produto {nameof(GetListAsync)}, exception: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Excluir o Produto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduotAsync(int id)
        {

            try
            {
                var result = await _produtoService.DeleteProduto(id);
                if (result)
                {
                    return NoContent();
                }
                else
                {
                    return BadRequest("Ocorreu um problema");
                }

            }
            catch (System.Exception ex)
            {
                _logger.LogCritical($"#### Erro ao recuperar os produtos {nameof(GetListAsync)}, exception: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
    }
}