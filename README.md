# Compiler Theory App

**Compiler Theory App** - это приложение для редактирования и анализа кода. Оно предоставляет удобный текстовый редактор с расширенными возможностями, также включает функции анализа кода с поддержкой автодополнения и вывода ошибок компиляции.

**Язык программирования:** C#, WPF.

## Оглавление

- [Лабораторная работа №1: Разработка пользовательского интерфейса (GUI) для языкового процессора](#лабораторная-работа-1-разработка-пользовательского-интерфейса-gui-для-языкового-процессора)
- [Лабораторная работа №2: Разработка лексического анализатора (сканера)](#лабораторная-работа-2-разработка-лексического-анализатора-сканера)
- [Лабораторная работа №3: Разработка синтаксического анализатора (парсера)](#лабораторная-работа-3-разработка-синтаксического-анализатора-парсера)
- [Лабораторная работа №4: Нейтрализация ошибок (метод Айронса)](#лабораторная-работа-4-нейтрализация-ошибок-метод-айронса)
- [Лабораторная работа №5: Включение семантики в анализатор. Создание внутренней формы представления программы](#лабораторная-работа-5-включение-семантики-в-анализатор-создание-внутренней-формы-представления-программы)
- [Лабораторная работа №6: Реализация алгоритма поиска подстрок с помощью регулярных выражений](#лабораторная-работа-6-реализация-алгоритма-поиска-подстрок-с-помощью-регулярных-выражений)
- [Лабораторная работа №7: Реализация метода рекурсивного спуска для синтаксического анализа](#лабораторная-работа-7-реализация-метода-рекурсивного-спуска-для-синтаксического-анализа)

## Лабораторная работа №1: Разработка пользовательского интерфейса (GUI) для языкового процессора

**Тема:** Создание текстового редактора с возможностью последующего расширения в направлении языкового процессора.

**Цель работы:** Разработка графического приложения с интерфейсом пользователя для редактирования текстовых данных. Приложение предполагается использовать как основу для будущего расширения функционала в направлении языкового процессора.

### Текстовый Редактор

- **Меню**
- | Пункт меню | Подпункты                                                    |
  | ------ |--------------------------------------------------------------|
  | Файл | ![Файл](compiler-theory/app/resources/Screenshots/9.png)     |
  | Правка | ![Правка](compiler-theory/app/resources/Screenshots/10.png)  |
  | Текст | ![Текст](compiler-theory/app/resources/Screenshots/11.png)   |
  | Справка | ![Справка](compiler-theory/app/resources/Screenshots/12.png) |

- **Базовые Операции:**
    - Открытие, создание и сохранение файлов.
    - Поддержка множества вкладок для одновременного редактирования различных файлов.

      ![Placeholder Screenshot](compiler-theory/app/resources/Screenshots/8.png)

- **Редактирование Кода:**
    - Подсветка синтаксиса и автодополнение для улучшенной читаемости кода.
    - Возможность отмены (Undo) и повтора (Redo) действий.
    - Операции копирования, вставки, удаления и выделения текста.
  
      ![Placeholder Screenshot](compiler-theory/app/resources/Screenshots/7.png)

- **Настройки Внешнего Вида:**
    - Изменение размера шрифта для удобства чтения.
  
      ![Placeholder Screenshot](compiler-theory/app/resources/Screenshots/6.png)

### Анализ Кода

- **Подсветка Ошибок Компиляции:**
    - Вывод ошибок компиляции в удобном формате.

      ![Placeholder Screenshot](compiler-theory/app/resources/Screenshots/5.png)
      
- **Автодополнение:**
    - Подсказки по автодополнению при вводе кода.

      ![Placeholder Screenshot](compiler-theory/app/resources/Screenshots/4.png)
      
- **Семантический Анализ:**
    - Анализ кода на основе компилятора C# для определения типов и доступных членов.
    - 
      ![Placeholder Screenshot](compiler-theory/app/resources/Screenshots/3.png)

### Другие Функции

- **Создание и Открытие Примеров Кода:**
    - Возможность загрузки примеров кода для изучения и экспериментов.

      ![Placeholder Screenshot](compiler-theory/app/resources/Screenshots/2.png)    

- **Поддержка Многозадачности:**
    - Возможность одновременного редактирования и анализа нескольких файлов.
  
      ![Placeholder Screenshot](compiler-theory/app/resources/Screenshots/1.png)


## Лабораторная работа №2: Разработка лексического анализатора (сканера)

**Тема:** разработка лексического анализатора (сканера).

**Цель работы:** изучить назначение лексического анализатора. Спроектировать алгоритм и выполнить программную реализацию сканера.

| №  | Тема | Пример верной строки | Справка |
|----| ------ | ------ | ------ |
| 17 | Объявление ассоциативного массива языка Java | Map<String, String> map = new HashMap<String, String>(); | [ссылка](https://www.geeksforgeeks.org/implementing-associate-array-in-java/) |

**В соответствии с вариантом задания необходимо:**

1. Спроектировать диаграмму состояний сканера.
2. Разработать лексический анализатор, позволяющий выделить в тексте лексемы, иные символы считать недопустимыми (выводить ошибку).
3. Встроить сканер в ранее разработанный интерфейс текстового редактора. Учесть, что текст для разбора может состоять из множества строк.

**Входные данные:** строка (текст программного кода).

**Выходные данные:** последовательность условных кодов, описывающих структуру разбираемого текста с указанием места положения и типа.

### Примеры допустимых строк

```
Map<String, String> map = new HashMap<String, String>();
```

```
Map<string, string> map = new HashMap<string, string>();
```

```
Map<float, float> _12map = new HashMap<float, float>();
```

```
Map<int, string> _map = new HashMap<int, string>();
```

### Диаграмма состояний сканера

![Диаграмма состояний сканера](compiler-theory/app/resources/Screenshots/13.jpg)

### Тестовые примеры

1. **Тест №1.** Пример, показывающий все возможные лексемы, которые могут быть найдены лексическим анализатором.
   ![Тест 1](compiler-theory/app/resources/Screenshots/14.png)

2. **Тест №2.** Сложный пример.
   ![Тест 2](compiler-theory/app/resources/Screenshots/15.png)

3. **Тест №3.** Сложный пример.
   ![Тест 3](compiler-theory/app/resources/Screenshots/16.png)

## Лабораторная работа №3: Разработка синтаксического анализатора (парсера)

**Тема:** разработка синтаксического анализатора (парсера).

**Цель работы:** изучить назначение синтаксического анализатора, спроектировать алгоритм и выполнить программную реализацию парсера.

| №  | Тема | Пример верной строки | Справка |
|----| ------ | ------ | ------ |
| 17 | Объявление ассоциативного массива языка Java | Map<String, String> map = new HashMap<String, String>(); | [ссылка](https://www.geeksforgeeks.org/implementing-associate-array-in-java/) |

**В соответствии с вариантом задания на курсовую работу необходимо:**
1. Разработать автоматную грамматику.
2. Спроектировать граф конечного автомата (перейти от автоматной грамматики к конечному автомату).
3. Выполнить программную реализацию алгоритма работы конечного автомата.
4. Встроить разработанную программу в интерфейс текстового редактора, созданного на первой лабораторной работе.

### Грамматика

1. I -> Map FIRSTOPERATOR
2. FIRSTOPERATOR-> < FIRSTTYPE
3. FIRSTTYPE-> DATATYPE  DATATYPESEPARATOR
4. DATATYPESEPARATOR-> , SECONDDATATYPE
5. SECONDDATATYPE -> DATATYPE SECONDOPERATOR
6. SECONDOPERATOR -> > IDENTIFIRE
7. IDENTIFIRE -> symbol IDENTIFIRE | MIDOPERATOR
8. MIDOPERATOR -> = NEWKEYWORD
9. NEWKEYWORD -> new HASHMAP
10. HASHMAP -> HashMap PRELASTOPERATOR
11. PRELASTOPERATOR -> < PRELASTDATATYPE
12. PRELASTDATATYPE -> DATATYPE  LASTSEPARATOR
13. LASTSEPARATOR -> , LASTDATATYPE
14. LASTDATATYPE -> DATATYPE  LASTOPERATOR
15. LASTOPERATOR -> > FIRSTBRACKET
16. FIRSTBRACKET -> ( SECONDBRACKET
17. SECONDBRACKET -> ) ENDLINE
18. ENDLINE -> ;

DATATYPE -> 'String' | 'Int' | 'Double' | 'Float' | 'Short' | 'Long' | 'Bool' | 'Char'


Vt = {",",     ";",     "(",     ")",    "<",     ">",   "=",
'Map',  'HashMap', 'new', 'String' , 'Int' , 'Double' , 'Float' , 'Short' , 'Long' , 'Bool' , 'Char' }

Vm = {I, FIRSTOPERATOR, FIRSTTYPE, DATATYPESEPARATOR, SECONDDATATYPE
SECONDOPERATOR, IDENTIFIRE, MIDOPERATOR, NEWKEYWORD, HASHMAP
PRELASTOPERATOR, PRELASTDATATYPE, LASTSEPARATOR , LASTDATATYPE,
LASTOPERATOR , FIRSTBRACKET , SECONDBRACKET, ENDLINE  }

### Грамматика для antlr

grammar MapParser;

1. prog : (map NEWLINE?)+;
2. map : MAP fb1;
3. fb1 : FB datatype1;
4. datatype1 : dataType comma1;
5. comma1 : COMMA datatype2;
6. datatype2 : dataType lb1;
7. lb1 : LB name;
8. name : IDENTIFIER ravno;
9. ravno : EQUAL new;
10. new : NEW hashmap;
11. hashmap : HASHMAP fb2;
12. fb2 : FB datatype3;
13. datatype3 : dataType comma2;
14. comma2 : COMMA datatype4;
15. datatype4 : dataType lb2;
16. lb2 : LB lp;
17. lp : LP rp;
18. rp : RP semicolon;
19. semicolon : SEMICOLON;

20. dataType : 'Int' | 'String' | 'Bool' | 'Float' | 'Byte' | 'Short' | 'Long' | 'Boolean' | 'Char' ;

21. NEWLINE : [\r\n]+ ;
22. MAP: 'Map';
23. FB: '<' ;
24. LB: '>' ;
25. COMMA: ',' ;
26. EQUAL: '=' ;
27. NEW: 'new' ;
28. HASHMAP: 'HashMap' ;
29. LP: '(' ;
30. RP: ')' ;
31. SEMICOLON: ';' ;

32. IDENTIFIER : [a-zA-Z_][a-zA-Z_0-9]* ;
33. WS : [ \t\r\n]+ -> skip ;

### Построенное дерево
![](compiler-theory/app/resources/Screenshots/23.png)

### Классификация грамматики

Согласно классификации Хомского, грамматика G[Z] является полностью автоматной.

### Граф конечного автомата

![Граф конечного автомата](compiler-theory/app/resources/Screenshots/17.jpg)

### Тестовые примеры

1. **Тест №1.** Все выражения написаны корректно.

   ![Тест 1](compiler-theory/app/resources/Screenshots/18.jpg)
2. **Тест №2.** Пример ошибок.

   ![Тест 2](compiler-theory/app/resources/Screenshots/19.png)

## Лабораторная работа №4: Нейтрализация ошибок (метод Айронса)

**Тема**: нейтрализация ошибок (метод Айронса).

**Цель работы:** реализовать алгоритм нейтрализации синтаксических ошибок и дополнить им программную реализацию парсера.

### Метод Айронса

Разрабатываемый синтаксический анализатор построен на базе автоматной грамматики. При нахождении лексемы, которая не соответствует грамматике предлагается свести алгоритм нейтрализации к последовательному
удалению следующего символа во входной цепочке до тех пор, пока следующий символ не окажется одним из допустимых в данный момент разбора.

Этот алгоритм был мной уже реализован в Лабораторной работе №3. В таблице ошибок выводятся их местоположение и текст ошибки, содержащий информацию об отброшенном фрагменте.

1. **Тест** Пример ошибок.
    ![Тест](compiler-theory/app/resources/Screenshots/21.png)
    ![Исправления](compiler-theory/app/resources/Screenshots/22.png)

## Лабораторная работа №5: Включение семантики в анализатор. Создание внутренней формы представления программы

**Тема:** включение семантики в анализатор, создание внутренней формы представления программы, используя польскую инверсную запись (ПОЛИЗ).

**Цель работы:** дополнить анализатор, разработанный в рамках лабораторных работ, этапом формирования внутренней формы представления программы.

## Лабораторная работа №6: Реализация алгоритма поиска подстрок с помощью регулярных выражений

**Тема:** реализация алгоритма поиска подстрок с помощью регулярных выражений.

**Цель работы:** реализовать алгоритм поиска в тексте подстрок, соответствующих заданным регулярным выражениям.

## Лабораторная работа №7: Реализация метода рекурсивного спуска для синтаксического анализа

**Тема:** реализация метода рекурсивного спуска для синтаксического анализа.

**Цель работы:** разработать для грамматики алгоритм синтаксического анализа на основе метода рекурсивного спуска.

## Используемые Библиотеки

- [AvalonEdit](https://www.nuget.org/packages/AvalonEdit/) (версия 6.3.0.90)
- [Material.Icons](https://www.nuget.org/packages/Material.Icons/) (версия 2.1.6)
- [Material.Icons.WPF](https://www.nuget.org/packages/Material.Icons.WPF/) (версия 2.1.0)
- [MaterialDesignExtensions](https://www.nuget.org/packages/MaterialDesignExtensions/) (версия 4.0.0-a02)
- [MaterialDesignThemes](https://www.nuget.org/packages/MaterialDesignThemes/) (версия 4.9.0)
- [MaterialDesignColors](https://www.nuget.org/packages/MaterialDesignColors/) (версия 2.1.4)
- [MaterialDesignThemes.MahApps](https://www.nuget.org/packages/MaterialDesignThemes.MahApps/) (версия 0.3.0)
- [Microsoft.CodeAnalysis.CSharp](https://www.nuget.org/packages/Microsoft.CodeAnalysis.CSharp/) (версия 4.9.0-3.final)
- [Microsoft.CodeAnalysis.CSharp.Scripting](https://www.nuget.org/packages/Microsoft.CodeAnalysis.CSharp.Scripting/) (версия 4.9.0-3.final)
- [Microsoft.CodeAnalysis.CSharp.Workspaces](https://www.nuget.org/packages/Microsoft.CodeAnalysis.CSharp.Workspaces/) (версия 4.9.0-3.final)
- [Microsoft.Xaml.Behaviors.Wpf](https://www.nuget.org/packages/Microsoft.Xaml.Behaviors.Wpf/) (версия 1.1.77)
- [Prism.Wpf](https://www.nuget.org/packages/Prism.Wpf/) (версия 9.0.271-pre)

*Примечание: Убедитесь, что все пакеты устанавливаются из указанных версий для обеспечения совместимости.*



---

*Разработано с использованием технологий .NET и популярных библиотек для создания удобного и эффективного инструмента для работы с кодом на языке C#.*