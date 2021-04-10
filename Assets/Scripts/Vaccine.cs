using System.Collections;
using System.Collections.Generic;
// using UnityEditor.SceneManagement;
using UnityEngine;

public class Vaccine : MonoBehaviour
{
   // we need a speed variable: // (Start) moving speed vaccine
   [SerializeField] 
   private float _vaccineSpeed = 5.0f;


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
    }  // This also deactivates the player releasing vaccines. Why??
        // Wrong object linked to the script (do not use those in Hierarchy, but in Assets/Prefabs
}
