using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CareMyPet
{
    public class AnimalRace : DatabaseEntity
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public static readonly Column[] columns =
        {
            new Column("ID", ColumnType.PRIMARY_KEY),
            new Column("Name", ColumnType.STRING),
        };

        public List<AnimalRace> GetAnimalsTypes()
        {
            return GetList<AnimalRace>(dr => new AnimalRace()
            {
                ID = dr.GetInt32(0),
                Name = dr.GetString(1),
            });
        }

        protected override Column[] Columns => columns;

        protected override string TableName => "AnimalRace";

        protected override Column PrimaryKey => Columns[0];
    }
}
