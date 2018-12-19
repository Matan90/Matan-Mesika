using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CareMyPet
{
    public enum ColumnType
    {
        PRIMARY_KEY, STRING, INT, SHORT, BOOL, DECIMAL, IMAGE, FLOAT
    }
    public class Column
    {
        public string Name;
        public ColumnType ColumnType;
        public Column(string Name, ColumnType columnType)
        {
            this.Name = Name;
            this.ColumnType = columnType;
        }
    }

}
