using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CareMyPet
{
    public class VeterinaryServices : DatabaseEntity
    {
        public int VsID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string AnimalsTypes { get; set; }
        public string Location { get; set; }
        public string ActivityTime { get; set; }
        public string Phone { get; set; }

        protected static Column[] columns =
        {
            new Column("VsID", ColumnType.INT),
            new Column("Name", ColumnType.STRING),
            new Column("Description", ColumnType.STRING),
            new Column("AnimalsTypes", ColumnType.STRING),
            new Column("Location", ColumnType.STRING),
            new Column("ActivityTime", ColumnType.STRING),
            new Column("Phone", ColumnType.STRING)
        };

        public List<VeterinaryServices> GetVS()
        {
            return GetList<VeterinaryServices>(dr => new VeterinaryServices()
            {
                VsID = dr.GetInt32(0),
                Name = dr.GetString(1),
                Description = dr.GetStringOrNull(2),
                AnimalsTypes = dr.GetStringOrNull(3),
                Location = dr.GetString(4),
                Phone = dr.GetString(5),
                ActivityTime = dr.GetString(6)
            });
        }


        protected override Column[] Columns => columns;

        protected override string TableName => "VS";

        protected override Column PrimaryKey => Columns[0];
    }
}
