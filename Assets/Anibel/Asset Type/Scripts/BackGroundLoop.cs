using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundLoop : MonoBehaviour
{
    private float plusY = 30f;
    
    private void Update()
    {
        if (transform.position.y < -10.5f)
        {
            transform.position = new Vector2(0, transform.position.y + plusY);
        }
    }
}
