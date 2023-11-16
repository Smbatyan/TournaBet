using TournaBet.Shared.Repository;

namespace TournaBet.Liga.Entities;

public class LigaParticipantEntity : BaseEntity
{
    public int LigaId { get; set; }
    public int PlayerId { get; set; }
    public decimal? Outcome { get; set; }
    public DateTime CreatedDate { get; set; }

    public LigaEntity Liga { get; set; }
}