using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Virtual Coin", menuName = "Economy System/Virtual Coin SO")]
public class VirtualCoinSO : ScriptableObject
{
    [SerializeField] private int _coins;
    public Action<int, int> OnUpdateCoin;
    private void Awake()
    {
        _coins = 0;

        //if (DataManager.HasData(name))
        //    _coins = DataManager.LoadData<int>(name);
        //else
        //{
        //    _coins = 0;
        //}

    }

    public void AddCoin(int value){
        _coins += value;
        if (_coins < 0)
            _coins = 0;
        //DataManager.SaveData<int>(name, _coins);
        OnUpdateCoin?.Invoke(_coins, value);
    }

    public int GetCoins(){
        return _coins;
    }

    public string GetCoinsString(){
        return _coins.ToString();
    }

    public void UpdateCoin() => OnUpdateCoin?.Invoke(_coins, 0);

    public void SetCoin(int value){
        _coins = value;
        OnUpdateCoin?.Invoke(_coins, value);
    }

    public bool Buy(int cost){
        if (_coins >= cost){
            _coins -= cost;
            OnUpdateCoin?.Invoke(_coins, 0);
            return true;
        }
        return false;
    }

    public bool CanBuy(float cost){
        return _coins >= cost;
    }
}