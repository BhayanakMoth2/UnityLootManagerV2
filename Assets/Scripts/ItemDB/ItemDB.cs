using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Data;
using Mono.Data.Sqlite;
using System;

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
            return _preprefix + " " + _prefix + " " + _name;
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
            TIME,
            DATE,
            FLOAT,
            BOOLEAN,
            REAL,
            VARCHAR,
            BLOB,
            NUMERIC,
            NVARCHAR
        };
        private string dbpath;
        private string tableName;

        //Self explanatory function names.
        public void createDatabase(string databaseName)
        {
            dbpath = "URI=file:C:/Unity_Projects/UnityChestDropSystem/Assets/Database/" + databaseName + ".s3b";
            SqliteConnection.CreateFile("Assets/Database/" + databaseName + ".s3b");
        }
        public void setDatabase(string dbName)
        {
            dbpath = "URI=file:C:/Unity_Projects/UnityChestDropSystem/Assets/Database/" + dbName + ".s3b";

        }

        //Creates a table with a default Key column that is of type Int32, primary key, not null and autoincrementing. 
        public void CreateSchema(string tableName)
        {
            using (var conn = new SqliteConnection(dbpath))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS '" + tableName + "'([Key] INTEGER NOT NULL UNIQUE PRIMARY KEY AUTOINCREMENT);";
                    var result = cmd.ExecuteNonQuery();
                    Debug.Log("create schema: " + result);
                }
            }
            this.tableName = tableName;
        }

        //Add column which is only works if you add a column whose name is not taken.
        public void AddColumn(string tableName, string ColumnName, ColumnType type, bool isUnique = true, bool isNull = false)
        {
            string Unique = "";
            if (isUnique)
            {
                Unique = "UNIQUE";
            }
            string Null = "";
            if (isNull)
            {
                Null = "NULL";
            }
            
            using (var conn = new SqliteConnection(dbpath))
            {

                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "ALTER TABLE [" + tableName + "]" + "" + " ADD COLUMN '"
                         + ColumnName + "'" + " " + Null + " " + Unique + " " + type.ToString() + ";";
                    try
                    {
                        var result = cmd.ExecuteNonQuery();
                    }
                    catch
                    {
                        Debug.Log("Duplicate Column Name!");
                    }
                   
                }
                conn.Close();
                Debug.Log("Test");
            }
            Debug.Log("The programme goes on...");
        }
        //Doesn't work.
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
        //Works on anything that is non-nullable.
        public string GetString(string dbName, string tableName, string columnName, int rowNum)
        {

            string value = null;

            string path = "URI=file:C:/Unity_Projects/UnityChestDropSystem/Assets/Database/" + dbName + ".s3b";
            using (var conn = new SqliteConnection(path))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT " + columnName + " FROM " + tableName + ";";
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        value = reader.GetString(rowNum);
                    }
                    reader.Close();
                }

            }

            return value;
        }
        public int GetInt32(string dbName, string tableName, string columnName, int rowNum)
        {

            int value = 0;

            string path = "URI=file:C:/Unity_Projects/UnityChestDropSystem/Assets/Database/" + dbName + ".s3b";
            using (var conn = new SqliteConnection(path))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT " + columnName + " FROM " + tableName + ";";
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        value = (int)reader.GetInt32(rowNum);

                    }
                    reader.Close();
                }

            }

            return value;
        }
        public float GetFloat(string dbName, string tableName, string columnName, int rowNum)
        {
            float value = 0;

            string path = "URI=file:C:/Unity_Projects/UnityChestDropSystem/Assets/Database/" + dbName + ".s3b";
            using (var conn = new SqliteConnection(path))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT " + columnName + " FROM " + tableName + ";";
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        value = reader.GetFloat(rowNum);
                    }
                    reader.Close();
                }

            }

            return value;
        }
        public DateTime GetDateTime(string dbName, string tableName, string columnName, int rowNum)
        {
            DateTime value = new DateTime();
            value = DateTime.Now;

            string path = "URI=file:C:/Unity_Projects/UnityChestDropSystem/Assets/Database/" + dbName + ".s3b";
            using (var conn = new SqliteConnection(path))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT " + columnName + " FROM " + tableName + ";";
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        value = reader.GetDateTime(rowNum);
                    }
                    reader.Close();
                }

            }

            return value;
        }
     }   
} 