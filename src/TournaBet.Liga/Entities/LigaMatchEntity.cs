using TournaBet.Shared.Repository;

namespace TournaBet.Liga.Entities;

public class LigaMatchEntity : BaseEntity
{
    public int LigaId { get; set; }
    public int MatchId { get; set; }
    public int StakeId { get; set; }
    public byte? ActualResult { get; set; }

    public LigaEntity Liga { get; set; }
}