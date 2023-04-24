using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Text txtScore;
    private int currentScore;

    public static ScoreManager Instance;

    private void Awake() {
        Instance = this;
    }

    private void Update() {
        txtScore.text = "$"+currentScore.ToString();
    }

    public void AddScore(int i){
        currentScore += i;
    }

    public bool Buy(int i){
        if(i <= currentScore){
            currentScore -= i;
            return true;
        }
        return false;
    }
}
