

public class Opgave
{
    public Guid Id { get; set; }

    public string Name { get; set; } = "";

    public string Description { get; set; } = "";

    public ProgrammingLanguage Type { get; set; } = ProgrammingLanguage.Markdown;

    public string InitialCode { get; set; } = "";
}

public enum ProgrammingLanguage
{
    Markdown,
    Javascript,
}