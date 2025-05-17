using FluentAssertions;
using Moq;
using TTM.Core.Domain.Team;
using TTM.Core.Infrastructure.Repositories;
using TTM.Core.Modules.Teams.Create;
using TTM.Core.Modules.Teams.Get;
using TTM.Core.Shared.Specifications.Teams;

namespace TTM.Test.Teams;

public class TeamTests
{
    private readonly Mock<IRepository<Team>> _mockTeamRepository;
    private readonly Mock<IRepository<TeamMember>> _mockTeamMemberRepository;
    private readonly Mock<IReadRepository<Team>> _mockTeamReadRepository;
    private readonly Mock<IReadRepository<TeamMember>> _mockTeamMemberReadRepository;

    public TeamTests()
    {
        _mockTeamRepository = new();
        _mockTeamMemberRepository = new();

        _mockTeamReadRepository = new();
        _mockTeamMemberReadRepository = new();
    }

    [Fact]
    public async Task CreateTeamHandlerWhenCalledShouldCreateTeam()
    {
        // Arrange
        CreateTeamCommand command = new()
        {
            Name = "TestName",
            Description = "test description",
            TeamMembersId = [Guid.NewGuid(), Guid.NewGuid()]
        };

        _mockTeamRepository.Setup(i => i.FirstOrDefaultAsync(It.IsAny<GetTeamByName>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Team)null);

        CreateTeamCommandHandler handler = new(_mockTeamRepository.Object, _mockTeamMemberRepository.Object);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.Should().NotBeNull();
        result.Message.Should().Be("Team Created successfully");

        _mockTeamRepository.Verify(i => i.FirstOrDefaultAsync(It.IsAny<GetTeamByName>(), It.IsAny<CancellationToken>()), Times.Once);
        _mockTeamRepository.Verify(i => i.AddAsync(It.IsAny<Team>(), It.IsAny<CancellationToken>()), Times.Once);
        _mockTeamMemberRepository.Verify(i => i.AddRangeAsync(It.IsAny<List<TeamMember>>(), It.IsAny<CancellationToken>()), Times.Once);
    }
    [Fact]
    public async Task GetTeamQueryHandlerWhenCalledShouldReturnTeam()
    {
        // Arrange
        var teamId = Guid.NewGuid();
        var team = Team.Create(teamId, "TestTeam", "Test Description");

        var mockTeamRepository = new Mock<IRepository<Team>>();
        _mockTeamReadRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(team);

        var handler = new GetTeamQueryHandler(_mockTeamReadRepository.Object, _mockTeamMemberReadRepository.Object);

        // Act
        var result = await handler.Handle(new GetTeamQuery(teamId), default);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(teamId);
        result.Name.Should().Be("TestTeam");
        result.Description.Should().Be("Test Description");

        _mockTeamReadRepository.Verify(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
