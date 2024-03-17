using System.IO;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using compiler_theory.app.model.parser;

namespace compiler_theory.app.model.parser;

/// <summary>
/// Custom error listener for syntax errors during parsing.
/// </summary>
class ErrorListener : BaseErrorListener
{
    public List<ParsingError> errors = new List<ParsingError>();

    public override void SyntaxError(TextWriter output, IRecognizer
            recognizer, IToken offendingSymbol, int line, int charPositionInLine,
        string msg, RecognitionException e)
    {
        var splitString = msg.Split();
        var needToken = "";

        if (splitString.Last() != "IDENTIFIER")
        {
            needToken = splitString.Last()[1..^1];

            if (needToken.Last() == '\'')
            {
                needToken = needToken[0..^1];
            }
        }
        else
        {
            needToken = "_asd";
        }
        
        
        errors.Add(new ParsingError
        {
            Message = msg,
            EndIndex = charPositionInLine,
            StartIndex = charPositionInLine,
            NumberOfError = errors.Count + 1,
            ErrorToken = e.OffendingToken.Text,
            NeedToken = needToken
        });
    }
}

/// <summary>
/// Custom error listener for lexer errors.
/// </summary>
class ErrorLexerListener : IAntlrErrorListener<int>
{
    public List<Token> errors = new List<Token>();
    
    public void SyntaxError(TextWriter output, IRecognizer recognizer,
        int offendingSymbol, int line, int charPositionInLine, string msg,
        RecognitionException e)
    {
        errors.Add(new Token
        {
            Code = "none",
            StartIndex = charPositionInLine,
            EndIndex = charPositionInLine,
            Value = msg
        });
    }
}

/// <summary>
/// Class for handling lexer and parsing errors.
/// </summary>
public class AntlrErrors
{
    private List<Token> LexerErrors;
    private List<ParsingError> ParsingErrors;
    
    /// <summary>
    /// Initializes a new instance of the AntlrErrors class.
    /// </summary>
    public AntlrErrors(List<Token> lexerErrors, List<ParsingError> parsingErrors)
    {
        LexerErrors = lexerErrors;
        ParsingErrors = parsingErrors;
    }

    /// <summary>
    /// Gets the lexer errors.
    /// </summary>
    public List<Token> GetLexerErrors()
    {
        return LexerErrors;
    }

    /// <summary>
    /// Gets the parsing errors.
    /// </summary>
    public List<ParsingError> GetParsingErrors()
    {
        return ParsingErrors;
    }
}

/// <summary>
/// Class for parsing code using Antlr.
/// </summary>
public class AntlrParser
{
    /// <summary>
    /// Parses the specified code and returns the lexer and parsing errors.
    /// </summary>
    public AntlrErrors Parse(string code)
    {
        ICharStream inputStream = CharStreams.fromString(code);
        MapParserLexer mapParserLexer = new MapParserLexer(inputStream);
        CommonTokenStream commonTokenStream = new CommonTokenStream(mapParserLexer);
        MapParserParser mapParserParser = new MapParserParser(commonTokenStream);
        
        var parserListener = new ErrorListener();
        var lexerListener = new ErrorLexerListener();
        
        mapParserLexer.RemoveErrorListeners();
        mapParserLexer.AddErrorListener(lexerListener);
        mapParserParser.RemoveErrorListeners();
        mapParserParser.AddErrorListener(parserListener);
        mapParserParser.map();

        var errors = new AntlrErrors(lexerListener.errors, parserListener.errors);
        
        foreach (var error in parserListener.errors)
        {
            Console.WriteLine(error);
        }

        return errors;
    }
}