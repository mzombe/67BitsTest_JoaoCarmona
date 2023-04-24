using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffScreenIndicators : MonoBehaviour
{
    [SerializeField] private List<Indicator> targetIndicators;
    [SerializeField] private GameObject indicatorPrefab;
    [SerializeField] private float checkTime = 0.1f;
    [SerializeField] private Vector2 offset;
    [SerializeField] private Vector2 offsetScale;
    [SerializeField] private float distanceLimit;
    [SerializeField] private Transform parentToInstantiate;
    
    private Camera _activeCamera;

    public static OffScreenIndicators Instance;

    private void Awake() {
        Instance = this;
    }

    private void Start(){
        _activeCamera = Camera.main;
        InstantiateIndicators();
        StartCoroutine(UpdateIndicators());
    }

    public void AddTarget(GameObject targeObject){
        targetIndicators.Add(new Indicator(){
            target = targeObject.transform
        });
        InstantiateIndicators();
    }

    private void InstantiateIndicators(){
        foreach (var targetIndicator in targetIndicators)
        {
            if(targetIndicator.indicatorUI == null){
                targetIndicator.indicatorUI = Instantiate(indicatorPrefab).transform;
                targetIndicator.indicatorUI.transform.parent = parentToInstantiate;
            }
            var rectTransform = targetIndicator.indicatorUI.GetComponent<RectTransform>();
            if(rectTransform == null){
                rectTransform = targetIndicator.indicatorUI.gameObject.AddComponent<RectTransform>();
            }
            targetIndicator.rectTransform = rectTransform;
        }
    }

    void UpdatePosition(Indicator targetIndicator){
        if(targetIndicator.target != null){
            var rect = targetIndicator.rectTransform.rect;
            var indicatorPosition = _activeCamera.WorldToScreenPoint(targetIndicator.target.position);
            if (indicatorPosition.z < 0){
                indicatorPosition.y = -indicatorPosition.y;
                indicatorPosition.x = -indicatorPosition.x;
            }
            var newPosition = new Vector3(indicatorPosition.x, indicatorPosition.y, indicatorPosition.z);
            indicatorPosition.x = Mathf.Clamp(indicatorPosition.x, rect.width / 2, Screen.width - rect.width / 2) + offset.x;
            indicatorPosition.y = Mathf.Clamp(indicatorPosition.y, rect.height / 2, Screen.height - rect.height / 2) + offset.y;
            indicatorPosition.z = 0;
            targetIndicator.indicatorUI.up = (newPosition - indicatorPosition).normalized;
            targetIndicator.indicatorUI.position = indicatorPosition;
            if (Mathf.Abs((targetIndicator.target.gameObject.transform.position - _activeCamera.transform.position).x) <= distanceLimit){
                targetIndicator.indicatorUI.gameObject.SetActive(false);
            }
            else{
                targetIndicator.indicatorUI.gameObject.SetActive(true);
            }
        }else{
            targetIndicators.Remove(targetIndicator);
            Destroy(targetIndicator.indicatorUI.gameObject);
        }
    }
    IEnumerator UpdateIndicators(){
        for (int i = 0; i < targetIndicators.Count; i++){
            if(targetIndicators[i].rectTransform == null){
                yield return null;
            }else{
                targetIndicators[i].rectTransform.localScale = offsetScale;
                UpdatePosition(targetIndicators[i]);
            }
        }
        yield return new WaitForSeconds(checkTime);
        StartCoroutine(UpdateIndicators());
    }

}

[System.Serializable]
public class Indicator
{
    public Transform target;
    public Transform indicatorUI;
    public RectTransform rectTransform;

}