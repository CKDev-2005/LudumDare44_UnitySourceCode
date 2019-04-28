using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeText : MonoBehaviour
{

    public PlayerController player;

    public float[] UpgradeCost;
    public float[] upgradeAmount;
    private float actualUpgradeCost;

    public int[] purchaseAmount;
    public int[] maxPurchaseAmount;

    public TextMeshProUGUI[] upgradeText;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

        UpdateCost();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpgradeFuelCapacity()
    {
        actualUpgradeCost = UpgradeCost[0] * purchaseAmount[0] * -1;
        if (-player.health < actualUpgradeCost && purchaseAmount[0] <= maxPurchaseAmount[0])
        {
            player.maxHeath += upgradeAmount[0];
            float amount = actualUpgradeCost;
            player.ChangeHealth(amount);
            purchaseAmount[0]++;
        }
        UpdateCost();
    }

    public void UpgradeFuelEfficiency()
    {
        actualUpgradeCost = UpgradeCost[1] * purchaseAmount[1] * -1;
        if(-player.health < actualUpgradeCost && purchaseAmount[1] <= maxPurchaseAmount[1])
        {
            player.fuelEfficiency += upgradeAmount[1];
            float amount = actualUpgradeCost;
            player.ChangeHealth(amount);
            purchaseAmount[1]++;
        }
        UpdateCost();
    }

    public void UpgradeShotCost()
    {
        actualUpgradeCost = UpgradeCost[2] * purchaseAmount[2] * -1;
        if(-player.health < actualUpgradeCost && purchaseAmount[2] <= maxPurchaseAmount[2])
        {
            player.shotEfficiency += upgradeAmount[2];
            float amount = actualUpgradeCost;
            player.ChangeHealth(amount);
            purchaseAmount[2]++;
        }
        UpdateCost();
    }

    public void UpdateCost()
    {
        //0
        if(purchaseAmount[0] > maxPurchaseAmount[0])
        {
            upgradeText[0].text = "Sold Out";
        }
        else
        {
            actualUpgradeCost = UpgradeCost[0] * purchaseAmount[0];
            upgradeText[0].text = actualUpgradeCost.ToString();
        }
        //1
        if (purchaseAmount[1] > maxPurchaseAmount[1])
        {
            upgradeText[1].text = "Sold Out";
        }
        else
        {
            actualUpgradeCost = UpgradeCost[1] * purchaseAmount[1];
            upgradeText[1].text = actualUpgradeCost.ToString();
        }
        //2
        if (purchaseAmount[2] > maxPurchaseAmount[2])
        {
            upgradeText[2].text = "Sold Out";
        }
        else
        {
            actualUpgradeCost = UpgradeCost[2] * purchaseAmount[2];
            upgradeText[2].text = actualUpgradeCost.ToString();
        }

       
    }

}
