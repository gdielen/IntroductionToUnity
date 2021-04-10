using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
// using UnityEditor.Build;
using UnityEngine;

public class BloodManager : MonoBehaviour
{
    [SerializeField] 
    private GameObject _bloodPrefab;

    [SerializeField]
    private float _delay = 0.5f;

    private bool _bloodingOn = true;
     
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BloodSystem());
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    
    // todo create public function, accessed by player, _spawningOn true/false
    public void onPlayerDeath()
    {
        _bloodingOn = false;
    }
    
    
    // instantiate a Blood every "_delay" seconds (on random position)
    IEnumerator BloodSystem()
    {
        // forever = as log as the game is running
        while (_bloodingOn)
        {
            // spawn a new virus
            Instantiate(_bloodPrefab, new Vector3(8f,Random.Range(-8f,8f),0), Quaternion.identity, this.transform); 

            // wait for 2 seconds of delay
            yield return new WaitForSeconds(_delay);
        }
        // destroy all Blood:
        Destroy(this.gameObject);
    }
}