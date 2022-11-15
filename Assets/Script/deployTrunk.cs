using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deployTrunk : MonoBehaviour
{
    public GameObject[] trunkPrefab;
    GameObject[] instanciatedObjects;
    public float respawnTime = 20.0f;
    Vector2 screenBounds;
    
    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(trunkWave());
    }

    void spawnTrunk()
    {
        instanciatedObjects = new GameObject[trunkPrefab.Length];
        for (int i = 0; i < trunkPrefab.Length; i++)
        {
            instanciatedObjects[i] = Instantiate(trunkPrefab[i]) as GameObject;
            instanciatedObjects[i].transform.position = new Vector2(screenBounds.x * 1, Random.Range(-screenBounds.y, screenBounds.y));
        }        
    }

    IEnumerator trunkWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime);
            spawnTrunk();
        }
    }
}
