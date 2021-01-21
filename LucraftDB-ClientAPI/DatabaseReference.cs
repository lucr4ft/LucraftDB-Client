namespace Lucraft.Database.Client
{
    public class DatabaseReference
    {
        public DatabaseReference(string id)
        {
            Id = id;
        }

        public string Id { get; }

        public CollectionReference GetCollection(string id)
        {
            return new(Id, id);
        }
    }
}
