using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Database;
[AddComponentMenu("Database")]
public class Test : MonoBehaviour {
    SQLDatabase sql = new SQLDatabase();
    void Start()
    {
        sql.SetDatabaseName("wepons");
        var conn = sql.GetConn();
        conn.Open();
        var reader = sql.GetReader("WeaponNames","Name",ref conn);
        while(reader.Read())
        Debug.Log(reader.GetString(1));
        
    }
    
   
	
}
