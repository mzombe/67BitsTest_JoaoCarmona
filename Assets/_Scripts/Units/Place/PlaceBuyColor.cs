using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceBuyColor : MonoBehaviour
{
    [SerializeField] int cost;
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.layer == 3){
            if(ScoreManager.Instance.Buy(cost)){
                other.GetComponentInChildren<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            }
        }
    }
}
