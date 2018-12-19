using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareMyPet
{
    public abstract class DatabaseEntity
    {
        protected abstract Column[] Columns { get; }
        protected abstract string TableName { get; }
        protected abstract Column PrimaryKey { get; }


        public delegate T ItemCreator<T>(SqlDataReader dr);

        private string GetColumnsCommaSeparated(bool withPrimaryKey, bool withAtSign = false)
        {
            StringBuilder sb = new StringBuilder();
            foreach(Column col in Columns)
            {
                if (!withPrimaryKey && col.ColumnType == ColumnType.PRIMARY_KEY)
                    continue;
                if(withAtSign)
                    sb.Append("@");
                sb.Append(col.Name + ",");
            }
            if(Columns.Length > 0)
                sb.Remove(sb.Length-1,1);
            return sb.ToString();
        }

        public List<T> GetList<T>(ItemCreator<T> itemCreator)
        {
            List<T> items = new List<T>();
            using (SqlConnection conn = new SqlConnection(DBHelper.CONN_STRING))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(SelectSQL(), conn))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            T databaseEntity = itemCreator(dr);
                            items.Add(databaseEntity);
                        }
                    }
                }
            }

            return items;
        }
        
        public bool Insert(SqlParameter[] parameters)
        {
            using(SqlConnection conn = new SqlConnection(DBHelper.CONN_STRING))
            {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(InsertSQL(), conn))
                {
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteNonQuery() == 1;
                }
            }
        }

        public bool Update(SqlParameter[] parameters)
        {
            using(SqlConnection conn = new SqlConnection(DBHelper.CONN_STRING))
            {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(UpdateSQL(), conn))
                {
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteNonQuery() == 1;
                }
            }
        }
        
        public bool Delete(object PrimaryKeyToBeDeleted)
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.CONN_STRING))
            {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand(DeleteSQL(), conn))
                {
                    cmd.Parameters.AddWithValue("@" + PrimaryKey.Name, PrimaryKeyToBeDeleted);
                    return cmd.ExecuteNonQuery() == 1;
                }
            }

            
        }

        public string InsertSQL()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO " + TableName + " (");
            sb.Append(GetColumnsCommaSeparated(false)+") VALUES (");
            sb.Append(GetColumnsCommaSeparated(false,true) + ")");
            return sb.ToString();
        }

        public string SelectSQL()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            sb.Append(GetColumnsCommaSeparated(true));
            sb.Append(" FROM " + TableName);
            return sb.ToString();
        }

        public string UpdateSQL()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE " + TableName + " SET ");
            string primaryKeyColumnName = null;
            for (int i = 0; i < Columns.Length-1; i++)
            {
                Column col = Columns[i];
                if(col.ColumnType == ColumnType.PRIMARY_KEY)
                {
                    primaryKeyColumnName = col.Name;
                    continue;
                }
                sb.Append(col.Name + "=@" + col.Name + ",");
            }

            if (Columns.Length > 0)
            {
                Column lastCol = Columns.Last();
                if (lastCol.ColumnType == ColumnType.PRIMARY_KEY)
                {
                    primaryKeyColumnName = lastCol.Name;
                }
                else
                {
                    sb.Append(Columns.Last().Name + "=@" + Columns.Last().Name);
                }
            }
            if (primaryKeyColumnName != null)
            {
                sb.Append(" WHERE " + primaryKeyColumnName + "=@" + primaryKeyColumnName);
            }
            else
            {
                throw new Exception("no primary key column. update not allowed");
            }
            return sb.ToString();
        }

        public string DeleteSQL()
        {
            StringBuilder sb = new StringBuilder();
            if(PrimaryKey == null)
            {
                throw new Exception("cannot delete without primary key");
            }
            sb.Append("DELETE FROM " + TableName + " WHERE " + PrimaryKey + "=@" + PrimaryKey);
            return sb.ToString();
        }

       
    }
}
