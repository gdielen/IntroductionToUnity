using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PowerupCollectables : MonoBehaviour
{
    [SerializeField]
    // Speed for Goodie
    private float _speed = 2.0f;
    

    // Update is called once per frame
    void Update()
    {
        transform.Translate((Vector3.down * _speed * Time.deltaTime));
        // destroy when bottom reached:
        if (transform.position.y < - 5)
        {
            Destroy(this.gameObject);
        }
    }

    
    // check for collussion:
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().ActivatePowerUp();
            Destroy(this.gameObject);
        }
    }
        
}
