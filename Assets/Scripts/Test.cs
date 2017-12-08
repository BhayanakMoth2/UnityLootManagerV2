using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Database;
[AddComponentMenu("Database")]
public class Test : MonoBehaviour {
    public random rng;
    public GameObject item;
    sqlDatabase sql;
    void Start () {
        rng = new random();
        sql = new sqlDatabase("Swords");
        sql.CreateSchema("WeaponNames");
        sql.AddColumn("WeaponNames", "Name", sqlDatabase.ColumnType.TEXT, false, false);
        sql.AddColumn("WeaponNames", "ID", sqlDatabase.ColumnType.INTEGER, false, false);
        sql.AddColumn("Swords","Damage", sqlDatabase.ColumnType.INTEGER, false, false);
	}
    public Rigidbody projectile;
    public void Generate()
    {
        
        item.name = rng.Name();
        sql.AddItem(item.name,rng.randInt());
        Instantiate(item);
      
    }
	
}
