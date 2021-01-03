using System;

namespace Lucraft.Database.Client
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class DatabaseProperty : Attribute 
    {
        public readonly string name;

        public DatabaseProperty(string name) => this.name = name;
    }
}
