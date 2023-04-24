using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineShake : MonoBehaviour
{
    [SerializeField] private GameEvent punchEvent;
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private float shakeTimer;
    private float startingInstensity;
    private float shakeTimerTotal;

    private void Start() {
        cinemachineVirtualCamera = this.gameObject.GetComponent<CinemachineVirtualCamera>();
    }

    private void OnEnable() {
        punchEvent.Subcribe(ShakeCamera);
    }

    private void OnDisable() {
        punchEvent.Unsubcribe(ShakeCamera);
    }

    public void ShakeCamera(){
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = 
            cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 1;
        shakeTimer = 0.5f;
        
        startingInstensity = 1;
        shakeTimerTotal = 0.5f;
    }

    private void Update() {
        if(shakeTimer > 0){
            shakeTimer -= Time.deltaTime;
            if(shakeTimer <= 0f){
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = 
                    cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(startingInstensity, 0f, 1-(shakeTimer / shakeTimerTotal));
                
            }   
        }
    }
}
