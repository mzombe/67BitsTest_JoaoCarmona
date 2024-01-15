using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalInertia : MonoBehaviour
{
    [SerializeField] private float _force;

    private void FixedUpdate(){
        if (Player.PlayerMove.IsMoving){
            AddInertia(Player.PlayerMove.Dir);
        }
    }
    private void AddInertia(Vector3 dir){
        for (int i = 0; i < CarryManager.AvaliableNpcs.Count; i++){
            float amount = i == 0 ? 0.2f : (i * _force) / CarryManager.Npcs.Count;
            CarryManager.AvaliableNpcs[i].Move(dir, amount );
        }
    }
}
