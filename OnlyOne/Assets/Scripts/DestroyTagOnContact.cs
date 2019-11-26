using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTagOnContact : MonoBehaviour
{
    public String tagToDestroy;
    public Transform redPlayer;
    public Transform bluePlayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == redPlayer.tag ||
            collision.gameObject.tag == bluePlayer.tag)
        {
            Destroy(GameObject.FindWithTag(tagToDestroy));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
