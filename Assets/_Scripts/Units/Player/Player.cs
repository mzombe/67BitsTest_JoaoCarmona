using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static PlayerMove _playerMove;
    public static PlayerMove PlayerMove { get { return _playerMove; } }
    private void Awake(){
        _playerMove = GetComponent<PlayerMove>();
    }
}
