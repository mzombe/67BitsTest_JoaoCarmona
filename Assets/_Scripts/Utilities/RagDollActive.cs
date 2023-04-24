using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagDollActive : MonoBehaviour
{
    [SerializeField] private Rigidbody _rig;
    [SerializeField] private Collider[] _desactiveCol;
    [SerializeField] private Animator _anim;
    [SerializeField] private bool active;
    [SerializeField] private GameObject vfxHit;
    private Rigidbody[] rigidbodies;
    private Collider[] colliders;

    private void Awake() {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        colliders = GetComponentsInChildren<Collider>();
    }

    private void Start(){
        if(!active) DeactiveRagDoll();
    }

    private void SetCollidersEnabled(bool enabled){
        foreach (Collider col in colliders){
            col.enabled = enabled;
        }
    }

    private void SetRigidbodiesKinematic(bool Kinematic){
        foreach (var rig in rigidbodies){
            rig.isKinematic = Kinematic;
        }
    }


    private void DeactiveRagDoll(){
        _rig.isKinematic = false;
        _anim.enabled = true;

        SetCollidersEnabled(false);
        SetRigidbodiesKinematic(true);
    }

    public void ImpulseRagDoll(Vector3 dir){
        foreach (var rig in rigidbodies){
            rig.AddExplosionForce(20f, dir, 20f, 0.5f, ForceMode.Impulse);
        }
    }

    public void ActiveRagDoll(){
        Instantiate(vfxHit, transform.position, Quaternion.identity);
        _rig.gameObject.tag = "RagDoll";
        for (int i = 0; i < _desactiveCol.Length; i++)
        {
            _desactiveCol[i].enabled = false;
        }
        _rig.isKinematic = true;
        _anim.enabled = false;
        SetCollidersEnabled(true);
        SetRigidbodiesKinematic(false);
    }
}
