using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucraft.Database.Client.Test
{
    public class Condition
    {
        private readonly int type;

        private readonly string field;
        private readonly object value;
        private readonly string op;
        private readonly Condition con1, con2;

        public Condition(string field, string op, object value)
        {
            type = 0;
            this.field = field;
            this.op = op;
            this.value = value;
        }

        public Condition(Condition con1, string op, Condition con2)
        {
            type = 1;
            this.con1 = con1;
            this.op = op;
            this.con2 = con2;
        }

        public override string ToString()
        {
            if (type == 0)
                return "Condition:{" + field + "," + op + "," + value + "}";
            return "Condition:{" + con1.ToString() + "," + op + "," + con2.ToString() + "}";
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj is Condition con)
            {
                if (type == con.type)
                {
                    if (type == 0 && field.Equals(con.field) && op.Equals(con.op) && value.Equals(con.value)) return true;
                    else if (type == 1 && con1.Equals(con.con1) && con2.Equals(con.con2) && op.Equals(con.op)) return true;
                    else return false;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
    }
}
