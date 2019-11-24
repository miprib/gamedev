using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaCollectable : MonoBehaviour
{
    public ParticleSystem bananaCollectedParticles;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "RedPlayer")
        {
            Debug.Log("Red took the banana");
            BananaWasCollected();
        }

        if(other.tag == "BluePlayer") {
            Debug.Log("Blue took the banana");
            BananaWasCollected();
        }   
    }

    private void BananaWasCollected() {
        SoundManagerScript.PlaySound("collectable");
        Instantiate(bananaCollectedParticles, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
