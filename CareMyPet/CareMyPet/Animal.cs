using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CareMyPet
{
    public class Animal : DatabaseEntity
    {
        public int ID { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Race { get; set; }
        public int Age { get; set; }

        protected static readonly Column[] columns =
        {
            new Column("ID", ColumnType.PRIMARY_KEY),
            new Column("Type", ColumnType.STRING),
            new Column("Name", ColumnType.STRING),
            new Column("Race", ColumnType.STRING),
            new Column("Age", ColumnType.STRING),
        };

        public List<Animal> GetAnimals()
        {
            return GetList<Animal>(dr => new Animal() {
                ID = dr.GetInt32(0),
                Type = dr.GetString(1),
                Name = dr.GetString(2),
                Race = dr.GetString(3),
                Age = dr.GetInt32(4)
            });
        }

        protected override Column[] Columns => columns;

        protected override string TableName => "Animals";
    
        protected override Column PrimaryKey => Columns[0];
    }
}
