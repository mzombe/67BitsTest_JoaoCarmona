using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CarryController : MonoBehaviour
{
    [SerializeField] private int _quantity;
    [SerializeField] private int _currentLimit;
    [SerializeField] private Text _txtQuantity;

    public static CarryController Instance;
    private int _maxLimit;
    private void Start() {
        Initialize();
    }

    private void Initialize(){
        Instance = this;
        _maxLimit = CarryManager.Npcs.Count;
        if (_quantity > 0) SetNPC(true);

        UpdateDisplay();
        //SetHierarchy();
    }
    private void SetHierarchy(){
        //for (int i = 0; i < CarryManager.Npcs.Count; i++){
        //    if(i != 0) CarryManager.Npcs[i].transform.SetParent(CarryManager.Npcs[i - 1].transform, true);
        //}
        
    }
    private void UpdateDisplay() => _txtQuantity.text = _quantity.ToString() + "/" + _currentLimit.ToString();

    private void SetNPC(bool appear){
        if (_quantity <= CarryManager.Npcs.Count && _quantity <= _currentLimit){
            for (int i = 0; i < _quantity; i++){
                CarryManager.Npcs[i].transform.GetChild(0).gameObject.SetActive(appear);
            }
        }

        UpdateDisplay();
        UpdateCamHeight();
    }
    private void UpdateCamHeight(){
        if (_quantity < CarryManager.Npcs.Count / 2)
        {
            VirtualCamController.Instance.ChangeHeight(12);
        }

        if (_quantity > CarryManager.Npcs.Count / 2 && _quantity < CarryManager.Npcs.Count)
        {
            VirtualCamController.Instance.ChangeHeight(16);
        }

        if (_quantity == CarryManager.Npcs.Count)
        {
            VirtualCamController.Instance.ChangeHeight(20);
        }
    }
    public void AddNPC(int value){
        _quantity += value;
        SetNPC(true);
        UpdateCamHeight();
    }

    public void RemoveAllNPC(){
        SetNPC(false);
        _quantity = 0;
        UpdateDisplay();
        UpdateCamHeight();
    }

    public bool Cancollect(){
        return _quantity < _currentLimit;
    }

    public void AddLimit(){
        if(_currentLimit < CarryManager.Npcs.Count) _currentLimit++;
        UpdateDisplay();
    }

    public int GetQuantity(){
        return _quantity;
    }

}
