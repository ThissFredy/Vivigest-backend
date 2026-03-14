namespace Vivigest_backend.Domain.Entities
{
    public class AuthorizedPerson
    {
        public int IdAuthorized { get; set; }
        public int IdResidentUser { get; set; }
        public int IdPerson { get; set; }
        public int IdRelationshipType { get; set; }
        public string VehiclePlate { get; set; } = string.Empty;
        public int IdState { get; set; }
        public DateTime DateTime { get; set; }

        // Relations
        public User ResidentUser { get; set; } = null!;
        public Person Person { get; set; } = null!;
        public RelationshipType RelationshipType { get; set; } = null!;
        public State State { get; set; } = null!;

    }
}
