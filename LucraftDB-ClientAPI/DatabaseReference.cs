using System;
using System.Collections.Generic;
using System.Text;

namespace Lucraft.Database.Client
{
    public class DatabaseReference
    {
        public DatabaseReference(string id)
        {
            ID = id;
        }

        public string ID { get; private set; }

        public CollectionReference GetCollection(string id)
        {
            return new CollectionReference(ID, id);
        }
    }
}
