using System.Collections;
using System.Collections.Generic;
// using UnityEditor.SceneManagement;
using UnityEngine;

public class Vaccine : MonoBehaviour
{
   // we need a speed variable: // (Start) moving speed vaccine
   [SerializeField] 
   private float _vaccineSpeed = 7.0f;  // or 5.0


   // Update is called once per frame
    void Update()
    {
        // vaccine-object moving: move the vaccine object
        transform.Translate(Vector3.up  * _vaccineSpeed * Time.deltaTime);
       
        // vaccine destroy when too high:
        if (transform.position.y > 7F)
        { 
            Destroy(this.gameObject);
        }
    }  
    
}
