using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Database;
[AddComponentMenu("Database")]
public class Test : MonoBehaviour {
    ISQLDatabase sql = new ISQLDatabase();
    void Start()
    {
       
        sql.SetDatabaseName("wepons");
        var conn = sql.GetConn();
        var cmd = sql.GetCommand(ref conn);       
        conn.Open();
        sql.ExecuteNonQuery("select * from WeaponNames where key%2 = 0",ref cmd);
        var reader = sql.GetReader(ref cmd,ref conn);
        var schema = reader.GetSchemaTable();
        var val1 =schema.Rows;
        Debug.Log("Val1: " + val1);
        string str="";
        while (reader.Read())
        {
            str = reader[1].ToString();
            Debug.Log(str);
        }
        
    }



}
