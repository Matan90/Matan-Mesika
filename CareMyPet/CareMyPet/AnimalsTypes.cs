using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CareMyPet
{
    public class AnimalsType : DatabaseEntity
    {
        public int ID { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Race { get; set; }
        public int Age { get; set; }

        public static readonly Column[] columns =
        {
            new Column("ID", ColumnType.PRIMARY_KEY),
            new Column("Type", ColumnType.STRING),
            new Column("Name", ColumnType.STRING),
            new Column("Race", ColumnType.STRING),
            new Column("Age", ColumnType.STRING)
        };

        public List<AnimalsType> GetAnimalsTypes()
        {
            return GetList<AnimalsType>(dr => new AnimalsType() {
                ID = dr.GetInt32(0),
                Type = dr.GetString(1),
                Name = dr.GetString(2),
                Race = dr.GetString(3),
                Age = dr.GetInt32(4)
            });
        }

        protected override Column[] Columns => columns;

        protected override string TableName => "AnimalsType";

        protected override Column PrimaryKey => Columns[0];



    }
}
