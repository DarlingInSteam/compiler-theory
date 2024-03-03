using System.IO;
using System.Text;
using Microsoft.Win32;

namespace compiler_theory.app.model;

public class FileService
{
    public static string ReadTextFromFile(string filePath)
    {
        try
        {
            // Проверяем, существует ли файл
            return File.Exists(filePath) ?
                // Читаем текст из файла
                File.ReadAllText(filePath) : "Файл не существует";
        }
        catch (Exception ex)
        {
            // Обработка ошибок чтения файла
            return $"Ошибка чтения файла: {ex.Message}";
        }
    }

    public static string Save(string code)
    {
        var saveFileDialog = new SaveFileDialog();
        saveFileDialog.Title = "Сохранение";
        saveFileDialog.Filter = "Текстовый файл (*.txt)|*.txt|C# файл (*.cs)|*.cs|C файл (*.c)|*.c|C++ файл (*.cpp)|*.cpp|Python файл (*.py)|*.py|JavaScript файл (*.js)|*.js|HTML файл (*.html)|*.html|CSS файл (*.css)|*.css|XML файл (*.xml)|*.xml|JSON файл (*.json)|*.json|Markdown файл (*.md)|*.md|PHP файл (*.php)|*.php|Java файл (*.java)|*.java|Все файлы (*.*)|*.*";

        if (saveFileDialog.ShowDialog() != true) return saveFileDialog.FileName;
        var filePath = saveFileDialog.FileName;
        File.WriteAllText(filePath, code);

        return saveFileDialog.FileName;
    }

    public static string Save(string code, string path)
    {
        File.WriteAllText(path, code);

        return "успешное сохранение";
    }

    public static string SaveAs(string code)
    {
        var saveFileDialog = new SaveFileDialog();
        saveFileDialog.Title = "Сохранение";
        saveFileDialog.Filter = "Текстовый файл (*.txt)|*.txt|C# файл (*.cs)|*.cs|C файл (*.c)|*.c|C++ файл (*.cpp)|*.cpp|Python файл (*.py)|*.py|JavaScript файл (*.js)|*.js|HTML файл (*.html)|*.html|CSS файл (*.css)|*.css|XML файл (*.xml)|*.xml|JSON файл (*.json)|*.json|Markdown файл (*.md)|*.md|PHP файл (*.php)|*.php|Java файл (*.java)|*.java|Все файлы (*.*)|*.*";


        if (saveFileDialog.ShowDialog() != true) return "Сохранение файла не получилось";
        var filePath = saveFileDialog.FileName;
        File.WriteAllText(filePath, code);

        return filePath;

    }

    public static string CreateFile()
    {
        var saveFileDialog = new SaveFileDialog();
        saveFileDialog.Title = "Создать файл";
        saveFileDialog.Filter = "Текстовый файл (*.txt)|*.txt|C# файл (*.cs)|*.cs|C файл (*.c)|*.c|C++ файл (*.cpp)|*.cpp|Python файл (*.py)|*.py|JavaScript файл (*.js)|*.js|HTML файл (*.html)|*.html|CSS файл (*.css)|*.css|XML файл (*.xml)|*.xml|JSON файл (*.json)|*.json|Markdown файл (*.md)|*.md|PHP файл (*.php)|*.php|Java файл (*.java)|*.java|Все файлы (*.*)|*.*";


        if (saveFileDialog.ShowDialog() != true) return "Создание файла не получилось";
        var filePath = saveFileDialog.FileName;

            
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, string.Empty);
        }

        return filePath;

    }
    
    public static string GetFileEncoding(string filePath)
    {
        Encoding resultEncoding = null;
        var buffer = new byte[5];

        using (var fileStream = new FileStream(filePath, FileMode.Open))
        {
            var read = fileStream.Read(buffer, 0, 5);
            fileStream.Close();

            resultEncoding = buffer[0] switch
            {
                0xef when buffer[1] == 0xbb && buffer[2] == 0xbf => Encoding.UTF8,
                0xff when buffer[1] == 0xfe => Encoding.Unicode,
                0xfe when buffer[1] == 0xff => Encoding.BigEndianUnicode,
                0 when buffer[1] == 0 && buffer[2] == 0xfe && buffer[3] == 0xff => Encoding.UTF32,
                _ => Encoding.Default
            };
        }

        return resultEncoding.EncodingName;
    }
    
    public static string GetLineSeparator(string filePath)
    {
        using var streamReader = new StreamReader(filePath);
        const int bufferSize = 1024; 
        var buffer = new char[bufferSize];
        var bytesRead = streamReader.Read(buffer, 0, bufferSize);

        var firstChunk = new string(buffer, 0, bytesRead);

        if (firstChunk.Contains("\r\n"))
        {
            return "CRLF";
        }
        else if (firstChunk.Contains("\r"))
        {
            return "CR";
        }
        else if (firstChunk.Contains("\n"))
        {
            return "LF";
        }
        else
        {
            return "Unknown";
        }
    }
}