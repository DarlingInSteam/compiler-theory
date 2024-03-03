using System.IO;
using System.Text;
using Microsoft.Win32;

namespace compiler_theory.app.view_model;

public class FileService
{
    public string ReadTextFromFile(string filePath)
    {
        try
        {
            // Проверяем, существует ли файл
            if (File.Exists(filePath))
            {
                // Читаем текст из файла
                return File.ReadAllText(filePath);
            }
            else
            {
                return "Файл не существует";
            }
        }
        catch (Exception ex)
        {
            // Обработка ошибок чтения файла
            return $"Ошибка чтения файла: {ex.Message}";
        }
    }

    public string Save(string code)
    {
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        saveFileDialog.Title = "Сохранение";
        saveFileDialog.Filter = "Текстовый файл (*.txt)|*.txt|C# файл (*.cs)|*.cs|C файл (*.c)|*.c|C++ файл (*.cpp)|*.cpp|Python файл (*.py)|*.py|JavaScript файл (*.js)|*.js|HTML файл (*.html)|*.html|CSS файл (*.css)|*.css|XML файл (*.xml)|*.xml|JSON файл (*.json)|*.json|Markdown файл (*.md)|*.md|PHP файл (*.php)|*.php|Java файл (*.java)|*.java|Все файлы (*.*)|*.*";

        if (saveFileDialog.ShowDialog() == true)
        {
            string filePath = saveFileDialog.FileName;
            File.WriteAllText(filePath, code);
        }

        return saveFileDialog.FileName;
    }

    public string Save(string code, string path)
    {
        File.WriteAllText(path, code);

        return "успешное сохранение";
    }

    public string SaveAs(string code)
    {
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        saveFileDialog.Title = "Сохранение";
        saveFileDialog.Filter = "Текстовый файл (*.txt)|*.txt|C# файл (*.cs)|*.cs|C файл (*.c)|*.c|C++ файл (*.cpp)|*.cpp|Python файл (*.py)|*.py|JavaScript файл (*.js)|*.js|HTML файл (*.html)|*.html|CSS файл (*.css)|*.css|XML файл (*.xml)|*.xml|JSON файл (*.json)|*.json|Markdown файл (*.md)|*.md|PHP файл (*.php)|*.php|Java файл (*.java)|*.java|Все файлы (*.*)|*.*";


        if (saveFileDialog.ShowDialog() == true)
        {
            string filePath = saveFileDialog.FileName;
            File.WriteAllText(filePath, code);

            return filePath;
        }

        return "Сохранение файла не получилось";
    }

    public string CreateFile()
    {
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        saveFileDialog.Title = "Создать файл";
        saveFileDialog.Filter = "Текстовый файл (*.txt)|*.txt|C# файл (*.cs)|*.cs|C файл (*.c)|*.c|C++ файл (*.cpp)|*.cpp|Python файл (*.py)|*.py|JavaScript файл (*.js)|*.js|HTML файл (*.html)|*.html|CSS файл (*.css)|*.css|XML файл (*.xml)|*.xml|JSON файл (*.json)|*.json|Markdown файл (*.md)|*.md|PHP файл (*.php)|*.php|Java файл (*.java)|*.java|Все файлы (*.*)|*.*";

        
        if (saveFileDialog.ShowDialog() == true)
        {
            string filePath = saveFileDialog.FileName;

            
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, string.Empty);
            }

            return filePath;
        }

        return "Создание файла не получилось";
    }
    
    public string GetFileEncoding(string filePath)
    {
        Encoding resultEncoding = null;
        byte[] buffer = new byte[5];

        using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
        {
            fileStream.Read(buffer, 0, 5);
            fileStream.Close();

            if (buffer[0] == 0xef && buffer[1] == 0xbb && buffer[2] == 0xbf)
            {
                resultEncoding = Encoding.UTF8;
            }
            else if (buffer[0] == 0xff && buffer[1] == 0xfe)
            {
                resultEncoding = Encoding.Unicode;
            }
            else if (buffer[0] == 0xfe && buffer[1] == 0xff)
            {
                resultEncoding = Encoding.BigEndianUnicode;
            }
            else if (buffer[0] == 0 && buffer[1] == 0 && buffer[2] == 0xfe && buffer[3] == 0xff)
            {
                resultEncoding = Encoding.UTF32;
            }
            else
            {
                resultEncoding = Encoding.Default;
            }
        }

        return resultEncoding.EncodingName;
    }
    
    public string GetLineSeparator(string filePath)
    {
        using (var streamReader = new StreamReader(filePath))
        {
            int bufferSize = 1024; 
            char[] buffer = new char[bufferSize];
            int bytesRead = streamReader.Read(buffer, 0, bufferSize);

            string firstChunk = new string(buffer, 0, bytesRead);

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
}