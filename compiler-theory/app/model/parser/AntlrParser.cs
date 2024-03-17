using System.IO;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using compiler_theory.app.model.parser;

namespace compiler_theory.app.model.parser;

class ErrorListener : BaseErrorListener
{
    public List<ParsingError> errors = new List<ParsingError>();

    public override void SyntaxError(TextWriter output, IRecognizer
            recognizer, IToken offendingSymbol, int line, int charPositionInLine,
        string msg, RecognitionException e)
    {
        errors.Add(new ParsingError
        {
            Message = msg,
            EndIndex = charPositionInLine,
            StartIndex = charPositionInLine,
            NumberOfError = errors.Count + 1,
        });
    }
}

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

public class AntlrErrors
{
    private List<Token> LexerErrors;
    private List<ParsingError> ParsingErrors;
    
    public AntlrErrors(List<Token> lexerErrors, List<ParsingError> parsingErrors)
    {
        LexerErrors = lexerErrors;
        ParsingErrors = parsingErrors;
    }

    public List<Token> GetLexerErrors()
    {
        return LexerErrors;
    }

    public List<ParsingError> GetParsingErrors()
    {
        return ParsingErrors;
    }
}

public class AntlrParser
{
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