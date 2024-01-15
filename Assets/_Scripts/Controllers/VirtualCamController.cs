using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

public class VirtualCamController : MonoBehaviour
{
    private CinemachineVirtualCamera _vcCam;
    private static VirtualCamController _instance;
    public CinemachineVirtualCamera VcCam {  get { return _vcCam; } }
    public static VirtualCamController Instance { get { return _instance; } }
    private void Awake(){
        _instance = this;
        _vcCam = GetComponent<CinemachineVirtualCamera>();
    }
    public void ChangeHeight(float value){
        CinemachineTransposer transposer = _vcCam.GetCinemachineComponent<CinemachineTransposer>();
        transposer.m_FollowOffset.y = value;
    }
}
