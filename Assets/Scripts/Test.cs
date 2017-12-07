using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Database;
public class Test : MonoBehaviour {
    public random rng;
    public GameObject item;
    void Start () {
        rng = new random();

	}
    public Rigidbody projectile;
    public void Generate()
    {
        
        item.name = rng.Name();
        Instantiate(item);
      
    }
	
}
