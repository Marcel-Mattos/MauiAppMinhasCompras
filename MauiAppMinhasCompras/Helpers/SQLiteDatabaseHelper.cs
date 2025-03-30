

using MauiAppMinhasCompras.Models;
using SQLite;

namespace MauiAppMinhasCompras.Helpers
{
    public class SQLiteDatabaseHelper
    {
        readonly SQLiteAsyncConnection _conn;
        public SQLiteDatabaseHelper(string path) 
        { 
            _conn = new SQLiteAsyncConnection(path);
            _conn.CreateTableAsync<Produto>().Wait();
        }
        //estrutura da classe
        public Task<int> Insert(Produto p) //inserir parametro p
        { 
            return _conn.InsertAsync(p);
        } 
        
        public Task<List<Produto>> Update(Produto p) //atualizar
        {
            string sql = "UPDATE Produto SET Categoria=?, Descricao=?, Quantidade=?, Preco=? WHERE Id=?";

            return _conn.QueryAsync<Produto>(
                sql, p.Categoria, p.Descricao, p.Quantidade, p.Preco, p.Id
             );
        }

        public Task<int> Delete(int id)  //deletar somente o que eu quero, por isso id
        {
            return _conn.Table<Produto>().DeleteAsync(i => i.Id == id);

        }
        public Task<List<Produto>> GetAll() //pegar todos os produtos
        {
            return _conn.Table<Produto>().ToListAsync(); 
        }
        public Task<List<Produto>> Search(string q) //busca na tabela
        {
            string sql = "SELECT * FROM Produto WHERE descricao LIKE '%"+ q +"%' ";

            return _conn.QueryAsync<Produto>(sql);

        }
        public Task<List<Produto>> Searchcategoria(string q) //busca na tabela para categoria
        {
            string sql = "SELECT * FROM Produto WHERE categoria LIKE '%"+ q +"%' ";

            return _conn.QueryAsync<Produto>(sql);

        }

    }
}
