using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{

    public PlayerController player;

    public Rigidbody2D rb;

    public float thrust;
    public float thrustCost;

    public float health;
    public float maxHeath;
    public Slider healthBar;

    public float invincibilityTime;
    private float timeInvincible;
    public bool invincible;


    public GameObject projectile;
    public Transform shotPoint;
    public float shotTime;
    public float timeBtwShots;
    public float shotCost;

    public float xThreshold;
    public float yThreshold;

    public bool docked;

    public float fuelEfficiency = 1;
    public float shotEfficiency = 1;

    public UpgradeStation station;
    public float stationInvincibleTime;
    public Transform dock;

    public GameObject inGameMenu;
    public int score;
    public TextMeshProUGUI scoreText;

    public GameObject particleEffects;
    public bool particlesActive;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        player = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (docked)
        {
            rb.velocity = Vector2.zero;
            transform.position = dock.position;

            if (Input.GetButtonDown("Fire2"))
            {
                station.Deactivate(player);

                timeInvincible = stationInvincibleTime;
                invincible = true;
            }
        }
        else
        {
            Movement();

            Shooting();

            Invincibility();
        }


        score += 1;
        scoreText.text = score.ToString();
        

        if(transform.position.x > xThreshold || transform.position.x < -xThreshold || transform.position.y > yThreshold || transform.position.y < -yThreshold)
        {
            GameOver();
        }
    }

    public void Movement()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 target = new Vector2(transform.position.x, transform.position.y) - mousePos;
        target.Normalize();

        float rot = Mathf.Atan2(target.x, target.y) * -Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(rot - 180, Vector3.forward);

        if (Input.GetButton("Fire1") && health > thrustCost)
        {
            rb.AddForce(transform.up * thrust);

            if(!particlesActive)
            {
                particleEffects.SetActive(true);
                particlesActive = true;
            }

            float amount = -thrustCost / fuelEfficiency;
            ChangeHealth(amount);
        }else if (particlesActive)
        {
            particleEffects.SetActive(false);
            particlesActive = false;
        }   
    }

    public void Shooting()
    {
        if (Input.GetButtonDown("Fire2") && shotTime < 0 && health > shotCost)
        {
            Instantiate(projectile, shotPoint.position, transform.rotation);

            float amount = -shotCost / shotEfficiency;

            ChangeHealth(amount);
        }
        else if (shotTime >= 0)
        {
            shotTime -= Time.deltaTime;
        }
    }

    public void Invincibility()
    {
        if (invincible && timeInvincible < 0)
        {
            invincible = false;
        }
        else if (invincible)
        {
            timeInvincible -= Time.deltaTime;
        }
    }


    public void ChangeHealth(float amount)
    {
        health += amount;
        health = Mathf.Clamp(health, 0, maxHeath);

        healthBar.value = health / maxHeath;

        if(health <= 0) 
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        if(docked == true)
        {
            station.Deactivate(player);
        }

        inGameMenu.SetActive(true);

        if(score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Asteroid") && !invincible && !docked)
        {
            float amount = collision.gameObject.GetComponent<Asteroid>().damage * -1;

            ChangeHealth(amount);

            timeInvincible = invincibilityTime;
            invincible = true;
        }
        


    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Fuel") && !docked)
        {
            float amount = collision.gameObject.GetComponent<Fuel>().fuelAmount;

            ChangeHealth(amount);

            Destroy(collision.gameObject);
        }
        if(collision.gameObject.layer == LayerMask.NameToLayer("UpgradeStation") && !docked)
        {
            

            
            station = collision.GetComponent<UpgradeStation>();
            station.dock.position = transform.position;
            dock = station.transform.GetChild(0).GetComponent<Transform>();
            station.Activate(player);
        }
    }

}
