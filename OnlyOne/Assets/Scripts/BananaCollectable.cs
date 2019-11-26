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
            BananaWasCollected(true);
        }

        if(other.tag == "BluePlayer") {
            Debug.Log("Blue took the banana");
            BananaWasCollected(false);
        }   
    }

    private void BananaWasCollected(bool isRed) {
        SoundManagerScript.PlaySound("collectable");
        Instantiate(bananaCollectedParticles, transform.position, transform.rotation);
        Destroy(gameObject);

        Object[] players = FindObjectsOfType<PlayerController>();
        Debug.Log(players + " : " + players.Length);
        Debug.Log("isRed : " + isRed);
        foreach (PlayerController player in players)
        {
            Debug.Log(player + " isRed : " + player.isRed);
            Debug.Log(player + " isBlue : " + player.isBlue);
            if (player.isRed && isRed)
            {
                player.addBanana = true;
            }

            if (player.isBlue && !isRed)
            {
                player.addBanana = true;
            }
        }
    }
}
