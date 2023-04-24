using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectNPC : MonoBehaviour
{
    private SphereCollider col;
    private bool canColect;
    private void Awake() {
        col = gameObject.GetComponent<SphereCollider>();
    }

    private void OnEnable() {
        col.enabled = true;
        StartCoroutine(TurnCollect());
    }
    IEnumerator TurnCollect(){
        yield return new WaitForSeconds(1f);
        canColect = true;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.layer == 3 && canColect && CarryManager.Instance.Cancollect()){
            CarryManager.Instance.AddOneNPC();
            Destroy(gameObject.transform.parent.transform.parent.gameObject);
        }
    }

    
}
