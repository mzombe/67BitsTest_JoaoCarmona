using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryManager : MonoBehaviour
{
 
    private static List<Transform> _npcs = new List<Transform>();
    public static List<Transform> Npcs { get { return _npcs; } }
    public static List<MoveInertia> AvaliableNpcs { get {
            List<MoveInertia> npc = new List<MoveInertia>();
            foreach (Transform t in _npcs) {
                if (t.gameObject.activeSelf && t.TryGetComponent<MoveInertia>(out MoveInertia mov))
                    npc.Add(mov);
            }
            return npc;
        } }
    private void Awake(){
        for (int i = 0; i < transform.childCount; i++){
            _npcs.Add(transform.GetChild(i));
        }
    }

}
