using System.Collections;
using System.Collections.Generic;
// using UnityEditor.SceneManagement;
using UnityEngine;

public class Vaccine : MonoBehaviour
{
   // we need a speed variable: // (Start) moving speed vaccine
   [SerializeField] 
   private float _vaccineSpeed = 7.0f;  // or 5.0
   [SerializeField] 
   private float _rotationSpeed = 30.0f; 


   // Update is called once per frame
    void Update()
    {
        // vaccine-object moving: move the vaccine object
        transform.Rotate(new Vector3(0f, _rotationSpeed * Time.deltaTime), 0f, Space.Self);
        
        if (CompareTag("Vaccine"))
        {
            transform.Translate(Vector3.up  * _vaccineSpeed * Time.deltaTime);
            // vaccine destroy when too high:
            if (transform.position.y > 7F)
            { 
                Destroy(this.gameObject);
            }
        }
        else
        {
            transform.Translate(Vector3.down  * _vaccineSpeed * Time.deltaTime);
            // vaccine destroy when too high:
            if (transform.position.y < -7F)
            { 
                Destroy(this.gameObject);
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
                if (! other.name.Contains("UVlight"))
                {
                    Destroy(other.gameObject);
                }

                Destroy(this.gameObject);
            }
        }

    }
    
}
