using TournaBet.Shared.Repository;

namespace TournaBet.Liga.Entities;

public class PlayerPredictionEntity : BaseEntity
{
    public int ParticipantId { get; set; }
    public int MatchId { get; set; }
    public byte Result { get; set; }

    public LigaParticipantEntity Participant { get; set; }
}