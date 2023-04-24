using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceNPC : MonoBehaviour
{
    [SerializeField] private Transform point;
    [SerializeField] private GameObject freeRag;
    private int prevQuantity;
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.layer == 3){
            prevQuantity = CarryManager.Instance.GetQuantity();
            if(CarryManager.Instance.GetQuantity() > 0){
                CarryManager.Instance.RemoveNPC();
                StartCoroutine(Spawn());
            }
        }
    }

    IEnumerator Spawn(){
        for (int i = 0; i < prevQuantity; i++){   
            GameObject obj = Instantiate(freeRag, point.position, freeRag.transform.rotation);
            Destroy(obj, 2f);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
