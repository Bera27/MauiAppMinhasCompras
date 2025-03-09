using MauiAppMinhasCompras.Models;
using SQLite;

namespace MauiAppMinhasCompras.Helpers
{
    public class SQLiteDatabaseHelper
    {
        readonly SQLiteAsyncConnection _connection;

        public SQLiteDatabaseHelper(string path)
        {
            _connection = new SQLiteAsyncConnection(path);
            _connection.CreateTableAsync<Produto>().Wait();
        }

        public Task<int> Insert(Produto produto) 
        {
            return _connection.InsertAsync(produto);
        }

        public Task<List<Produto>> Update(Produto produto) 
        {
            string sql = "UPDATE Produto SET Descricao=?, Quantidade=?, Preco=? WHERE Id=?";

            return _connection.QueryAsync<Produto>(sql , produto.Descricao, produto.Quantidade, produto.Preco, produto.Id);
        }

        public Task<int> Delete(int id) 
        {
            return _connection.Table<Produto>().DeleteAsync(i => i.Id == id);
        }

        public Task<List<Produto>> GetAll() 
        {
            return _connection.Table<Produto>().ToListAsync();
        }

        public Task<List<Produto>> Search(string query) 
        {
            string sql = "SELECT * FROM  Produto WHERE descricao LIKE '%" + query + "%'";

            return _connection.QueryAsync<Produto>(sql);
        }
    }
}
