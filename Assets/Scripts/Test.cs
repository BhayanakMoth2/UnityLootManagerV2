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
        //sql.AddColumn("WeaponNames", "Damage", sqlDatabase.ColumnType.INTEGER, false, false);
        string str =sql.GetValue("wepons", "WeaponNames", "Name", 0) as string;
        Debug.Log(str);
    }
    public void Generate()
    {
       
      
    }
	
}
