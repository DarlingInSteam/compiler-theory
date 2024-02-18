using System.IO;
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
        saveFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";

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
        saveFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";

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
        saveFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
        
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
}