namespace Vivigest_backend.Domain.Entities
{
    public class Incident
    {
        public int IdIncident { get; set; }
        public int IdIncidentType { get; set; }
        public DateTime DateTime { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string UrlEvidence { get; set; } = string.Empty;
        public int IdUserReportee { get; set; }
        public DateTime CreatedAt { get; set; }

        // Relations
        public IncidentType IncidentType { get; set; } = null!;
        public User UserReportee { get; set; } = null!;

    }
}
