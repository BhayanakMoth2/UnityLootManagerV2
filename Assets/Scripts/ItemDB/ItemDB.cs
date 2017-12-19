﻿using System.Collections;
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

        public SqliteException ExecuteNonQuery(string CommandText,ref SqliteCommand cmd)
        {
            SqliteException sqle = null ;
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
                    sqle = sqlE;
                    Debug.Log("Command Executed: " + cmd.CommandText);
            }
                return sqle;
            }
        public SqliteException ExecuteNonQuery(ref SqliteCommand cmd)
        {
            SqliteException sqle = null;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SqliteException sqlE)
            {
                string exp = sqlE.Message;
                Debug.Log("SQLite Exception!:" + exp);
                sqle = sqlE;
                Debug.Log("Command Executed: " + cmd.CommandText);
            }
            return sqle;

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
        public void table_info(ref SqliteCommand cmd, string tableName)
        {
            cmd.CommandText = "PRAGMA table_info("+tableName+");";
            cmd.ExecuteNonQuery();
            var reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                Debug.Log(reader.GetString(1));
            }

        }
    }
    public class DataTable
    {
        System.Data.DataTable dbTable = new System.Data.DataTable("Weaponnames");
        ISQLDatabase iSql = new ISQLDatabase();
        SqliteConnection conn = null;
        SqliteCommand cmd = null;
        public DataTable()
        {
            iSql.SetDatabaseDirectory(Application.dataPath + "/Database/","wepons");
            conn = iSql.GetConn();
            cmd = iSql.GetCommand(ref conn);
            conn.Open();
            cmd.CommandText = "SELECT * FROM WeaponNames;";
            iSql.ExecuteNonQuery(ref cmd);
            SqliteDataAdapter dbAdapter = new SqliteDataAdapter(cmd);
            dbAdapter.AcceptChangesDuringFill = true;
            dbAdapter.Fill(dbTable);
            
            dbTable.Rows[0][1] = "killer pistal";
         
            SqliteCommandBuilder dbBuilder = new SqliteCommandBuilder(dbAdapter);
            cmd = dbBuilder.GetUpdateCommand();
            Debug.Log("Flag :"+dbAdapter.AcceptChangesDuringUpdate);
            dbAdapter.AcceptChangesDuringUpdate = true;
            dbAdapter.Update(dbTable);   
        }
    }
} 