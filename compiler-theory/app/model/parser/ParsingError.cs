namespace compiler_theory.app.model.parser;

public class ParsingError
{
    public string Message { get; set; }
    public int StartIndex { get; set; }
    public int EndIndex { get; set; }
    public string ExpectedToken { get; set; }
}