using HtmlAgilityPack;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CMS.HTML.Console
{
    class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }

    class Produto
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
    }

    class Program
    {
        static private string ProxyIp = "10.10.20.254";
        static private int ProxyPort = 8080;
        static private string ProxyUser = "cmartins";
        static private string ProxyPass = "#Chrs2387";

        static void Main(string[] args)
        {
            System.Console.WriteLine("Inicio...");
            System.Console.WriteLine("");
            try
            {

                // TesteDownloadImg();
                // TesteTypeCast();
                // GetTextFromPDF();
                DoWorkAsync();

            }
            catch (Exception ex)
            {
                System.Console.WriteLine("");
                System.Console.WriteLine($"Erro: {ex.Message}");
            }
            finally
            {
                System.Console.WriteLine("");
                System.Console.WriteLine("Fim...");
                System.Console.ReadLine();
            }
        }

        public static string JDRemoveNumeros(string value)
        {
            //return Regex.Replace(value, @"\d", string.Empty);,
            //return Regex.Replace(value, @"[\d-]", string.Empty);
            return Regex.Replace(value, @"[0-9\-]", string.Empty);
        }

        private static async Task TesteDownloadImg()
        {

            //https://static.guiainvest.com.br/Images/Ativo/UCAS_120.gif
            //https://static.guiainvest.com.br/Images/Ativo/ITSA_120.gif
            //https://static.guiainvest.com.br/Images/Ativo/CCRO_120.gif

            var lstAtivos = CriarListaAtivos();

            //lstAtivos.Clear();
            //lstAtivos.Add("ITSA4");
            //lstAtivos.Add("CCRO3");
            //lstAtivos.Add("EGIE3");
            //lstAtivos.Add("LIGT3");

            foreach (var codigo in lstAtivos)
            {
                var codigoResumedo = JDRemoveNumeros(codigo.ToUpper());
                var path = $@"C:\Users\CHRIS\Desktop\TnB Ativos Imagens\{codigoResumedo}.gif";
                if (!File.Exists(path))
                {
                    using (var wrGETURL = new WebClient())
                    {
                        wrGETURL.Proxy = WebProxy.GetDefaultProxy();
                        wrGETURL.Proxy.Credentials = CredentialCache.DefaultCredentials;
                        wrGETURL.DownloadFileAsync(new Uri($"https://static.guiainvest.com.br/Images/Ativo/{codigoResumedo}_120.gif"), path);
                    }
                }
            }





            //         var imageBytes = await _httpClient.GetByteArrayAsync(uri);
            //         await File.WriteAllBytesAsync(path, imageBytes);

            //         Stream stream = client.OpenRead(imageUrl);
            //         Bitmap bitmap; bitmap = new Bitmap(stream);
            //         if (bitmap != null)
            //             bitmap.Save(filename, format);
            //     }
            //     stream.Flush();
            // stream.Close();

            // byte[] data = webClient.DownloadData("https://fbcdn-sphotos-h-a.akamaihd.net/hphotos-ak-xpf1/v/t34.0-12/10555140_10201501435212873_1318258071_n.jpg?oh=97ebc03895b7acee9aebbde7d6b002bf&oe=53C9ABB0&__gda__=1405685729_110e04e71d9");
            //using (MemoryStream mem = new MemoryStream(data)) 
            //{
            //    using (var yourImage = Image.FromStream(mem)) 
            //    { 
            //        yourImage.Save("path_to_your_file.png", ImageFormat.Png) ; 
            //    }
            //} 







        }

        private static void TesteTypeCast()
        {

            dynamic data = null;

            data = new Pessoa();
            data.Id = 10;
            data.Nome = "Pessoa";
            System.Console.WriteLine($"{data.Id.ToString()} - {data.Nome} ");

            data = new Produto();
            data.Codigo = 20;
            data.Descricao = "Produto";
            System.Console.WriteLine($"{data.Codigo.ToString()} - {data.Descricao}");
        }

        private static string GetTextFromPDF()
        {
            var text = new StringBuilder(); //  string text = string.Empty;
            using (PdfReader reader = new PdfReader(@"C:\Users\cmartins\Desktop\2020.02.pdf"))
            {
                for (int iPage = 1; iPage <= reader.NumberOfPages; iPage++)
                    text.Append(PdfTextExtractor.GetTextFromPage(reader, iPage));
                reader.Close();
            }
            System.Console.WriteLine(text.ToString());
            return text.ToString();
        }

        public static async Task DoWorkAsync()
        {

            var lstAtivos = CriarListaAtivos();

            lstAtivos.Clear();
            lstAtivos.Add("ITSA4");
            //lstAtivos.Add("CCRO3");
            //lstAtivos.Add("EGIE3");

            var listOfTasks = new List<Task>();

            var iAtivo = 0;
            foreach (var codigo in lstAtivos)
                listOfTasks.Add(GetPageSource01(codigo: codigo, iAtivo: iAtivo++, address: $"https://br.financas.yahoo.com/quote/{codigo.ToUpper()}.SA/")); // GetPageSource02

            //var stopwatch = new Stopwatch();
            //stopwatch.Reset();
            //stopwatch.Start();
            //System.Console.WriteLine("Task.WhenAll");
            Task.WaitAll(listOfTasks.ToArray()); // await Task.WhenAll(listOfTasks);
            //stopwatch.Stop();
            //System.Console.WriteLine($"Task.WhenAll - Tempo Total:  {stopwatch.Elapsed}");

        }

        private static async Task DoGetPageAsync(string codigo, int iAtivo)
        {
            // await Task.Delay(1000 * i);  // System.Threading.Thread.Sleep(1000);
            //var url = $"https://br.financas.yahoo.com/quote/{codigo.ToUpper()}.SA/";
            //var result = await GetPageSource01(codigo: codigo, iAtivo: iAtivo, address: url); // GetPageSource02(codigo: codigo, iAtivo: iAtivo, address: url);
            // System.Console.WriteLine(result);
        }

        private static async Task<string> GetPageSource01(string codigo, int iAtivo, string address)
        {
            var htmlSource = string.Empty;
            try
            {
                using (var wrGETURL = new System.Net.WebClient())
                {

                    var stopwatch = new Stopwatch();
                    stopwatch.Reset();
                    stopwatch.Start();

                    //-------------------------------------------------------------
                    //-------------------------------------------------------------

                    var stopwatch1 = new Stopwatch();
                    stopwatch1.Reset();
                    stopwatch1.Start();

                    wrGETURL.Proxy = WebProxy.GetDefaultProxy(); // new WebProxy(ProxyIp, ProxyPort); // null;
                    wrGETURL.Proxy.Credentials = CredentialCache.DefaultCredentials; // new System.Net.NetworkCredential(ProxyUser, ProxyPass);
                    htmlSource = await wrGETURL.DownloadStringTaskAsync(new Uri(address)); // DownloadStringAsync

                    stopwatch1.Stop();
                    System.Console.WriteLine($"DownloadStringTaskAsync - Ativo: {codigo} Tempo:  {stopwatch1.Elapsed}");

                    //-------------------------------------------------------------
                    //-------------------------------------------------------------

                    var stopwatch2 = new Stopwatch();
                    stopwatch2.Reset();
                    stopwatch2.Start();

                    //    //https://html-agility-pack.net/select-nodes
                    HtmlDocument htmlDoc = new HtmlDocument();
                    htmlDoc.LoadHtml(htmlSource);
                    var preco = htmlDoc.DocumentNode.SelectNodes(@"//html//body//div[1]//div//div//div[1]//div//div[2]//div//div//div[4]//div//div//div//div[3]//div//div//span[1]").First().InnerHtml; // InnerText
                    var variacao = htmlDoc.DocumentNode.SelectNodes(@"//html//body//div[1]//div//div//div[1]//div//div[2]//div//div//div[4]//div//div//div//div[3]//div//div//span[2]").First().InnerHtml; // InnerText

                    stopwatch2.Stop();
                    System.Console.WriteLine($"HtmlDocument - Ativo: {codigo} Tempo:  {stopwatch2.Elapsed}");

                    //-------------------------------------------------------------
                    //-------------------------------------------------------------

                    //var stopwatch3 = new Stopwatch();
                    //stopwatch3.Reset();
                    //stopwatch3.Start();

                    //string pattern = @"//html//body//div[1]//div//div//div[1]//div//div[2]//div//div//div[4]//div//div//div//div[3]//div//div//span[1]";
                    //// @"//html//body//div[1]//div//div//div[1]//div//div[2]//div//div//div[4]//div//div//div//div[3]//div//div//span[2]"

                    //MatchCollection matches = Regex.Matches(htmlSource, pattern);
                    //if (matches.Count > 0)
                    //    foreach (Match m in matches)
                    //        System.Console.WriteLine($"Inner DIV: {m.Groups[2]}");

                    //Match m2 = Regex.Match(htmlSource, pattern);
                    //if (m2.Success)
                    //    System.Console.WriteLine($"Inner DIV2: {m2.Groups[1].Value}");

                    //stopwatch3.Stop();
                    //System.Console.WriteLine($"HtmlDocument - Ativo: {codigo} Tempo:  {stopwatch3.Elapsed}");

                    //-------------------------------------------------------------
                    //-------------------------------------------------------------

                    //                    Fim Coletar Dados Cotacao... (Tempo Decorrido: 00:44:20:057 )
                    //559 / 559 - Procesando YDUQ3 - VlrFechamento: 51,89 - fVlrAnterior: 51,3 - VariacaoPerc: 1,15 - VariacaoVlr: 0,59... (Tempo Decorrido: 00:44:20:040 )


                    //  fVlrFechamento:= FncStringParaExtended(objSiteYahoo.PrecoFechamento);
                    //      if (fVlrFechamento > 0) then
                    //         Begin
                    //  fVlrAnterior:= fVlrFechamento;
                    //  fVariacaoPerc:= FncStringParaExtended(objSiteYahoo.VariacaoPerc);
                    //  fVariacaoVlr:= FncStringParaExtended(objSiteYahoo.VariacaoVlr);
                    //      End
                    //else
                    //      fVlrFechamento:= fVlrFechamento;

                    //      if (fVariacaoVlr > 0) then
                    //                      fVlrAnterior := fVlrFechamento - fVariacaoVlr;
                    //      if (fVariacaoVlr < 0) then
                    //        fVlrAnterior := fVlrFechamento + Abs(fVariacaoVlr);

                    //      if (fVlrFechamento > 0) And(fVlrAnterior > 0) Then
                    //      LstCotacao.Add(TAtivoCotacao.Create(FormatDateTime('YYYYMMDD', Now), iIdAtivo, fVlrAberuta, fVlrFechamento, fVlrMaxima, fVlrMinima, fVlrAnterior, fVariacaoPerc, FormatDateTime('YYYYMMDDHHNNSSZZZ', Now)));

                    //-------------------------------------------------------------
                    //-------------------------------------------------------------

                    stopwatch.Stop();
                    System.Console.WriteLine($"Nro: {iAtivo.ToString()} - Ativo: {codigo} - Preço: {preco} - Variação: {variacao} - Tempo:  {stopwatch.Elapsed}");
                }
            }
            catch (WebException ex)
            {
                System.Console.WriteLine($"Erro GetPage(Web): {ex.Message}");
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Erro GetPage: {ex.Message}");
            }
            return htmlSource;
        }

        private static string GetPageSource02(string codigo, int iAtivo, string address)
        {
            var htmlSource = string.Empty;
            try
            {

                HttpWebRequest wrGETURL = (HttpWebRequest)WebRequest.Create(new Uri(address));
                wrGETURL.Timeout = 360000;
                wrGETURL.ReadWriteTimeout = 360000;
                wrGETURL.AllowAutoRedirect = true;
                wrGETURL.MaximumAutomaticRedirections = 300;
                wrGETURL.Method = "GET"; //  "POST";
                wrGETURL.KeepAlive = false;
                wrGETURL.ContentType = "application/x-www-form-urlencoded";

                WebProxy webproxy = WebProxy.GetDefaultProxy(); // new WebProxy(ProxyIp, ProxyPort);
                webproxy.BypassProxyOnLocal = true;
                wrGETURL.Proxy = webproxy;
                wrGETURL.Proxy.Credentials = CredentialCache.DefaultCredentials; // new System.Net.NetworkCredential(ProxyUser, ProxyPass);

                // CookieContainer cookieContainer = new CookieContainer();
                //wrGETURL.CookieContainer = cookieContainer;

                //using (WebResponse MyResponse_PageSource = wrGETURL.GetResponse())
                //{
                //    str_PageSource = new StreamReader(MyResponse_PageSource.GetResponseStream(), System.Text.Encoding.UTF8);
                //    pagesource1 = str_PageSource.ReadToEnd();
                //    success = true;
                //}

                // response.Content.ReadAsStringAsync().Result;

                HttpWebResponse streamResponse = (HttpWebResponse)wrGETURL.GetResponse(); // (HttpWebResponse)wrGETURL.GetResponse().GetResponseStream();
                try
                {
                    //Console.WriteLine (streamResponse.StatusDescription);
                    var streamRead = streamResponse.GetResponseStream();
                    try
                    {
                        if (streamRead != null)
                        {
                            using (var strReader = new StreamReader(streamRead))
                            {
                                htmlSource = strReader.ReadToEnd();
                                //var pageDocument = new HtmlDocument();
                                //pageDocument.LoadHtml(htmlSource);
                                //var headlineText = pageDocument.DocumentNode.SelectSingleNode("(//div[contains(@class,'pb-f-homepage-hero')]//h3)[1]").InnerText;


                                //using (var memstream = new MemoryStream())
                                //{
                                //    var buffer = new byte[512];
                                //    var bytesRead = default(int);
                                //    while ((bytesRead = strReader.BaseStream.Read(buffer, 0, buffer.Length)) > 0)
                                //        memstream.Write(buffer, 0, bytesRead);
                                //}

                                //using (var memstream = new MemoryStream())
                                //{
                                //    strReader.BaseStream.CopyTo(memstream);
                                //}

                                strReader.Close();
                            }
                        }
                    }
                    finally
                    {
                        streamRead.Close();
                    }
                }
                finally
                {
                    streamResponse.Close();
                }
            }
            catch (WebException ex)
            {
                System.Console.WriteLine($"Erro GetPage(Web): {ex.Message}");
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Erro GetPage: {ex.Message}");
            }
            return htmlSource;
        }

        private static List<string> CriarListaAtivos()
        {
            var lstAtivos = new List<string>();
            lstAtivos.Add("AALR3");
            lstAtivos.Add("ABCB3");
            lstAtivos.Add("ABCB4");
            lstAtivos.Add("ABEV3");
            lstAtivos.Add("ADHM3");
            lstAtivos.Add("AELP3");
            lstAtivos.Add("AFLT3");
            lstAtivos.Add("AFLU3");
            lstAtivos.Add("AFLU5");
            lstAtivos.Add("AGRO3");
            lstAtivos.Add("AHEB3");
            lstAtivos.Add("AHEB5");
            lstAtivos.Add("AHEB6");
            lstAtivos.Add("ALPA3");
            lstAtivos.Add("ALPA4");
            lstAtivos.Add("ALSC");
            lstAtivos.Add("ALSO3");
            lstAtivos.Add("ALUP11");
            lstAtivos.Add("ALUP3");
            lstAtivos.Add("ALUP4");
            lstAtivos.Add("AMAR3");
            lstAtivos.Add("ANIM3");
            lstAtivos.Add("APER3");
            lstAtivos.Add("APTI4");
            lstAtivos.Add("ARZZ3");
            lstAtivos.Add("ATOM3");
            lstAtivos.Add("AZEV3");
            lstAtivos.Add("AZEV4");
            lstAtivos.Add("AZUL4");
            lstAtivos.Add("B3SA3");
            lstAtivos.Add("BAHI11");
            lstAtivos.Add("BAHI3");
            lstAtivos.Add("BAHI4");
            lstAtivos.Add("BAHI5");
            lstAtivos.Add("BALM3");
            lstAtivos.Add("BALM4");
            lstAtivos.Add("BAUH3");
            lstAtivos.Add("BAUH4");
            lstAtivos.Add("BAZA3");
            lstAtivos.Add("BBAS11");
            lstAtivos.Add("BBAS12");
            lstAtivos.Add("BBAS3");
            lstAtivos.Add("BBDC3");
            lstAtivos.Add("BBDC4");
            lstAtivos.Add("BBRK3");
            lstAtivos.Add("BBSE3");
            lstAtivos.Add("BDLL3");
            lstAtivos.Add("BDLL4");
            lstAtivos.Add("BEEF3");
            lstAtivos.Add("BEES3");
            lstAtivos.Add("BEES4");
            lstAtivos.Add("BGIP3");
            lstAtivos.Add("BGIP4");
            lstAtivos.Add("BIDI11");
            lstAtivos.Add("BIDI3");
            lstAtivos.Add("BIDI4");
            lstAtivos.Add("BIOM3");
            lstAtivos.Add("BIOM4");
            lstAtivos.Add("BKBR3");
            lstAtivos.Add("BMEB3");
            lstAtivos.Add("BMEB4");
            lstAtivos.Add("BMGB11");
            lstAtivos.Add("BMGB4");
            lstAtivos.Add("BMIN3");
            lstAtivos.Add("BMIN4");
            lstAtivos.Add("BMKS3");
            lstAtivos.Add("BNBR3");
            lstAtivos.Add("BNBR4");
            lstAtivos.Add("BOBR3");
            lstAtivos.Add("BOBR4");
            lstAtivos.Add("BPAC11");
            lstAtivos.Add("BPAC3");
            lstAtivos.Add("BPAC5");
            lstAtivos.Add("BPAN4");
            lstAtivos.Add("BPAR3");
            lstAtivos.Add("BPAT3");
            lstAtivos.Add("BPHA3");
            lstAtivos.Add("BRAP3");
            lstAtivos.Add("BRAP4");
            lstAtivos.Add("BRDT3");
            lstAtivos.Add("BRFS3");
            lstAtivos.Add("BRGE11");
            lstAtivos.Add("BRGE12");
            lstAtivos.Add("BRGE3");
            lstAtivos.Add("BRGE5");
            lstAtivos.Add("BRGE6");
            lstAtivos.Add("BRGE7");
            lstAtivos.Add("BRGE8");
            lstAtivos.Add("BRIN3");
            lstAtivos.Add("BRIV3");
            lstAtivos.Add("BRIV4");
            lstAtivos.Add("BRKM3");
            lstAtivos.Add("BRKM5");
            lstAtivos.Add("BRKM6");
            lstAtivos.Add("BRML3");
            lstAtivos.Add("BRPR3");
            lstAtivos.Add("BRSR3");
            lstAtivos.Add("BRSR4");
            lstAtivos.Add("BRSR5");
            lstAtivos.Add("BRSR6");
            lstAtivos.Add("BSEV3");
            lstAtivos.Add("BSLI3");
            lstAtivos.Add("BSLI4");
            lstAtivos.Add("BTOW3");
            lstAtivos.Add("BTTL3");
            lstAtivos.Add("BTTL4");
            lstAtivos.Add("CALI3");
            lstAtivos.Add("CALI4");
            lstAtivos.Add("CAMB3");
            lstAtivos.Add("CAMB4");
            lstAtivos.Add("CAML3");
            lstAtivos.Add("CARD3");
            lstAtivos.Add("CASN3");
            lstAtivos.Add("CBEE3");
            lstAtivos.Add("CCPR3");
            lstAtivos.Add("CCRO3");
            lstAtivos.Add("CCXC3");
            lstAtivos.Add("CEAB3");
            lstAtivos.Add("CEBR3");
            lstAtivos.Add("CEBR5");
            lstAtivos.Add("CEBR6");
            lstAtivos.Add("CEDO3");
            lstAtivos.Add("CEDO4");
            lstAtivos.Add("CEEB3");
            lstAtivos.Add("CEEB5");
            lstAtivos.Add("CEED3");
            lstAtivos.Add("CEED4");
            lstAtivos.Add("CEGR3");
            lstAtivos.Add("CELP3");
            lstAtivos.Add("CELP5");
            lstAtivos.Add("CELP6");
            lstAtivos.Add("CELP7");
            lstAtivos.Add("CEPE3");
            lstAtivos.Add("CEPE5");
            lstAtivos.Add("CEPE6");
            lstAtivos.Add("CESP3");
            lstAtivos.Add("CESP4");
            lstAtivos.Add("CESP5");
            lstAtivos.Add("CESP6");
            lstAtivos.Add("CGAS3");
            lstAtivos.Add("CGAS5");
            lstAtivos.Add("CGRA3");
            lstAtivos.Add("CGRA4");
            lstAtivos.Add("CIEL3");
            lstAtivos.Add("CLSC3");
            lstAtivos.Add("CLSC4");
            lstAtivos.Add("CLSC5");
            lstAtivos.Add("CLSC6");
            lstAtivos.Add("CMIG3");
            lstAtivos.Add("CMIG4");
            lstAtivos.Add("CNTO3");
            lstAtivos.Add("COCE3");
            lstAtivos.Add("COCE5");
            lstAtivos.Add("COCE6");
            lstAtivos.Add("COGN3");
            lstAtivos.Add("CORR3");
            lstAtivos.Add("CORR4");
            lstAtivos.Add("CPFE3");
            lstAtivos.Add("CPLE3");
            lstAtivos.Add("CPLE5");
            lstAtivos.Add("CPLE6");
            lstAtivos.Add("CPRE3");
            lstAtivos.Add("CRDE3");
            lstAtivos.Add("CREM3");
            lstAtivos.Add("CREM4");
            lstAtivos.Add("CRFB3");
            lstAtivos.Add("CRIV3");
            lstAtivos.Add("CRIV4");
            lstAtivos.Add("CRPG3");
            lstAtivos.Add("CRPG5");
            lstAtivos.Add("CRPG6");
            lstAtivos.Add("CSAB3");
            lstAtivos.Add("CSAB4");
            lstAtivos.Add("CSAN3");
            lstAtivos.Add("CSMG3");
            lstAtivos.Add("CSNA3");
            lstAtivos.Add("CSRN3");
            lstAtivos.Add("CSRN5");
            lstAtivos.Add("CSRN6");
            lstAtivos.Add("CTKA3");
            lstAtivos.Add("CTKA4");
            lstAtivos.Add("CTNM3");
            lstAtivos.Add("CTNM4");
            lstAtivos.Add("CTSA3");
            lstAtivos.Add("CTSA4");
            lstAtivos.Add("CTSA8");
            lstAtivos.Add("CVCB3");
            lstAtivos.Add("CYRE3");
            lstAtivos.Add("CYRE4");
            lstAtivos.Add("CZLT33");
            lstAtivos.Add("DAGB33");
            lstAtivos.Add("DASA3");
            lstAtivos.Add("DIRR3");
            lstAtivos.Add("DMMO3");
            lstAtivos.Add("DOHL3");
            lstAtivos.Add("DOHL4");
            lstAtivos.Add("DTCY3");
            lstAtivos.Add("DTEX3");
            lstAtivos.Add("EALT3");
            lstAtivos.Add("EALT4");
            lstAtivos.Add("ECOR3");
            lstAtivos.Add("ECPR3");
            lstAtivos.Add("ECPR4");
            lstAtivos.Add("EEEL3");
            lstAtivos.Add("EEEL4");
            lstAtivos.Add("EGIE3");
            lstAtivos.Add("EKTR3");
            lstAtivos.Add("EKTR4");
            lstAtivos.Add("ELEK3");
            lstAtivos.Add("ELEK4");
            lstAtivos.Add("ELET3");
            lstAtivos.Add("ELET5");
            lstAtivos.Add("ELET6");
            lstAtivos.Add("ELPL3");
            lstAtivos.Add("ELPL4");
            lstAtivos.Add("ELPL");
            lstAtivos.Add("ELPL");
            lstAtivos.Add("EMAE3");
            lstAtivos.Add("EMAE4");
            lstAtivos.Add("EMBR3");
            lstAtivos.Add("ENAT3");
            lstAtivos.Add("ENBR3");
            lstAtivos.Add("ENEV3");
            lstAtivos.Add("ENGI11");
            lstAtivos.Add("ENGI3");
            lstAtivos.Add("ENGI4");
            lstAtivos.Add("ENMT3");
            lstAtivos.Add("ENMT4");
            lstAtivos.Add("EQTL3");
            lstAtivos.Add("ESTC1");
            lstAtivos.Add("ESTC");
            lstAtivos.Add("ESTC");
            lstAtivos.Add("ESTR3");
            lstAtivos.Add("ESTR4");
            lstAtivos.Add("ETER3");
            lstAtivos.Add("ETER4");
            lstAtivos.Add("EUCA3");
            lstAtivos.Add("EUCA4");
            lstAtivos.Add("EVEN3");
            lstAtivos.Add("EZTC3");
            lstAtivos.Add("FBMC3");
            lstAtivos.Add("FBMC4");
            lstAtivos.Add("FESA3");
            lstAtivos.Add("FESA4");
            lstAtivos.Add("FHER3");
            lstAtivos.Add("FIBR3");
            lstAtivos.Add("FIGE3");
            lstAtivos.Add("FIGE4");
            lstAtivos.Add("FJTA");
            lstAtivos.Add("FJTA");
            lstAtivos.Add("FLRY3");
            lstAtivos.Add("FRAS3");
            lstAtivos.Add("FRAS4");
            lstAtivos.Add("FRIO3");
            lstAtivos.Add("FRTA3");
            lstAtivos.Add("GBIO33");
            lstAtivos.Add("GEPA3");
            lstAtivos.Add("GEPA4");
            lstAtivos.Add("GFSA3");
            lstAtivos.Add("GGBR3");
            lstAtivos.Add("GGBR4");
            lstAtivos.Add("GNDI3");
            lstAtivos.Add("GOAU3");
            lstAtivos.Add("GOAU4");
            lstAtivos.Add("GOLL4");
            lstAtivos.Add("GPCP3");
            lstAtivos.Add("GPIV33");
            lstAtivos.Add("GRND3");
            lstAtivos.Add("GSHP3");
            lstAtivos.Add("GUAR3");
            lstAtivos.Add("GUAR");
            lstAtivos.Add("HAGA3");
            lstAtivos.Add("HAGA4");
            lstAtivos.Add("HAPV3");
            lstAtivos.Add("HBOR3");
            lstAtivos.Add("HBTS3");
            lstAtivos.Add("HBTS5");
            lstAtivos.Add("HBTS6");
            lstAtivos.Add("HETA3");
            lstAtivos.Add("HETA4");
            lstAtivos.Add("HGTX3");
            lstAtivos.Add("HGTX4");
            lstAtivos.Add("HOOT4");
            lstAtivos.Add("HYPE3");
            lstAtivos.Add("IBOV");
            lstAtivos.Add("IBXX");
            lstAtivos.Add("IDIV");
            lstAtivos.Add("IDNT3");
            lstAtivos.Add("IDVL1");
            lstAtivos.Add("IDVL3");
            lstAtivos.Add("IDVL4");
            lstAtivos.Add("IGBR3");
            lstAtivos.Add("IGBR5");
            lstAtivos.Add("IGBR6");
            lstAtivos.Add("IGSN3");
            lstAtivos.Add("IGTA3");
            lstAtivos.Add("INEP3");
            lstAtivos.Add("INEP4");
            lstAtivos.Add("IRBR3");
            lstAtivos.Add("ITEC");
            lstAtivos.Add("ITSA3");
            lstAtivos.Add("ITSA4");
            lstAtivos.Add("ITUB3");
            lstAtivos.Add("ITUB4");
            lstAtivos.Add("JBDU3");
            lstAtivos.Add("JBDU4");
            lstAtivos.Add("JBSS3");
            lstAtivos.Add("JFEN3");
            lstAtivos.Add("JHSF3");
            lstAtivos.Add("JOPA3");
            lstAtivos.Add("JOPA4");
            lstAtivos.Add("JPSA3");
            lstAtivos.Add("JSLG3");
            lstAtivos.Add("KEPL3");
            lstAtivos.Add("KLBN11");
            lstAtivos.Add("KLBN3");
            lstAtivos.Add("KLBN4");
            lstAtivos.Add("KROT11");
            lstAtivos.Add("KROT");
            lstAtivos.Add("KROT");
            lstAtivos.Add("LAME3");
            lstAtivos.Add("LAME4");
            lstAtivos.Add("LCAM3");
            lstAtivos.Add("LEVE3");
            lstAtivos.Add("LEVE4");
            lstAtivos.Add("LFFE3");
            lstAtivos.Add("LFFE4");
            lstAtivos.Add("LIGT3");
            lstAtivos.Add("LINX3");
            lstAtivos.Add("LIPR3");
            lstAtivos.Add("LIQO3");
            lstAtivos.Add("LIXC3");
            lstAtivos.Add("LIXC4");
            lstAtivos.Add("LLIS3");
            lstAtivos.Add("LOGG3");
            lstAtivos.Add("LOGN3");
            lstAtivos.Add("LPSB3");
            lstAtivos.Add("LREN3");
            lstAtivos.Add("LREN4");
            lstAtivos.Add("LUPA3");
            lstAtivos.Add("LUXM3");
            lstAtivos.Add("LUXM4");
            lstAtivos.Add("LWSA3");
            lstAtivos.Add("MAGG");
            lstAtivos.Add("MAPT3");
            lstAtivos.Add("MAPT4");
            lstAtivos.Add("MDIA3");
            lstAtivos.Add("MDNE3");
            lstAtivos.Add("MEAL3");
            lstAtivos.Add("MEND5");
            lstAtivos.Add("MEND6");
            lstAtivos.Add("MERC3");
            lstAtivos.Add("MERC4");
            lstAtivos.Add("MGEL3");
            lstAtivos.Add("MGEL4");
            lstAtivos.Add("MGLU3");
            lstAtivos.Add("MILS3");
            lstAtivos.Add("MLFT3");
            lstAtivos.Add("MLFT4");
            lstAtivos.Add("MMAQ3");
            lstAtivos.Add("MMAQ4");
            lstAtivos.Add("MMXM3");
            lstAtivos.Add("MNDL3");
            lstAtivos.Add("MNDL4");
            lstAtivos.Add("MNPR3");
            lstAtivos.Add("MNPR4");
            lstAtivos.Add("MOAR3");
            lstAtivos.Add("MOVI3");
            lstAtivos.Add("MPLU");
            lstAtivos.Add("MRFG3");
            lstAtivos.Add("MRVE3");
            lstAtivos.Add("MSPA3");
            lstAtivos.Add("MSPA4");
            lstAtivos.Add("MTIG3");
            lstAtivos.Add("MTIG4");
            lstAtivos.Add("MTRE3");
            lstAtivos.Add("MTSA3");
            lstAtivos.Add("MTSA4");
            lstAtivos.Add("MULT3");
            lstAtivos.Add("MWET3");
            lstAtivos.Add("MWET4");
            lstAtivos.Add("MYPK3");
            lstAtivos.Add("MYPK4");
            lstAtivos.Add("NAFG3");
            lstAtivos.Add("NAFG4");
            lstAtivos.Add("NATU3");
            lstAtivos.Add("NEOE3");
            lstAtivos.Add("NORD3");
            lstAtivos.Add("NTCO3");
            lstAtivos.Add("NUTR3");
            lstAtivos.Add("ODPV3");
            lstAtivos.Add("OFSA3");
            lstAtivos.Add("OGXP3");
            lstAtivos.Add("OIBR3");
            lstAtivos.Add("OIBR4");
            lstAtivos.Add("OMGE3");
            lstAtivos.Add("OSXB3");
            lstAtivos.Add("PARD3");
            lstAtivos.Add("PATI3");
            lstAtivos.Add("PATI4");
            lstAtivos.Add("PCAR3");
            lstAtivos.Add("PCAR4");
            lstAtivos.Add("PCAR");
            lstAtivos.Add("PDGR3");
            lstAtivos.Add("PEAB3");
            lstAtivos.Add("PEAB4");
            lstAtivos.Add("PETR3");
            lstAtivos.Add("PETR4");
            lstAtivos.Add("PFRM3");
            lstAtivos.Add("PINE");
            lstAtivos.Add("PINE4");
            lstAtivos.Add("PLAS3");
            lstAtivos.Add("PMAM3");
            lstAtivos.Add("PMAM4");
            lstAtivos.Add("PNVL3");
            lstAtivos.Add("PNVL4");
            lstAtivos.Add("POMO3");
            lstAtivos.Add("POMO4");
            lstAtivos.Add("POSI3");
            lstAtivos.Add("PRBC4");
            lstAtivos.Add("PRIO3");
            lstAtivos.Add("PRML3");
            lstAtivos.Add("PRNR3");
            lstAtivos.Add("PSSA3");
            lstAtivos.Add("PTBL3");
            lstAtivos.Add("PTBL4");
            lstAtivos.Add("PTNT3");
            lstAtivos.Add("PTNT4");
            lstAtivos.Add("QGEP");
            lstAtivos.Add("QUAL3");
            lstAtivos.Add("RADL3");
            lstAtivos.Add("RAIL3");
            lstAtivos.Add("RANI3");
            lstAtivos.Add("RANI4");
            lstAtivos.Add("RAPT3");
            lstAtivos.Add("RAPT4");
            lstAtivos.Add("RCSL3");
            lstAtivos.Add("RCSL4");
            lstAtivos.Add("RDNI3");
            lstAtivos.Add("REDE3");
            lstAtivos.Add("REDE4");
            lstAtivos.Add("RENT3");
            lstAtivos.Add("RLOG3");
            lstAtivos.Add("RNEW11");
            lstAtivos.Add("RNEW3");
            lstAtivos.Add("RNEW4");
            lstAtivos.Add("ROMI3");
            lstAtivos.Add("ROMI4");
            lstAtivos.Add("RPAD3");
            lstAtivos.Add("RPAD5");
            lstAtivos.Add("RPAD6");
            lstAtivos.Add("RPMG3");
            lstAtivos.Add("RPMG4");
            lstAtivos.Add("RSID3");
            lstAtivos.Add("RSUL4");
            lstAtivos.Add("SANB11");
            lstAtivos.Add("SANB3");
            lstAtivos.Add("SANB4");
            lstAtivos.Add("SAPR11");
            lstAtivos.Add("SAPR3");
            lstAtivos.Add("SAPR4");
            lstAtivos.Add("SBSP3");
            lstAtivos.Add("SCAR3");
            lstAtivos.Add("SCAR4");
            lstAtivos.Add("SEDU");
            lstAtivos.Add("SEER3");
            lstAtivos.Add("SGPS3");
            lstAtivos.Add("SHOW3");
            lstAtivos.Add("SHUL3");
            lstAtivos.Add("SHUL4");
            lstAtivos.Add("SLCE3");
            lstAtivos.Add("SLED3");
            lstAtivos.Add("SLED4");
            lstAtivos.Add("SMLL");
            lstAtivos.Add("SMLS3");
            lstAtivos.Add("SMTO3");
            lstAtivos.Add("SNSL");
            lstAtivos.Add("SNSY5");
            lstAtivos.Add("SOND3");
            lstAtivos.Add("SOND5");
            lstAtivos.Add("SOND6");
            lstAtivos.Add("SPRI3");
            lstAtivos.Add("SPRI5");
            lstAtivos.Add("SPRI6");
            lstAtivos.Add("SQIA3");
            lstAtivos.Add("SSBR");
            lstAtivos.Add("STBP11");
            lstAtivos.Add("STBP3");
            lstAtivos.Add("SULA11");
            lstAtivos.Add("SULA3");
            lstAtivos.Add("SULA4");
            lstAtivos.Add("SUZB3");
            lstAtivos.Add("SUZB5");
            lstAtivos.Add("SUZB6");
            lstAtivos.Add("TAEE11");
            lstAtivos.Add("TAEE3");
            lstAtivos.Add("TAEE4");
            lstAtivos.Add("TASA3");
            lstAtivos.Add("TASA4");
            lstAtivos.Add("TCNO3");
            lstAtivos.Add("TCNO4");
            lstAtivos.Add("TCSA3");
            lstAtivos.Add("TECN3");
            lstAtivos.Add("TEKA3");
            lstAtivos.Add("TEKA3");
            lstAtivos.Add("TEKA4");
            lstAtivos.Add("TEKA4");
            lstAtivos.Add("TELB3");
            lstAtivos.Add("TELB4");
            lstAtivos.Add("TEND3");
            lstAtivos.Add("TESA3");
            lstAtivos.Add("TGMA3");
            lstAtivos.Add("TIET11");
            lstAtivos.Add("TIET3");
            lstAtivos.Add("TIET4");
            lstAtivos.Add("TIMP3");
            lstAtivos.Add("TKNO4");
            lstAtivos.Add("TOTS3");
            lstAtivos.Add("TOYB");
            lstAtivos.Add("TOYB");
            lstAtivos.Add("TPIS3");
            lstAtivos.Add("TRIS3");
            lstAtivos.Add("TRPL3");
            lstAtivos.Add("TRPL4");
            lstAtivos.Add("TRPN");
            lstAtivos.Add("TUPY3");
            lstAtivos.Add("TUPY4");
            lstAtivos.Add("TXRX3");
            lstAtivos.Add("TXRX4");
            lstAtivos.Add("UCAS3");
            lstAtivos.Add("UGPA3");
            lstAtivos.Add("UGPA4");
            lstAtivos.Add("UNIP3");
            lstAtivos.Add("UNIP5");
            lstAtivos.Add("UNIP6");
            lstAtivos.Add("USIM3");
            lstAtivos.Add("USIM5");
            lstAtivos.Add("USIM6");
            lstAtivos.Add("VALE3");
            lstAtivos.Add("VALE5");
            lstAtivos.Add("VIVA3");
            lstAtivos.Add("VIVR3");
            lstAtivos.Add("VIVT3");
            lstAtivos.Add("VIVT4");
            lstAtivos.Add("VLID3");
            lstAtivos.Add("VULC3");
            lstAtivos.Add("VULC4");
            lstAtivos.Add("VVAR11");
            lstAtivos.Add("VVAR3");
            lstAtivos.Add("VVAR4");
            lstAtivos.Add("WEGE3");
            lstAtivos.Add("WEGE4");
            lstAtivos.Add("WHRL3");
            lstAtivos.Add("WHRL4");
            lstAtivos.Add("WIZS3");
            lstAtivos.Add("WLMM3");
            lstAtivos.Add("WLMM4");
            lstAtivos.Add("WSON33");
            lstAtivos.Add("YDUQ3");
            return lstAtivos;
        }

    }
}
