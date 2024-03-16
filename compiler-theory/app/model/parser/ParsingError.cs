namespace compiler_theory.app.model.parser;

public class ParsingError
{
    public int NumberOfError { get; set; }
    public string Message { get; set; }
    public int StartIndex { get; set; }
    public int EndIndex { get; set; }
    public string ExpectedToken { get; set; }
    public string UnexpectedToken { get; set; }
}