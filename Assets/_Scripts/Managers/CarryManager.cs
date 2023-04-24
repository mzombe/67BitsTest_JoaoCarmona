using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarryManager : MonoBehaviour
{
    [SerializeField] private Transform[] loadNPC;
    [SerializeField] private int quantity;
    [SerializeField] private int limit;
    [SerializeField] private Text txtQuantity;

    public static CarryManager Instance;
    private void Start() {
        Instance = this;
    }

    private void Update() {
        txtQuantity.text = quantity.ToString() + "/" + limit.ToString();
    }

    public void AddOneNPC(){
        if(quantity < loadNPC.Length && quantity <= limit){
            if(!loadNPC[quantity].gameObject.activeSelf) loadNPC[quantity].gameObject.SetActive(true);
            quantity++;
        }
    }

    public void RemoveNPC(){
        ScoreManager.Instance.AddScore(10 * quantity);
        for (int i = 0; i < loadNPC.Length; i++){
            loadNPC[i].gameObject.SetActive(false);
            if(quantity > 0) quantity--;
        } 
    }

    public bool Cancollect(){
        if(quantity < limit)
            return true;
        else
            return false;
    }

    public void AddLimit(){
        if(limit < loadNPC.Length) limit++;
    }

    public int GetQuantity(){
        return quantity;
    }

}
