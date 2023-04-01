using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows.Forms;

namespace Consumindo_WebApi_Produtos
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private static string _urlBase;
        private static AccessToken _accessToken;
        string _uri = "";
        Guid _codigoProduto = Guid.Empty;

        private async void btnObterProdutos_Click(object sender, EventArgs e)
        {
            try
            {
                _uri = txtURI.Text;
                var acessaAPI = new AcessaAPIService();
                List<Produto> produtos = await acessaAPI.GetAllProdutos(_uri, _accessToken);
                dgvDados.DataSource = produtos;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro : " + ex.Message);
            }
        }

        private async void btnProdutosPorId_Click(object sender, EventArgs e)
        {
            BindingSource bsDados = new BindingSource();
            InputBox();

            if (_codigoProduto != Guid.Empty)
            {
                try
                {
                    _uri = txtURI.Text + "/" + _codigoProduto;
                    var acessaAPI = new AcessaAPIService();
                    Produto produto = await acessaAPI.GetProdutoById(_uri, _accessToken);
                    bsDados.DataSource = produto;
                    dgvDados.DataSource = bsDados;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro : " + ex.Message);
                }
            }
        }

        private async void btnIncluirProduto_Click(object sender, EventArgs e)
        {
            Random randNum = new Random();
            Produto prod = new Produto();
            prod.Nome = "Novo Produto " + DateTime.Now.Second.ToString();
            prod.Descricao = "Novo Produto descricao " + DateTime.Now.Second.ToString();
            prod.CategoriaId = Guid.NewGuid();
            prod.ImagemUrl = "novaImagem" + DateTime.Now.Second.ToString() + ".jpg";
            prod.Preco = randNum.Next(100);
            _uri = txtURI.Text;

            try
            {
                var acessaAPI = new AcessaAPIService();
                var resultado = await acessaAPI.AddProduto(_uri, _accessToken, prod);
                MessageBox.Show(resultado.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro : " + ex.Message);
            }
        }

        private async void btnAtualizaProduto_Click(object sender, EventArgs e)
        {
            Random randNum = new Random();
            Produto prod = new Produto();
            prod.Descricao = "Novo Produto descricao alterada " + DateTime.Now.Second.ToString();
            prod.Nome = "Novo Produto alterado" + DateTime.Now.Second.ToString();
            prod.CategoriaId = Guid.NewGuid();
            prod.ImagemUrl = "novo alterado" + DateTime.Now.Second.ToString() + ".jpg";
            prod.Preco = randNum.Next(100); // atualizando o preço do produto
            InputBox();

            if (_codigoProduto != Guid.Empty)
            {
                prod.Id = _codigoProduto;
                _uri = txtURI.Text + "/" + prod.Id;
                try
                {
                    var acessaAPI = new AcessaAPIService();
                    var resultado = await acessaAPI.UpdateProduto(_uri, _accessToken, prod);
                    MessageBox.Show(resultado.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro : " + ex.Message);
                }
            }
        }

        private async void btnDeletarProduto_Click(object sender, EventArgs e)
        {
            _uri = txtURI.Text;
            InputBox();

            if (_codigoProduto != Guid.Empty)
            {
                try
                {
                    var acessaAPI = new AcessaAPIService();
                    var resultado = await acessaAPI.DeleteProduto(_uri, _accessToken, _codigoProduto);
                    MessageBox.Show(resultado.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro : " + ex.Message);
                }
            }
        }

        private void InputBox()
        {
            /* usando a função VB.Net para exibir um prompt para o usuário informar a senha */
            string Prompt = "Informe o código do Produto.";
            string Titulo = "www.google.net";
            string Resultado = Microsoft.VisualBasic.Interaction.InputBox(Prompt, Titulo, "79c7476b-c022-11ed-b61c-0242ac130002", 600, 350);
            /* verifica se o resultado é uma string vazia o que indica que foi cancelado. */
            _codigoProduto = Resultado != "" ? Guid.Parse(Resultado) : Guid.Empty;
        }

        private void BtnAutenticar_Click(object sender, EventArgs e)
        {
            _urlBase = ConfigurationManager.AppSettings["UrlBase"];
            var email = ConfigurationManager.AppSettings["UserID"];
            var password = ConfigurationManager.AppSettings["AccessKey"];
            var confirmPassword = password;

            var urlbase = $"{_urlBase}autoriza/login";

            using (var client = new HttpClient())
            {
                string conteudo = "";

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Envio da requisição a fim de autenticar e obter o token de acesso
                HttpResponseMessage respToken = client.PostAsync(urlbase, new StringContent(JsonConvert.SerializeObject(new { email, password, confirmPassword }), Encoding.UTF8, "application/json")).Result;

                try
                {
                    conteudo = respToken.Content.ReadAsStringAsync().Result;
                    btnProdutosPorId.Enabled = true;
                    btnObterProdutos.Enabled = true;
                    btnIncluirProduto.Enabled = true;
                    btnDeletarProduto.Enabled = true;
                    btnAtualizaProduto.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro " + ex.Message);
                    throw ex;
                }

                if (respToken.StatusCode == HttpStatusCode.OK)
                {
                    _accessToken = JsonConvert.DeserializeObject<AccessToken>(conteudo);

                    if (_accessToken.Authenticated)
                    {
                        // Associar o token aos headers do objeto do tipo HttpClient
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken.Token);
                        MessageBox.Show($"token JWT Autenticado ");
                    }
                    else
                    {
                        MessageBox.Show("Falha na autenticação");
                    }
                }
            }
        }
    }
}
