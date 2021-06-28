namespace Api.Produtos.Models
{
    public class ProdutoModel
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public int Estoque { get; set; }

        public double Preco { get; set; }
       
    }
}