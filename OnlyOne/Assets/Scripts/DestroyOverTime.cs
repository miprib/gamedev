﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{

    public float lifetime;

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
            Destroy(gameObject);
        }

    }
}
