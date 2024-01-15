using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Text txtScore;
    [SerializeField] private VirtualCoinSO _coins;

    public static ScoreManager Instance;
    public int Coins { get { return _coins.GetCoins(); } }
    private void Awake() {
        Instance = this;
    }
    private void OnEnable() => _coins.OnUpdateCoin += UpdateDisplay;

    private void OnDisable() => _coins.OnUpdateCoin -= UpdateDisplay;
    private void Start(){
        UpdateDisplay();
    }
    private void UpdateDisplay(int i = 0, int amount = 0) => txtScore.text = "$"+_coins.GetCoinsString();

    public void AddScore(int i) => _coins.AddCoin(i);

    public bool Buy(int i){
        if(_coins.CanBuy(i)){
            _coins.Buy(i);
            return true;
        }
        return false;
    }
}
