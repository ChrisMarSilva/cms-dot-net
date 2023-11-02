Console.WriteLine("INI");
try
{
    var path = @"C:\Users\chris\Desktop\CMS DotNet\CMS DotNet Teste File\CMS DotNet PDF\CMS DotNet PDF IronPDF\ParticipantesSTR.pdf";

    //var pdfDocument = IronPdf.PdfDocument.FromFile(path);
    //string text = pdfDocument.ExtractAllText();

	string text = string.Empty;
	
    using PdfDocument PDF = PdfDocument.FromFile(path);
	
    for (var index = 0; index < PDF.PageCount; index++)
    {
        int PageNumber = index + 1;
        text += "\n\n" + PDF.ExtractTextFromPage(index);
    }

    Console.WriteLine(text);
}
catch (Exception ex)
{
    Console.WriteLine($"ERRO: {ex}");
}
finally
{
    Console.WriteLine("FIM");
}

