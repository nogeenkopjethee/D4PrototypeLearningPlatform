namespace D4PrototypeLearningPlatform.Model
{
    public class Cursus
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";

        public IList<Module>? Modules { get; set; } = new List<Module>();
    }
}
