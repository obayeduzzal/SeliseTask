namespace TTM.Core.Shared.DTOs;
public class TeamDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public List<Guid> TeamIds { get; set; }
    public List<TeamMemberDTO> TeamMembers { get; set; }

}

public class TeamMemberDTO
{
    public Guid UserId { get; set; }
    public Guid TeamId { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
}
