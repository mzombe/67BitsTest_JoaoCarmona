using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceNPC : MonoBehaviour
{
    [SerializeField] private GameEvent _sellEvent;
    [SerializeField] private Transform point;
    [SerializeField] private GameObject freeRag;
    private int prevQuantity;
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.layer == 3){
            prevQuantity = CarryController.Instance.GetQuantity();
            if(CarryController.Instance.GetQuantity() > 0){
                CarryController.Instance.RemoveAllNPC();
                StartCoroutine(Spawn());
            }
        }
    }

    IEnumerator Spawn(){
        for (int i = 0; i < prevQuantity; i++){
            _sellEvent.Invoke();
            GameObject obj = Instantiate(freeRag, point.position, freeRag.transform.rotation);
            ScoreManager.Instance.AddScore(10);
            Destroy(obj, 2f);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
