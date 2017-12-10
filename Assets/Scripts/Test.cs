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
       
        sql.AddColumn("WeaponNames", "Level", sqlDatabase.ColumnType.INTEGER, false, false);
        sql.AddColumn("WeaponNames", "RoF", sqlDatabase.ColumnType.FLOAT, false, false);
        float str =sql.GetValue("wepons", "WeaponNames", "Name", 0) as float;
        Debug.Log(str);
        int damage = sql.GetInt32("wepons","WeaponNames","Damage",0);
        Debug.Log(damage);
    }
    public void Generate()
    {
       
      
    }
	
}
