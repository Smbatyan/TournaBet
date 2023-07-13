namespace TournaBet.Liga.Entities;

public class PredictionEntity
{
    public int Id { get; set; }
    public int ParticipantId { get; set; }
    public int MatchId { get; set; }
    public byte Result { get; set; }
}