using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Database;
[AddComponentMenu("Database")]
public class Test : MonoBehaviour {

    public random rng;

    sqlDatabase sql;

    void Start () {
        rng = new random();
        sql = new sqlDatabase();
        //sql.createDatabase("Potions");
        sql.setDatabase("wepons");
        sql.CreateSchema("WeaponNames");
        sql.AddColumn("WeaponNames", "Name", sqlDatabase.ColumnType.TEXT, false, false);
  
    }
    public void Generate()
    {
        
    //    sql.AddItem(rng.Name(),rng.randInt());
      
    }
	
}
