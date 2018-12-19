using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CareMyPet
{
    public class Store : DatabaseEntity
    {
        public int StoreID { get; set; }
        public string StoreName { get; set; }
        public string Description { get; set; }
        public string ActivityTime { get; set; }
        public string Location { get; set; }
        public string Phone { get; set; }


        private static readonly Column[] columns = {
            new Column("StoreID",ColumnType.PRIMARY_KEY),
            new Column("StoreName",ColumnType.STRING),
            new Column("Description",ColumnType.STRING),
            new Column("ActivityTime",ColumnType.STRING),
            new Column("Location",ColumnType.STRING),
            new Column("Phone",ColumnType.STRING)
        };

        public List<Store> GetStores()
        {
            return GetList<Store>(dr => new Store()
            {
                StoreID = dr.GetInt32(0),
                StoreName = dr.GetString(1),
                Description = dr.GetStringOrNull(2),
                ActivityTime = dr.GetStringOrNull(3),
                Location = dr.GetStringOrNull(4),
                Phone = dr.GetStringOrNull(5)
            });
        }

        protected override Column[] Columns => columns;

        protected override string TableName => "Stores";

        protected override Column PrimaryKey => Columns[0];
    }
}
