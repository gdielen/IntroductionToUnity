using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Xml.Schema;
using Unity.IO.LowLevel.Unsafe;
// using UnityEditor.Build;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Random")]
        [Range(0f,1f)]
        [SerializeField] 
        private float _normalCoronaSpawnChance = 0.0f;
        
        [Range(0f,1f)]
        [SerializeField] 
        private float _chanceModifier = 0.01f;
    
    [Header("Virus Prefabs")]
        [SerializeField] 
        private List<GameObject> _virusPrefab;
    
    [Header("Settings")]
        [SerializeField]
        private GameObject _uvLightPrefab;

        [SerializeField]
        private float _delay = 2f;
        
        [SerializeField]
        private float _powerUpSpawnRate = 5f;

        [SerializeField]
        public bool _spawningOn = false;
         
        [SerializeField] 
        private UIManager _uiManager;

    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Spawnmanager Start");
        StartCoroutine(SpawnSystem());
        StartCoroutine(SpawnPowerup());

    }

    
    
    public void onPlayerDeath()
    {
        _spawningOn = false;
    }
    
    
    // instantiate a Corona every 2 seconds (on random position)
    
    private int SelectVirusIndex()
    {
        int virusIndex = 0;
        if (_normalCoronaSpawnChance < Random.Range(0.0f, 1.0f))
        {
            _normalCoronaSpawnChance += _chanceModifier;
            virusIndex = 0;
        }
        else
        {
            virusIndex = Random.Range(1, _virusPrefab.Count);
        }
        return virusIndex;
    }
    
    
    
    
    
    
    
    IEnumerator SpawnSystem()
    {
        // forever = as log as the game is running
        while (_spawningOn)
        {
            // spawn a new virus
            Instantiate(_virusPrefab[SelectVirusIndex()], new Vector3(Random.Range(-8.5f,8.5f),7.5f,0), Quaternion.identity, this.transform); 
            // wait for _delay - seconds of delay
            yield return new WaitForSeconds(_delay);
        }
        // destroy all Corona:
        Destroy(this.gameObject);
    }
    
    IEnumerator SpawnPowerup()
    {
        while (_spawningOn)
        {
            Instantiate(_uvLightPrefab, new Vector3(Random.Range(-8.5f,8.5f),7.5f,0), Quaternion.identity, this.transform);
            yield return new WaitForSeconds(_powerUpSpawnRate);  
        }
    }
    
}
