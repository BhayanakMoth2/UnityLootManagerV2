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
        public void OnGUI()
        {
            EditorGUILayout.BeginVertical();
            GUILayout.Label("Mic Check.", GUILayout.ExpandHeight(false));
            GUILayout.Space(5);
            string dbName = EditorGUILayout.TextField("Enter Database Name", GUILayout.ExpandWidth(false), GUILayout.ExpandHeight(false));
            bool openDb = GUILayout.Button("Open Database", GUILayout.ExpandHeight(false), GUILayout.ExpandWidth(false));
            if(openDb)
            {
                if (dbName != "")
                {
                    dbTable.SetDatabase(dbPath, dbName);
                    
                }
                else
                {
                    Debug.Log("Enter the database name.");
                }
            }
            EditorGUILayout.EndVertical();
        }

        class DatabaseTable
        {
            string dbPath = "";
            SqliteConnection conn = null;
            SqliteCommand cmd = null;
            public List<string> dbNames = new List<string>();
            ISQLDatabase ISQL = new ISQLDatabase();
            public DatabaseTable()
            {

            }
            public bool SetDatabase(string dbPath,string active)
            {
                this.dbPath = dbPath;
                ISQL.SetDatabaseDirectory(dbPath, active);
                conn = ISQL.GetConn();
                conn.Open();
                cmd = ISQL.GetCommand(ref conn);
                cmd.CommandText = "SELECT name FROM sqlite_master WHERE type = 'table' ;";
                var sqle = ISQL.ExecuteNonQuery(ref cmd);
                conn.Close(); 
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
            
        }
    }

}