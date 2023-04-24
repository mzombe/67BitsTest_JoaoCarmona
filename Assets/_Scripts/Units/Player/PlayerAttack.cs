using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private SphereCollider[] cols;
    private Animator _animator;

    private void Awake(){
        _animator = GetComponentInChildren<Animator>();
    }
    
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == 6){
            if(!other.gameObject.CompareTag("RagDoll")) _animator.SetTrigger("Punch");
            for (int i = 0; i < cols.Length; i++){
                if(other.gameObject.CompareTag("NPC")) cols[i].enabled = true;
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        for (int i = 0; i < cols.Length; i++){
            cols[i].enabled = false;
        }
    }

}
