using Domain;
using Data;
using System;
using System.Data.Entity;
using System.Linq;
using System.Diagnostics;

namespace Application
{
    class Program
    {
        static TraceSource source1 = new TraceSource("MyProgram.Source1");

        static void Main(string[] args)
        {
            Console.WriteLine("Inicio");
                                   
            #region CMS Teste Context

            //Console.WriteLine("new ProductDbContext");
            var context = new ProductDbContext();
            Console.WriteLine("");

            #endregion
                       
            #region CMS Teste Loja Insert/Update/Delete

            //var loja = new Loja() { Id = 10, Nome = "Loja 003", Descricao = "" };
            //context.Lojas.Add(loja);
            //context.SaveChanges();

            // context.Lojas.Update(loja);
            // or
            // context.Entry(loja).State = EntityState.Modified;
            // context.SaveChanges();

            // var loja = context.Lojas.Find(3);
            // if (loja != null)
            // {
            //    context.Lojas.Remove(loja);
            //    context.SaveChanges();
            // }

            #endregion

            #region CMS Teste Loja Find

            //var itemLoja = context.Lojas.FirstOrDefault(item => item.Id == 6);
            //var itemLoja = context.Lojas.Find(6);
            //if (itemLoja != null)
            //{
            //    Console.WriteLine($"Loja.Id: {itemLoja.Id} - Loja.Nome: {itemLoja.Nome}");
            //}
            //else
            //{
            //    Console.WriteLine("Loja Não Localizada...");
            //}
            //Console.WriteLine("");

            #endregion

            #region CMS Teste Loja ToList

            //Console.WriteLine("context.Lojas.GetAll");
            //Console.WriteLine("Lojas");
            //var lojas = context.Lojas.ToList();
            //if (lojas != null)
            //{
            //    foreach (var item in lojas)
            //    {
            //        Console.WriteLine($"Loja.Id: {item.Id} - Loja.Nome: {item.Nome}");
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("Lista de Produto Vazia...");
            //}
            //Console.WriteLine("");

            #endregion
                        
            #region CMS Teste Marca Insert/Update/Delete

            //var marca = new Marca() { Id = 10, Nome = "Marca 003" };
            //context.Marcas.Add(marca);
            //context.SaveChanges();

            // context.Marcas.Update(marca);
            // or
            // context.Entry(marca).State = EntityState.Modified;
            // context.SaveChanges();

            // var marca = context.Marcas.Find(3);
            // if (marca != null)
            // {
            //    context.Marcas.Remove(marca);
            //    context.SaveChanges();
            // }

            #endregion

            #region CMS Teste Marca Find

            //var itemMarca = context.Lojas.FirstOrDefault(item => item.Id == 6);
            //var itemMarca = context.Lojas.Find(2);
            //if (itemMarca != null)
            //{
            //    Console.WriteLine($"Marca.Id: {itemMarca.Id} - Marca.Nome: {itemMarca.Nome}");
            //    itemMarca.Produtos.ToList().ForEach(item => Console.WriteLine($"Prod.Id: {item.Id} - Prod.Nome: {item.Nome} - Prod.Valor: {item.Valor}") );
            //}
            //else
            //{
            //    Console.WriteLine("Marca Não Localizada...");
            //}
            //Console.WriteLine("");

            #endregion

            #region CMS Teste Marca ToList

            //Console.WriteLine("Marcas");
            //var marcas = context.Marcas.ToList();
            //if (marcas != null)
            //{
            //    foreach (var item in marcas)
            //    {
            //        Console.WriteLine($"Marca.Id: {item.Id} - Marca.Nome: {item.Nome}");
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("Lista de Marca Vazia...");
            //}
            //Console.WriteLine("");

            #endregion
            
            #region CMS Teste Produto Insert/Update/Delete

            //var prod = new Produto() { Id = 1, Nome = "Produto 0003", Descricao = "", Valor = 123m, Loja = loja, Marca = marca };
            //context.Produtos.Add(prod);
            //context.SaveChanges();

            // context.Produtos.Update(prod); // Atualiza Somente os Campos Modificados
            // or
            // context.Entry(prod).State = EntityState.Modified;// Atualiza TODOS os Campos Modificados ou Nao
            // context.SaveChanges();

            // context.Produtos.Remove(prod);
            // context.SaveChanges();

            // produtos.ForEach(item => item.Valor += item.Valor * 0.1m); //Add 10%
            // context.SaveChanges();

            #endregion

            #region CMS Teste Produto Find

            //var itemProd = context.Produtos.FirstOrDefault(item => item.Id == 5);
            //var itemProd = context.Produtos.Find(5);
            //if (itemProd != null)
            //{
            //    Console.WriteLine($"Prod.Id: {itemProd.Id} - Prod.Nome: {itemProd.Nome} - Prod.Valor: {itemProd.Valor} - Loja.Nome: {itemProd.Loja.Nome} - Marca.Nome: {itemProd.Marca.Nome}");
            //}
            //else
            //{
            //    Console.WriteLine("Produto Não Localizado...");
            //}
            //Console.WriteLine("");

            #endregion

            #region CMS Teste Produto ToList

            ////Console.WriteLine("Produtos");
            //var produtos = context.Produtos.ToList();
            ////var produtos = context.Produtos.OrderByDescending(item => item.Loja.Nome).ToList();
            ////var produtos = context.Produtos.Where(item => item.Nome.StartsWith("A")).ToList();
            ////var produtos = context.Produtos.Where(item => item.Loja.Id == 3).ToList();
            //if ((produtos != null) && (produtos.Count > 0))
            //{
            //    foreach (var item in produtos)
            //    {
            //        Console.WriteLine($"Prod.Id: {item.Id} - Prod.Nome: {item.Nome} - Prod.Valor: {item.Valor} - Loja.Nome: {item.Loja.Nome} - Marca.Nome: {item.Marca.Nome}");
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("Lista de Produto Vazia...");
            //}
            //Console.WriteLine("");

            #endregion
            
            #region CMS Teste Trace

            //Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            //Trace.AutoFlush = true;
            //Trace.Indent();
            //Trace.WriteLine("Entering Main");
            //Trace.Indent();
            //Trace.WriteLine("Entering Main - 2");
            //Trace.WriteLine("Exiting Main - 2");
            //Trace.Unindent();
            //Trace.WriteLine("Exiting Main");
            //Trace.Unindent();
            //Console.WriteLine("");

            //Debug.Listeners.Add(new TextWriterTraceListener(Console.Out));
            //Debug.AutoFlush = true;
            //Debug.Indent();
            //Debug.WriteLine("Entering Main");
            //Console.WriteLine("Hello World.");
            //Debug.WriteLine("Exiting Main");
            //Debug.Unindent();

            //source1.TraceInformation("Main enters");
            //Console.WriteLine("Hello World");
            //source1.TraceInformation("Main exists");


            #endregion

            Console.WriteLine("Fim");
            Console.ReadKey();

        }
    }
}
