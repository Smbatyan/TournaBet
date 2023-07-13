namespace TournaBet.Liga.Entities;

public class ParticipantEntity
{
    public int Id { get; set; }
    public int LigaId { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedDate { get; set; }
}