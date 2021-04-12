using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corona501V2 : MonoBehaviour
{
    // (Start) moving speed Corona:
    [SerializeField] 
    private float _infectionSpeed = 3.0f;  // or 5.0
    
    // Eval Virus 501V2
    [SerializeField] 
    private GameObject _evalVaccinePrefab;

    [SerializeField] 
    private float _incidentRate = 2.0f;

    [SerializeField]
    private float _canInfect = -1f;
    
    
 

    // Update is called once per frame
    void Update()
    {
        // vaccine-object moving: move the Corona object
        transform.Translate(Vector3.down * _infectionSpeed * Time.deltaTime);
        Infect();
    }



    public void Infect()
    {
        // SPACE pressed?  time to wait long enough // then instantiate vaccine prefab
        if (Time.time > _canInfect)
        {
            _canInfect = Time.time + _incidentRate;
            Instantiate(_evalVaccinePrefab, transform.position + new Vector3(0,-0.7f,0), Quaternion.identity); 
        }
            
    }
    
    

    void OnTriggerEnter(Collider other)
    {
        // if the Corona collides with the player:
        if (other.CompareTag("Player"))
        {
            // damage player or destroy it   and virus:
            other.GetComponent<Player>().Dammage();
            Destroy(this.gameObject);
        }
        // if the Corona collides with the Vaccine:
        else if (other.CompareTag("Vaccine"))
        {
            // imunify against UVlight:
            // if (! other.name.Contains("UVlight"))
            {
                Destroy(other.gameObject);
            }
           
            
            GameObject.FindWithTag("Player").GetComponent<Player>().RelayScore(5);
        
            Destroy(this.gameObject);
        }
    
    }

}
