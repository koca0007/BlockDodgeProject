using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    Rigidbody2D rb;

    private float gravityDivider = 15f;
    private float YBoundary = -2f;
    public float gravityLimit = 1.8f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale += Time.timeSinceLevelLoad / gravityDivider;
        

    }
    void Update()
    {
        if (transform.position.y < YBoundary )
        {
            Destroy(this.gameObject);
        }
        
    }
}
