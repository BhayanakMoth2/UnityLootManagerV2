using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Data;
using Mono.Data.Sqlite;

namespace Database 
{
    [System.Serializable]
    public class random
    {
        System.Random rd;
        public List<string> preprefix = new List<string>();
        public List<string> prefix = new List<string>();
        public List<string> name = new List<string>();
       
        public string Name()
        {
            rd = new System.Random();
            int len1 = preprefix.Count;
            int len2 = prefix.Count;
            int len3 = name.Count;
            string _preprefix = this.preprefix[rd.Next(len1 - 1)];
            string _prefix = this.prefix[rd.Next(len2 - 1)];
            string _name = this.name[rd.Next(len3 - 1)];
            return  _preprefix+" "+_prefix+" "+_name; 
        }
        public int randInt()
        {
            return rd.Next();
        }
    }
    [AddComponentMenu("SqlDatabase")]
    public class sqlDatabase : MonoBehaviour
    {
        public enum ColumnType
        {
            INTEGER,
            TEXT,
            DATETIME
        };
        private string dbpath;
        private string tableName;
        sqlDatabase dbase;
        public void Start(string name)
        {
            dbase = new sqlDatabase(name);

        }
        public sqlDatabase(string databaseName)
        {
            dbpath = "URI=file:C:/Unity_Projects/UnityChestDropSystem/Assets/Database/"+databaseName+".s3b";
            Debug.Log(dbpath);
           
        }
        public void CreateSchema(string tableName)
        {
            using (var conn = new SqliteConnection(dbpath))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS '"+tableName+"'([Key] INTEGER NOT NULL UNIQUE PRIMARY KEY AUTOINCREMENT);";
                    var result = cmd.ExecuteNonQuery();
                    Debug.Log("create schema: "+result);
                }
            }
            this.tableName = tableName;
        }
        public void AddColumn(string tableName, string ColumnName,ColumnType type ,bool isUnique=true, bool isNull=false)
        {
            string Unique = "";
            if(isUnique)
            {
                Unique = "UNIQUE";
            }
            string Null = "";
            if(isNull)
            {
                Null = "NULL";
            }
            using (var conn = new SqliteConnection(dbpath))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT [" + ColumnName + "] FROM [" + tableName + "];";
                    var result = cmd.ExecuteNonQuery();
                    if (result != 0)
                    {
                        cmd.CommandText = "ALTER TABLE [" + tableName + "]" + "" + " ADD COLUMN IF NOT EXISTS '"
                            + ColumnName + "'" + " " + Null + " " + Unique + " " + type.ToString() + ";";
                        Debug.Log("adding column:" + result);
                        Debug.Log("Type added:" + type.ToString());
                    }
                }
            }
        }
       
        public void AddItem(string itemName, int ID)
        {
            using (var conn = new SqliteConnection(dbpath))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO WeaponNames(Name,ID)" + "VALUES(@Name,@ID);";
                    cmd.Parameters.Add(new SqliteParameter { ParameterName = "Name", Value = itemName });
                    cmd.Parameters.Add(new SqliteParameter { ParameterName = "ID", Value = ID });
                    var result = cmd.ExecuteNonQuery();
                    Debug.Log("Insert result:" + result);
                }
            }
        }


    }
} 