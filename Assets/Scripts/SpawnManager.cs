using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Xml.Schema;
// using UnityEditor.Build;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] 
    private GameObject _virusPrefab;

    [SerializeField]
    private GameObject _uvLightPrefab;

    [SerializeField]
    private float _delay = 2f;
    
    [SerializeField]
    private float _powerUpSpawnRate = 30f;

    [SerializeField]
    public bool _spawningOn = false;
     
    [SerializeField] 
    private UIManager _uiManager;

    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Spwanmanager Start");
        StartCoroutine(SpawnSystem());
        StartCoroutine(SpawnPowerup());

    }

    
    // Update is called once per frame
    void Update()
    {        
        Debug.Log("Spwanmanager Update");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space pressed");
            _uiManager.StartFinished();
        }

    }
    
    
    // todo create public function, accessed by player, _spawningOn true/false
    public void onPlayerDeath()
    {
        _spawningOn = false;
    }
    
    
    // instantiate a Corona every 2 seconds (on random position)
    IEnumerator SpawnSystem()
    {
        // forever = as log as the game is running
        while (_spawningOn)
        {
            // spawn a new virus
            Instantiate(_virusPrefab, new Vector3(Random.Range(-8.5f,8.5f),7.5f,0), Quaternion.identity, this.transform); 
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
