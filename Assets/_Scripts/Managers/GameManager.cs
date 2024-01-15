using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum GameState
{
    RUNNING,
    END
}
public class GameManager : MonoBehaviour
{
    [SerializeField] private GameEvent _gameEndEvent;
    [SerializeField] private GameEvent _sellEvent;
    private GameState _currentState;
    private NPCSpawner _spawner;
    private ScoreManager _scoreManager;
    private void Awake(){
        Initialize();
    }
    private void Start(){
        Application.targetFrameRate = 60;
    }

    private void OnEnable(){
        _sellEvent.Subcribe(VerifyNpc);
    }
    private void OnDisable(){
        _sellEvent.Unsubcribe(VerifyNpc);
    }

    private void Initialize(){ 
        _spawner = GetComponent<NPCSpawner>();
        _scoreManager = GetComponent<ScoreManager>();
    }

    private void VerifyNpc(){
        if (_spawner.npcCount <= 0)
            _gameEndEvent.Invoke();
    }
}
