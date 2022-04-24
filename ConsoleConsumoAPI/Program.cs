using ConsoleConsumoAPI.Models;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace ConsoleConsumoAPI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            CadastrarProduto();
            ListarProdutos();
        }
        public static async void CadastrarProduto()
        {
            string urlApi = "http://localhost:4200/Create";
            try
            {
                using (var cliente = new HttpClient())
                {
                    var produto = new Produto();
                    produto.Descricao = "Produto - teste" + DateTime.Now.ToString();
                    produto.DataCriacao = DateTime.Now;

                    string jsonObjeto = JsonConvert.SerializeObject(produto);
                    var content = new StringContent(jsonObjeto, Encoding.UTF8, "application/json");
                                        
                    var resposta = cliente.PostAsync(urlApi, content);
                    resposta.Wait();

                    if (resposta.Result.IsSuccessStatusCode)
                    {
                        var retorno = resposta.Result.Content.ReadAsStringAsync();

                        var produtoCriado = JsonConvert.DeserializeObject<Produto>(retorno.Result);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public static async void ListarProdutos()
        {
            string urlApi = "http://localhost:4200/List";
            try
            {
                using (var cliente = new HttpClient())
                {
                    var resposta = cliente.GetStringAsync(urlApi);
                    resposta.Wait();

                    var retorno = JsonConvert.DeserializeObject<Produto[]>(resposta.Result).ToList();
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
    
}
