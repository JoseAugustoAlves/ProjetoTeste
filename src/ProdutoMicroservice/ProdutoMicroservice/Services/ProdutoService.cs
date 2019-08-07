using ProdutoMicroservice.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace ProdutoMicroservice.Services
{
    public class ProdutoService
    {
        private readonly IMongoCollection<Produto> _produto;

        public ProdutoService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _produto = database.GetCollection<Produto>(settings.ProdutosCollectionName);
        }

        public List<Produto> Get() =>
            _produto.Find(produto => true).ToList();

        public Produto Get(string id) =>
            _produto.Find<Produto>(produto => produto.Id == id).FirstOrDefault();

        public Produto Create(Produto produto)
        {
            _produto.InsertOne(produto);
            return produto;
        }

        public void Update(string id, Produto produtoIn) =>
            _produto.ReplaceOne(produto => produto.Id == id, produtoIn);

        public void Remove(Produto produtoIn) =>
            _produto.DeleteOne(produto => produto.Id == produtoIn.Id);

        public void Remove(string id) =>
            _produto.DeleteOne(produto => produto.Id == id);
    }
}

