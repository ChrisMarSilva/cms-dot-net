Console.WriteLine($"[{DateTime.Now.TimeOfDay}] - INICIO ");
try
{
    var rootPath = @"C:\Users\chris\Desktop\CMS DotNet";
    var foldersToDelete = new List<string> { "bin", "obj", ".vs", "node_modules" };
    var filesToDelete = new List<string> { "*.tmp", "*.log", "*.bak" };

    CleanProjects(rootPath, foldersToDelete, filesToDelete);
    Console.WriteLine("");
}
catch (Exception ex)
{
    Console.WriteLine($"[{DateTime.Now.TimeOfDay}] - ERRO: {ex.Message}");
}
finally
{
    Console.WriteLine($"[{DateTime.Now.TimeOfDay}] - FIM");
    Console.ReadLine();
}

void CleanProjects(string rootPath, List<string> foldersToDelete, List<string> filesToDelete)
{
    Console.WriteLine("");
    Console.WriteLine($"rootPath: {rootPath}");

    var paths = Directory.GetDirectories(rootPath, "*", SearchOption.AllDirectories);

    Parallel.ForEach(paths, new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount }, dir =>
    {
        CleanProjectFolders(dir, foldersToDelete);
        CleanProjectFiles(dir, filesToDelete);
    });
}

void CleanProjectFolders(string rootPath, List<string> foldersToDelete)
{
    Console.WriteLine("");
    Console.WriteLine("CleanProjectFolders");

    foreach (var rootFolder in foldersToDelete)
    {
        var folders = Directory.GetDirectories(rootPath, rootFolder, SearchOption.TopDirectoryOnly); // AllDirectories
        if (folders.Count() > 1) Console.WriteLine($"    {rootFolder} ({folders.Count()})");

        foreach (var folder in folders)
        {
            if (folders.Contains("CMS DotNet My Setup")) continue;
            try
            {
                Directory.Delete(folder, true);
                Console.WriteLine($"        {Path.GetRelativePath(rootPath, folder)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{DateTime.Now.TimeOfDay}] - ERRO (FOLDER) {folder}: {ex.Message}");
            }
        }
    }
}

void CleanProjectFiles(string rootPath, List<string> filesToDelete)
{
    Console.WriteLine("");
    Console.WriteLine("CleanProjectFiles");

    foreach (var rootFile in filesToDelete)
    {
        var files = Directory.GetFiles(rootPath, rootFile, SearchOption.TopDirectoryOnly); // AllDirectories
        if (files.Count() > 1) Console.WriteLine($"    {rootFile} ({files.Count()})");

        foreach (var file in files)
        {
            if (file.Contains("CMS DotNet My Setup")) continue;
            try
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
                Console.WriteLine($"        {Path.GetRelativePath(rootPath, file)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{DateTime.Now.TimeOfDay}] - ERRO (FILE) {file}: {ex.Message}");
            }
        }
    }
}