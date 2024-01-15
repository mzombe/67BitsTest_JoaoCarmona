using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceBuyLimit : MonoBehaviour
{
    [SerializeField] int cost;
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.layer == 3){
            int x = ScoreManager.Instance.Coins / cost;
            if(ScoreManager.Instance.Buy(ScoreManager.Instance.Coins)){
                for (int i = 0; i < x; i++){
                    CarryController.Instance.AddLimit();
                }
                other.GetComponentInChildren<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            }
        }
    }
}
