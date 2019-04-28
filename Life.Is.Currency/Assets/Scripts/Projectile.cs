using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public Rigidbody2D rb;

    public float force;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(transform.up * force);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Asteroid"))
        {
            collision.gameObject.GetComponent<Asteroid>().Destroy();

            Destroy(gameObject);
        }
    }

}
