using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;
using UnityEngine;
using UnityEditor;
using System.IO;
using Database;
using Mono.Data.Sqlite;
namespace Database
{
    class DatabaseUI : EditorWindow
    {

        static string dbPath = "";
        static DatabaseTable dbTable;
        [MenuItem("Tools/Database Editor/Editor")]
        public static void ShowWindow()
        {
            var ui = EditorWindow.GetWindow(typeof(DatabaseUI));
            ui.name = "Mic Check!";
            ui.Show();
        }
        public void OnEnable()
        {
            dbPath = Application.dataPath + "/Database";
            Debug.Log(dbPath);
            dbTable = new DatabaseTable();
        }
        public DatabaseUI()
        {
            this.titleContent = new GUIContent("Database Editor.");
            Debug.Log(dbPath);


        }
        bool isDbOpen = false;
        bool isTableOpen = false;
        List<string> tableNames = new List<string>();
        bool openDb = false;
        string tableName = " ";
        public void OnGUI()
        {

            EditorGUILayout.BeginVertical();
            GUILayout.Label("Mic Check.", GUILayout.ExpandHeight(false));
            GUILayout.Space(5);

            if (!isDbOpen)
            {

                string dbName = "wepons";//EditorGUILayout.TextField("Enter Database Name", GUILayout.ExpandWidth(false), GUILayout.ExpandHeight(false));
                openDb = true;//GUILayout.Button("Open Database", GUILayout.ExpandHeight(false), GUILayout.ExpandWidth(false));

                if (openDb)
                {
                    if (dbName != "")
                    {
                        isDbOpen = dbTable.SetDatabase(dbPath, dbName);

                    }
                    else
                    {
                        Debug.Log("Enter the database name.");
                    }
                }
            }
            if (!isTableOpen)
            {

                if (isDbOpen)
                {
                    bool openTable = true;//GUILayout.Button("Open Table", GUILayout.ExpandWidth(false), GUILayout.ExpandHeight(false));
                    if (openTable)
                    {
                        isTableOpen = dbTable.ColumnName("WeaponNames");
                        Debug.Log(tableName);
                    }
                    else
                    {
                        tableName = EditorGUILayout.TextField("Test", tableName);
                        Debug.Log(tableName);

                    }


                }
            }
            if(isTableOpen)
            {

            }
           EditorGUILayout.EndVertical();
        }
    }





    /***********************DATABASETABLE CLASS*******************************/



    public class DatabaseTable
    {
        string dbPath = "";
        SqliteConnection conn = null;
        SqliteCommand cmd = null;
        public List<string> tableNames = new List<string>();
        ISQLDatabase ISQL = new ISQLDatabase();

        List<string> columnNames = new List<string>();
        List<string> type = new List<string>();

        public DatabaseTable()
        {

        }
        public bool SetDatabase(string dbPath, string active)
        {
            this.dbPath = dbPath;
            ISQL.SetDatabaseDirectory(dbPath, active);
            conn = ISQL.GetConn();
            conn.Open();
            cmd = ISQL.GetCommand(ref conn);
            cmd.CommandText = "SELECT name FROM sqlite_master;";
            var sqle = ISQL.ExecuteNonQuery(ref cmd);
            tableNames = ListTable();
            if (sqle != null)
            {

                Debug.Log("Enter a proper database name.");
                return false;
            }
            else
            {
                return true;
            }
        }
        public List<string> ListTable()
        {
            List<string> tableNames = new List<string>();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string name = reader.GetString(0);
                if (!name.Contains("sqlite"))
                {
                    tableNames.Add(name);
                }
            }
            reader.Close();
            return tableNames;
        }

        public bool ColumnName(string tableName)
        {
            cmd.CommandText = "PRAGMA table_info(" + tableName + ");";
            cmd.ExecuteNonQuery();
            var sqlE = ISQL.ExecuteNonQuery(ref cmd);
            if (sqlE == null)
            {
                SqliteDataReader reader = null;
                try
                {
                    reader = cmd.ExecuteReader();
                }
                catch (SqliteException sqle)
                {
                    Debug.Log(sqle.Message.ToString());
                    return false;
                }
                reader.Read();
                string test = reader.GetString(1);
                while (reader.Read())
                {
                    columnNames.Add(reader.GetString(1));
                }
                reader.Close();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    type.Add(reader.GetString(2));
                }
                foreach (string element in type)
                {
                    Debug.Log(element);
                }

                return true;
            }
            else
            {
                Debug.Log("Sqlite Exception: " + sqlE.Message.ToString());
                return false;
            }
        }
           
    }

    /************************DATAROW CLASS***************************************/

    public class DataHeader
    {
        public int rowID = 0;
        public string name = "";
        public int damage = 0;
        public string description = "";
        
    }
}    


