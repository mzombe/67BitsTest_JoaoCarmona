using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private GameEvent m_eventActive;
    [SerializeField] private GameObject m_screen;

    private void OnEnable(){
        m_eventActive.Subcribe(ActiveScreen);
    }

    private void OnDisable(){
        m_eventActive.Unsubcribe(ActiveScreen);
    }

    private void ActiveScreen(){
        m_screen.SetActive(true);
    }
}
