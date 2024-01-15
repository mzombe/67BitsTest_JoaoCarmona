using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    [SerializeField] private GameObject npcPrefab;
    [SerializeField] private int startQuantity;
    [SerializeField] private float spawnAreaWidth;
    [SerializeField] private float spawnAreaHeight;
    [SerializeField] private LayerMask _layerNotSpawn;

    private List<Vector3> usedPositions = new List<Vector3>();
    private List<GameObject> _currentNPCS = new List<GameObject>();
    public int npcCount { get {  return _currentNPCS.Count; } }

    void Start(){
        InstatiateNPC(startQuantity);
    }

    public void InstatiateNPC(int x){
        for (int i = 0; i < x; i++){
            Vector3 spawnPosition = GenerateSpawnPosition();
            float test = Random.Range(0f,360f);
            GameObject obj = Instantiate(npcPrefab, spawnPosition, Quaternion.Euler(0,test,0));
            OffScreenIndicators.Instance.AddTarget(obj.gameObject);
            _currentNPCS.Add(obj);

            if (obj.gameObject.TryGetComponent<NPC>(out NPC npc)){
                npc.SetNpcSpawner(this);
            }
        }
    }

    private Vector3 GenerateSpawnPosition(){
        Vector3 spawnPosition = RandomPosition();

        while (VerifyArea(spawnPosition)){
            spawnPosition = RandomPosition();
        }

        while (usedPositions.Contains(spawnPosition)){
            spawnPosition = RandomPosition();
        }
        usedPositions.Add(spawnPosition);

        return spawnPosition;
    }
    private Vector3 RandomPosition(){
        return transform.position + new Vector3(Random.Range(-spawnAreaWidth / 2, spawnAreaWidth / 2), 0, Random.Range(-spawnAreaHeight / 2, spawnAreaHeight / 2));
    }
    private bool VerifyArea(Vector3 pos){
        Ray ray = new Ray(new Vector3(pos.x, -9, pos.z), Vector3.up * 10);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 10, _layerNotSpawn, QueryTriggerInteraction.Collide)){
            Debug.DrawRay(pos, Vector3.up * 10, Color.red, 5f);
            return true;
        }else{
            Debug.DrawRay(pos, Vector3.up * 10, Color.green, 5f);
            return false;
        }
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(-spawnAreaWidth,0.5f,spawnAreaHeight));
    }

    public void RemoveNPC(GameObject npc) => _currentNPCS.Remove(npc);

}
