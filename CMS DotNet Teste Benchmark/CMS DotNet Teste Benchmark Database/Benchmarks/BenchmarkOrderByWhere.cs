//using BenchmarkDotNet.Attributes;
//using TesteBenchmarkDotNet.Models;

//namespace TesteBenchmarkDotNet.Benchmarks;

//[MemoryDiagnoser]
//public class BenchmarkOrderByWhere : Benchmark
//{
//    [Params("500")]
//[Params("1", "100", "500", "1000", "5000")]
//    public string? NameParams { get; set; }
//    [Benchmark] public List<Book> WhereFirst() => Context.Books.Where(x => x.Author.FirstName.Contains(NameParams!)).OrderBy(x => x.Title).ToList();
//    [Benchmark] public List<Book> OrderByFirst() => Context.BooksWhere(x => x.Author.FirstName.Contains(NameParams!)).OrderBy(x => x.Title).ToList();
//}
