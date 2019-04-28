using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeStation : MonoBehaviour
{

    public GameObject upgradeText;


    float startRot;
    public float maxRot;

    public float minStartForce;
    public float maxStartForce;
    public float startForce;

    public Transform center;

    public Rigidbody2D rb;

    public float xThreshold;
    public float yThreshold;

    private bool hasDocked;

    public Transform dock;

    // Start is called before the first frame update
    void Start()
    {
        upgradeText = GameObject.FindWithTag("UpgradeText").transform.GetChild(0).gameObject;

        rb = GetComponent<Rigidbody2D>();

        center = GameObject.FindWithTag("GameController").GetComponent<Transform>();


        startRot = Random.Range(-maxRot, maxRot);

        Vector2 target = transform.position - center.position;
        target.Normalize();

        float rot = Mathf.Atan2(target.x, target.y) * -Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(rot - 180 + startRot, Vector3.forward);

        startForce = Random.Range(minStartForce, maxStartForce);

        rb.AddForce(transform.up * startForce);

        dock = transform.GetChild(0).GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate(PlayerController player)
    {
        if (!hasDocked)
        {
            player.docked = true;

            player.gameObject.GetComponent<Collider2D>().isTrigger = true;

            upgradeText.SetActive(true);
        }
        
    }

    public void Deactivate(PlayerController player)
    {
        player.docked = false;

        player.gameObject.GetComponent<Collider2D>().isTrigger = false;

        upgradeText.SetActive(false);

        hasDocked = true;
    }
}
