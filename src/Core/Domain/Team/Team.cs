namespace TTM.Core.Domain.Team;
public class Team : IAggregateRoot
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }

    public static Team Create(string name, string description)
    {
        return new Team
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description
        };
    }

    public static Team Create(Guid id, string name, string description)
    {
        return new Team
        {
            Id = id,
            Name = name,
            Description = description
        };
    }
}
