using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

static string pdfText(string path)
{
    // var text = string.Empty;
	var text = new StringBuilder();
 
    using PdfReader reader = new PdfReader(path);

    for (int page = 1; page <= reader.NumberOfPages; page++)
    {
        //text += PdfTextExtractor.GetTextFromPage(reader, page);
        ITextExtractionStrategy its = new SimpleTextExtractionStrategy();
        String s = PdfTextExtractor.GetTextFromPage(reader, page, its);
        s = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(s)));
        // text += s;
		text.Append(s);
    }

    // return text;
    return text.ToString();
}

Console.WriteLine("INI");
try
{
    //var path = @"D:\CMS DotNet\CMS DotNet Teste File\CMS DotNet PDF\ParticipantesSTR.pdf";
	var path = @"C:\Users\chris\Desktop\CMS DotNet\CMS DotNet Teste File\CMS DotNet PDF\CMS DotNet PDF iTextSharp\ParticipantesSTR.pdf";
    var texto = pdfText(path: path);

    Console.WriteLine(texto);
}
catch (Exception ex)
{
    Console.WriteLine($"ERRO: {ex}");
}
finally
{
    Console.WriteLine("FIM");
}


