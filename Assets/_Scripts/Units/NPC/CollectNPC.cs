using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectNPC : MonoBehaviour
{
    [SerializeField] private GameEvent _onCollectNpc;
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
        if(other.gameObject.layer == 3 && canColect && CarryController.Instance.Cancollect()){
            CarryController.Instance.AddNPC(1);
            Destroy(gameObject.transform.parent.transform.parent.gameObject);
            _onCollectNpc.Invoke();
        }

    }

    
}
