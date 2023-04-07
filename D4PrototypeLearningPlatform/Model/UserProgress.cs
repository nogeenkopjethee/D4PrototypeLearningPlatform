namespace D4PrototypeLearningPlatform.Model;

/// <summary>
/// This beast is to keep the progress of the user. For reasons unknown we need to do this like this!
/// </summary>
public class UserProgress
{
    public Guid Id { get; set; }
    public Guid CursusId { get; set; }
    public Guid ModuleId { get; set; }
    public Guid OpgaveId { get; set; }
    public Guid UserId { get; set; }
}
