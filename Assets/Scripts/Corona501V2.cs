using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corona501V2 : MonoBehaviour
{
    // (Start) moving speed Corona:
    [SerializeField] 
    private float _infectionSpeed = 3.0f;  // or 5.0
    
    // Virus
    [SerializeField] 
    private GameObject _vaccinePrefab;

 

    // Update is called once per frame
    void Update()
    {
        // vaccine-object moving: move the Corona object
        transform.Translate(Vector3.down * _infectionSpeed  * Time.deltaTime );

        
        
        
        void OnTriggerEnter(Collider other)
        {
            // this = Corona Virus
            // Other = Player oder Vaccine (je nach Kontext)
            // Debug.Log(other.name);
            // Debug.LogWarning(other.name);
            // Debug.LogError(other.name);

            // if the Corona collides with the player:
            if (other.CompareTag("Player"))
            {
                // damage player or destroy it   and virus:
                // Debug.LogWarning("player health not implemented");
                // Damage() in Player.cs aufrufen:
                other.GetComponent<Player>().Dammage();
                Destroy(this.gameObject);
            }
            // if the Corona collides with the Vaccine:
            else if (other.CompareTag("Vaccine"))
            {
                // Debug.LogWarning("Vaccine hit");
                // destroy both
                // imunify against UVlight:
                // if (! other.name.Contains("UVlight"))
                {
                    Destroy(other.gameObject);

                }

                
                
                
                GameObject.FindWithTag("Player").GetComponent<Player>().RelayScore(10);
            
                Destroy(this.gameObject);
            }
        
        }

    }
}
