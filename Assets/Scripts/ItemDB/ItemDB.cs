using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
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
            return  _preprefix+" "+_prefix+" "+_name; 
        }
        public int randInt()
        {
            return rd.Next();
        }
    }
    public class sqlDatabase
    {

    }
} 