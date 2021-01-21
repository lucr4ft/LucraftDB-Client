using System;

namespace Lucraft.Database.Client
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class DatabaseProperty : Attribute 
    {
        public readonly string Name;

        public DatabaseProperty(string name) => Name = name;
    }
}
