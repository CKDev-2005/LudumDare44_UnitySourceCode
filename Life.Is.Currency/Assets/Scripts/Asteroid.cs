using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    float startRot;
    public float maxRot;

    public float minStartForce;
    public float maxStartForce;
    public float startForce;

    public Transform center;

    public Rigidbody2D rb;

    public float damage;

    public float xThreshold;
    public float yThreshold;

    public GameObject particleEffect;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        center = GameObject.FindWithTag("GameController").GetComponent<Transform>();


        startRot = Random.Range(-maxRot, maxRot);

        Vector2 target = transform.position - center.position;
        target.Normalize();

        float rot = Mathf.Atan2(target.x, target.y) * -Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(rot - 180 + startRot, Vector3.forward);

        startForce = Random.Range(minStartForce, maxStartForce);

        rb.AddForce(transform.up * startForce);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > xThreshold || transform.position.x < -xThreshold || transform.position.y > yThreshold || transform.position.y < -yThreshold)
        {
            Destroy();
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public void Split()
    {

    }
}