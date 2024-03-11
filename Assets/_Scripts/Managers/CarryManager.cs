using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryManager : MonoBehaviour
{
 
    private static List<Transform> _npcs = new List<Transform>();
    public static List<Transform> Npcs { get { return _npcs; } }
    private void Awake(){
        for (int i = 0; i < transform.childCount; i++){
            _npcs.Add(transform.GetChild(i));
            //_npcs[i].transform.parent = null;
        }
    }

}
