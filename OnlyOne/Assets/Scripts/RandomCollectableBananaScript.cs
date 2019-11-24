using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCollectableBananaScript : MonoBehaviour
{

    public float lifetime;
    public GameObject collectableBanana;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lifetime = lifetime - Time.deltaTime;

        if (lifetime < 0)
        {
            Instantiate(collectableBanana, transform.position, transform.rotation);
            Destroy(gameObject);
        }

    }
}
