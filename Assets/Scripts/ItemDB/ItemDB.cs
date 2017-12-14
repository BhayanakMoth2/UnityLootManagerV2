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
    public class ISQLDatabase
    {
        string dbPath = "";
        string activeDb = "";
        public void SetDatabaseName(string databaseName)
        {
            dbPath = "URI=file:" + Application.dataPath +"/Database/"+databaseName+".s3b";
            Debug.Log("DbPath: "+dbPath);
            activeDb = databaseName;
        }
        public void SetDatabaseDirectory(string path, string activeDb)
        {
            dbPath = "URI = file:" + path + "/" + activeDb + ".s3b";
            this.activeDb = activeDb;
        }

        public void ExecuteNonQuery(string CommandText,ref SqliteCommand cmd)
        {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = CommandText;
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (SqliteException sqlE)
                {
                    string exp = sqlE.Message;
                    Debug.Log("SQLite Exception!:" + exp);
                }
            }

       public SqliteCommand GetCommand(ref SqliteConnection conn)
        {
            var cmd = conn.CreateCommand();
            return cmd;
        }
        public SqliteDataReader GetReader(ref SqliteCommand cmd,ref SqliteConnection conn)
        {
            
            try
            {
                var reader = cmd.ExecuteReader();
                return reader;
            }
            catch
            {
                return null;
            }
            
        }
        public SqliteConnection GetConn()
        {
            SqliteConnection conn = new SqliteConnection(dbPath);
            return conn;
        }
    }
 
} 