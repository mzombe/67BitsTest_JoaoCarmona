using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    [SerializeField] private GameObject npcPrefab;
    [SerializeField] private int startQuantity;
    [SerializeField] private float spawnAreaWidth;
    [SerializeField] private float spawnAreaHeight;

    private List<Vector3> usedPositions = new List<Vector3>();
    private List<GameObject> currentNPC = new List<GameObject>();

    void Start(){
        InstatiateNPC(startQuantity);
    }

    public void InstatiateNPC(int x){
        for (int i = 0; i < x; i++){
            Vector3 spawnPosition = GenerateSpawnPosition();
            float test = Random.Range(0f,360f);
            GameObject obj = Instantiate(npcPrefab, spawnPosition, Quaternion.Euler(0,test,0));
            OffScreenIndicators.Instance.AddTarget(obj.gameObject);
            currentNPC.Add(obj);
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        Vector3 spawnPosition = transform.position + new Vector3(Random.Range(-spawnAreaWidth / 2, spawnAreaWidth / 2), 0, Random.Range(-spawnAreaHeight / 2, spawnAreaHeight / 2));
        while (usedPositions.Contains(spawnPosition)){
            spawnPosition = transform.position + new Vector3(Random.Range(-spawnAreaWidth / 2, spawnAreaWidth / 2), 0, Random.Range(-spawnAreaHeight / 2, spawnAreaHeight / 2));
        }
        usedPositions.Add(spawnPosition);

        return spawnPosition;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(-spawnAreaWidth,0.5f,spawnAreaHeight));
    }

}
