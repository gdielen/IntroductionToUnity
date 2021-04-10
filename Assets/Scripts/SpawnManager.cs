using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
// using UnityEditor.Build;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] 
    private GameObject _virusPrefab;

    [SerializeField]
    private float _delay = 2f;

    private bool _spawningOn = true;
     
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnSystem());
    }

    // Update is called once per frame
    void Update()
    {
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
            Instantiate(_virusPrefab, new Vector3(Random.Range(-8f,8f),7f,0), Quaternion.identity, this.transform); 

            // wait for 2 seconds of delay
            yield return new WaitForSeconds(_delay);
        }
        // destroy all Corona:
        Destroy(this.gameObject);
    }
}
