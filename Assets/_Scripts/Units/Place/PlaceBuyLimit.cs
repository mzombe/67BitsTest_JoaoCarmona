using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceBuyLimit : MonoBehaviour
{
    [SerializeField] int cost;
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.layer == 3){
            if(ScoreManager.Instance.Buy(cost)){
                CarryManager.Instance.AddLimit();
            }
        }
    }
}
