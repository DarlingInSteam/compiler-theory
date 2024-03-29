using System.Diagnostics;
using System.IO;

namespace compiler_theory.app.model;

public static class HtmlHelper
{
    /// <summary>
    /// Opens the specified file in the default browser.
    /// </summary>
    /// <param name="filePath">The file path.</param>
    public static void OpenInBrowser(string filePath)
    {
        switch (filePath)
        {
            case "About.html":
            {
                File.WriteAllText("About.html", "<!DOCTYPE html>\n<html lang=\"ru\">\n<head>\n    <meta charset=\"UTF-8\">\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\n    <title>О программе</title>\n    <style>\n        body {\n            font-family: Arial, sans-serif;\n            margin: 0;\n            padding: 0;\n            display: flex;\n            justify-content: center;\n            align-items: center;\n            height: 100vh;\n            background-color: #f4f4f4;\n        }\n\n        .container {\n            text-align: center;\n            max-width: 600px;\n            padding: 20px;\n            border: 1px solid #ccc;\n            border-radius: 10px;\n            background-color: #fff;\n            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);\n        }\n\n        h1 {\n            color: #333;\n            margin-bottom: 20px;\n        }\n\n        p {\n            color: #555;\n            margin-bottom: 10px;\n            line-height: 1.4;\n        }\n\n        .author {\n            font-weight: bold;\n            color: #007BFF;\n        }\n\n        .version {\n            color: #28A745;\n        }\n\n        .description {\n            font-style: italic;\n            color: #6C757D;\n        }\n\n        .year {\n            color: #FFC107;\n        }\n\n        .university {\n            color: #6610f2;\n            font-weight: bold;\n            margin-top: 20px;\n        }\n    </style>\n</head>\n<body>\n<div class=\"container\">\n    <h1>О программе</h1>\n    <p class=\"version\">Версия программы: 1.0.0</p>\n    <p class=\"author\">Автор: Пронько А.В., АВТ-113</p>\n    <p class=\"description\">Описание: текстовый редактор с функциями языкового процессора</p>\n    <p class=\"year\">2024 г.</p>\n    <p class=\"university\">Разработано для Новосибирского государственного технического университета (НГТУ)</p>\n</div>\n</body>\n</html>\n");
                var p = new Process();
                p.StartInfo = new ProcessStartInfo(filePath) { UseShellExecute = true };
                p.Start();
                break;
            }
            case "Help.html":
            {
                File.WriteAllText("Help.html", "<!DOCTYPE html>\n<html lang=\"ru\">\n<head>\n    <meta charset=\"UTF-8\">\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\n    <title>Окно справки</title>\n    <style>\n        body {\n            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;\n            margin: 20px;\n            padding: 0;\n            background-color: #f8f9fa;\n        }\n\n        h1 {\n            color: #343a40;\n            border-bottom: 2px solid #007bff;\n            padding-bottom: 5px;\n        }\n\n        h2 {\n            color: #0056b3;\n            margin-top: 15px;\n        }\n\n        p {\n            color: #495057;\n            margin-bottom: 10px;\n        }\n\n        strong {\n            color: #007bff;\n        }\n    </style>\n</head>\n<body>\n<h1>Справка</h1>\n\n<h2>Файл:</h2>\n<p><strong>Создать:</strong> Создает новый файл для редактирования.</p>\n<p><strong>Открыть:</strong> Открывает существующий файл для редактирования.</p>\n<p><strong>Сохранить:</strong> Сохраняет текущее состояние файла.</p>\n<p><strong>Сохранить как:</strong> Сохраняет текущее состояние файла с новым именем.</p>\n<p><strong>Выход:</strong> Закрывает текстовый редактор.</p>\n\n<h2>Правка:</h2>\n<p><strong>Отменить:</strong> Отменяет последнее действие.</p>\n<p><strong>Повторить:</strong> Повторяет последнее отмененное действие.</p>\n<p><strong>Вырезать:</strong> Удаляет выбранный текст и копирует его в буфер обмена.</p>\n<p><strong>Копировать:</strong> Копирует выбранный текст в буфер обмена.</p>\n<p><strong>Вставить:</strong> Вставляет текст из буфера обмена в текущее место курсора.</p>\n<p><strong>Удалить:</strong> Удаляет выбранный текст.</p>\n<p><strong>Выделить все:</strong> Выделяет весь текст в редакторе.</p>\n\n<h2>Справка:</h2>\n<p><strong>Вызов справки:</strong> Отображает окно справки с описанием функций редактора.</p>\n<p><strong>О программе:</strong> Показывает информацию о текстовом редакторе и его версии.</p>\n\n<h2>Текст:</h2>\n<p><strong>Постановка задачи:</strong> [Описание задачи]</p>\n<p><strong>Грамматика:</strong> [Описание грамматики]</p>\n<p><strong>Классификация грамматики:</strong> [Описание классификации грамматики]</p>\n<p><strong>Метод анализа:</strong> [Описание метода анализа]</p>\n<p><strong>Диагностика и нейтрализация ошибок:</strong> [Описание диагностики и нейтрализации ошибок]</p>\n<p><strong>Тестовый пример:</strong> [Описание тестового примера]</p>\n<p><strong>Список литературы:</strong> [Список литературы]</p>\n<p><strong>Исходный код программы:</strong> [Описание исходного кода программы]</p>\n<p><strong>Размер текста:</strong> [Описание изменения размера текста]</p>\n\n<h2>Пуск:</h2>\n<p><strong>Запустить:</strong> Запускает выполнение программы.</p>\n\n<h2>Язык:</h2>\n<p><strong>Русский:</strong> Установлен текущим языком интерфейса.</p>\n<p><strong>Английский:</strong> Изменяет язык интерфейса на английский.</p>\n\n</body>\n</html>\n");
                var p = new Process();
                p.StartInfo = new ProcessStartInfo(filePath) { UseShellExecute = true };
                p.Start();
                break;
            }
            case "Postanovka.html":
            {
                File.WriteAllText("Postanovka.html", "<!DOCTYPE html><html lang=\"en\"><head><meta charset=\"UTF-8\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"><title>Тема работы - Объявление ассоциативного массива языка Java</title></head><body><h1>Тема работы - Объявление ассоциативного массива языка Java</h1><h2>Особенности:</h2><p>Возможность выбрать любой тип Java.</p><h2>Примеры верных строк:</h2><p><code>Map&lt;String, String&gt; map = new HashMap&lt;String, String&gt;();</code></p><p><code>Map&lt;Char, Char&gt; _asd = new HashMap &lt;Char, Char&gt;();</code></p></body></html>");
                var p = new Process();
                p.StartInfo = new ProcessStartInfo(filePath) { UseShellExecute = true };
                p.Start();
                break;
            }
            case "Gramma.html":
            {
                File.WriteAllText("Gramma.html", "<!DOCTYPE html><html lang=\"en\"><head><meta charset=\"UTF-8\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"><title>Грамматика</title><style>table { border-collapse: collapse; } th, td { border: 1px solid black; padding: 8px; }</style></head><body><h1>Грамматика</h1><table><tr><th>Нетерминал</th><th>Продукция</th></tr><tr><td>I</td><td>Map FIRSTOPERATOR</td></tr><tr><td>FIRSTOPERATOR</td><td>&lt; FIRSTTYPE</td></tr><tr><td>FIRSTTYPE</td><td>DATATYPE DATATYPESEPARATOR</td></tr><tr><td>DATATYPESEPARATOR</td><td>, SECONDDATATYPE</td></tr><tr><td>SECONDDATATYPE</td><td>DATATYPE SECONDOPERATOR</td></tr><tr><td>SECONDOPERATOR</td><td>&gt; IDENTIFIRE</td></tr><tr><td>IDENTIFIRE</td><td>symbol IDENTIFIRE | MIDOPERATOR</td></tr><tr><td>MIDOPERATOR</td><td>= NEWKEYWORD</td></tr><tr><td>NEWKEYWORD</td><td>new HASHMAP</td></tr><tr><td>HASHMAP</td><td>HashMap PRELASTOPERATOR</td></tr><tr><td>PRELASTOPERATOR</td><td>&lt; PRELASTDATATYPE</td></tr><tr><td>PRELASTDATATYPE</td><td>DATATYPE LASTSEPARATOR</td></tr><tr><td>LASTSEPARATOR</td><td>, LASTDATATYPE</td></tr><tr><td>LASTDATATYPE</td><td>DATATYPE LASTOPERATOR</td></tr><tr><td>LASTOPERATOR</td><td>&gt; FIRSTBRACKET</td></tr><tr><td>FIRSTBRACKET</td><td>( SECONDBRACKET</td></tr><tr><td>SECONDBRACKET</td><td>) ENDLINE</td></tr><tr><td>ENDLINE</td><td>;</td></tr></table></body></html>");
                var p = new Process();
                p.StartInfo = new ProcessStartInfo(filePath) { UseShellExecute = true };
                p.Start();
                break;
            }
            case "ClassGramm.html":
            {
                File.WriteAllText("Gramma.html", "");
                var p = new Process();
                p.StartInfo = new ProcessStartInfo(filePath) { UseShellExecute = true };
                p.Start();
                break;
            }
            case "Analyze.html":
            {
                File.WriteAllText("Gramma.html", "<!DOCTYPE html><html lang=\"en\"><head><meta charset=\"UTF-8\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"><title>Грамматика</title><style>table { border-collapse: collapse; } th, td { border: 1px solid black; padding: 8px; }</style></head><body><h1>Грамматика</h1><table><tr><th>Нетерминал</th><th>Продукция</th></tr><tr><td>I</td><td>Map FIRSTOPERATOR</td></tr><tr><td>FIRSTOPERATOR</td><td>&lt; FIRSTTYPE</td></tr><tr><td>FIRSTTYPE</td><td>DATATYPE DATATYPESEPARATOR</td></tr><tr><td>DATATYPESEPARATOR</td><td>, SECONDDATATYPE</td></tr><tr><td>SECONDDATATYPE</td><td>DATATYPE SECONDOPERATOR</td></tr><tr><td>SECONDOPERATOR</td><td>&gt; IDENTIFIRE</td></tr><tr><td>IDENTIFIRE</td><td>symbol IDENTIFIRE | MIDOPERATOR</td></tr><tr><td>MIDOPERATOR</td><td>= NEWKEYWORD</td></tr><tr><td>NEWKEYWORD</td><td>new HASHMAP</td></tr><tr><td>HASHMAP</td><td>HashMap PRELASTOPERATOR</td></tr><tr><td>PRELASTOPERATOR</td><td>&lt; PRELASTDATATYPE</td></tr><tr><td>PRELASTDATATYPE</td><td>DATATYPE LASTSEPARATOR</td></tr><tr><td>LASTSEPARATOR</td><td>, LASTDATATYPE</td></tr><tr><td>LASTDATATYPE</td><td>DATATYPE LASTOPERATOR</td></tr><tr><td>LASTOPERATOR</td><td>&gt; FIRSTBRACKET</td></tr><tr><td>FIRSTBRACKET</td><td>( SECONDBRACKET</td></tr><tr><td>SECONDBRACKET</td><td>) ENDLINE</td></tr><tr><td>ENDLINE</td><td>;</td></tr></table></body></html>");
                var p = new Process();
                p.StartInfo = new ProcessStartInfo(filePath) { UseShellExecute = true };
                p.Start();
                break;
                break;
            }
            case "OutCode.html":
            {
                break;
            }
        }
    }
}