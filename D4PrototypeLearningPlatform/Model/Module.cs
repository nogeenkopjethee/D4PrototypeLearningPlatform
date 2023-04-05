namespace D4PrototypeLearningPlatform.Model;

public class Module
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";

    public string? Description { get; set; } = "";

    public IList<Opgave> Opgaves { get; set; } = new List<Opgave>();
}
