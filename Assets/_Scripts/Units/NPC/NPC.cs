using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private GameEvent punchEvent;
    [SerializeField] private RagDollActive activeRagDoll;
    [SerializeField] private bool isRagDoll;
    [SerializeField] private CollectNPC collectNPC;
    
    private void Update() {
        if(!isRagDoll)
            return;

        activeRagDoll.ActiveRagDoll();
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("HitBox")){
            punchEvent.Invoke();
            activeRagDoll.ActiveRagDoll();
            activeRagDoll.ImpulseRagDoll(other.contacts[0].point);
            collectNPC.enabled = true;
            this.enabled = false;
        }
    }


}
