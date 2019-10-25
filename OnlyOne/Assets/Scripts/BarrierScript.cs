using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierScript : MonoBehaviour
{

    public Transform playerWhoCannotPass;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Script started");
    }

    //     private void OnTriggerEnter2D(Collider2D collider)
    // {
    //     if(collider.tag == playerWhoCannotPass.tag) {

    //         //playerWhoCannotPass.position
    //     }
    // }

        void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != playerWhoCannotPass.tag) {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
