using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerScript : MonoBehaviour
{
   
    public float damageDone;
    public int moneySpent;
    public int[] upgradeIndex;
    public GameObject rangeDisplay;
    Button button1;
    Text button1Text;
    Button button2;
    Text button2Text;
    public TowerUpgradesSO towerUpgrades;
    public ButtonScript btnScript;



    public GameController gameCon;


    void Start()
    {
        //Get all tower stats from GameController. Save space by giving towers only their stats instead of the stats of every tower type.
        GetComponent<BoxCollider2D>().size = new Vector2(GetComponent<Shooting>().towerStats.colliderLength, GetComponent<Shooting>().towerStats.colliderHeight);
        GetComponent<BoxCollider2D>().offset = new Vector2(GetComponent<Shooting>().towerStats.colliderOffsetX, GetComponent<Shooting>().towerStats.colliderOffsetY);
        gameCon = GameObject.Find("GameController").GetComponent<GameController>();
        upgradeIndex = new int[2];
        upgradeIndex[1] = 5;
        gameCon.GetComponent<GameController>().activePhantomTower = -1;

        button1 = GameObject.Find("Upgrade1").GetComponent<Button>();
        button1Text = GameObject.Find("Upgrade1Text").GetComponent<Text>();
        button2 = GameObject.Find("Upgrade2").GetComponent<Button>();
        button2Text = GameObject.Find("Upgrade2Text").GetComponent<Text>();

        moneySpent += GetComponent<Shooting>().towerStats.cost;

    }


    void selectTower()
    {
        newRangeDisplays();
        openUpgradeMenu();
    }

    void newRangeDisplays()
    { 
        GameObject[] rangeDisplays = GameObject.FindGameObjectsWithTag("RangeDisplay");
        foreach (GameObject i in rangeDisplays)
        {
            Destroy(i);
        }
            
        var rangeDisplayVar = Instantiate(rangeDisplay, gameObject.transform.position, Quaternion.identity);
        rangeDisplayVar.transform.localScale = new Vector2(GetComponent<Shooting>().towerStats.range, GetComponent<Shooting>().towerStats.range);
    }

    void changeButtonStuff()
    {
        if (upgradeIndex[0] == 5 || (upgradeIndex[0] == 2 && upgradeIndex[1] - 5 >= 3))
        {
            button1.GetComponent<UpgradeButtonScript>().maxed = true;
        }
        else
        {
            button1.GetComponent<UpgradeButtonScript>().maxed = false;
            button1.GetComponent<UpgradeButtonScript>().cost = towerUpgrades.cost[upgradeIndex[0]];
            button1.GetComponent<UpgradeButtonScript>().tooltipText = towerUpgrades.descriptions[upgradeIndex[0]];
            button1.GetComponentInChildren<Text>().text = ("Buy for: " + towerUpgrades.cost[upgradeIndex[0]] + "\n");           
            
        }


        if (upgradeIndex[1] - 5 == 5 || (upgradeIndex[1] - 5 == 2 && upgradeIndex[0] >= 3))
        {
            button2.GetComponent<UpgradeButtonScript>().maxed = true;
        }
        else
        {
            button2.GetComponent<UpgradeButtonScript>().maxed = false;
            button2.GetComponent<UpgradeButtonScript>().cost = towerUpgrades.cost[upgradeIndex[1]];
            button2.GetComponent<UpgradeButtonScript>().tooltipText = towerUpgrades.descriptions[upgradeIndex[1]];
            button2.GetComponentInChildren<Text>().text = ("Buy for: " + towerUpgrades.cost[upgradeIndex[1]] + "\n");
        }
    }

    void openUpgradeMenu()
    {
        changeButtonStuff();


        GameObject.Find("UpgradeMenu").GetComponent<UpgradeMenuScript>().activeTower = this.gameObject;
        Vector3 upgradeMenuV = gameCon.upgradeMenu.transform.position;
        upgradeMenuV.y = -51;
        gameCon.upgradeMenu.transform.position = upgradeMenuV;
    }

    public void upgrade(int path)
    {
        if (gameCon.GetComponent<GameController>().money >= towerUpgrades.cost[upgradeIndex[path]])        
        {
            gameCon.GetComponent<GameController>().money -= towerUpgrades.cost[upgradeIndex[path]];
            moneySpent += towerUpgrades.cost[upgradeIndex[path]];

            if (towerUpgrades.range[upgradeIndex[path]] != 0)
            {
                GetComponent<Shooting>().towerStats.range *= (towerUpgrades.range[upgradeIndex[path]] + 1);
                newRangeDisplays();
            }
            if (towerUpgrades.damage[upgradeIndex[path]] != 0)
            {
                GetComponent<Shooting>().towerStats.damage *= (towerUpgrades.damage[upgradeIndex[path]] + 1);
            }
            if (towerUpgrades.frequency[upgradeIndex[path]] != 0)
            {
                GetComponent<Shooting>().towerStats.frequency *= (towerUpgrades.frequency[upgradeIndex[path]] + 1);
                GetComponent<Shooting>().animator.speed = GetComponent<Shooting>().towerStats.frequency;
            }
            if (towerUpgrades.pierce[upgradeIndex[path]] != 0)
            {
                GetComponent<Shooting>().towerStats.pierce = Mathf.FloorToInt(GetComponent<Shooting>().towerStats.pierce * (towerUpgrades.pierce[upgradeIndex[path]] + 1));
            }
            if (towerUpgrades.speed[upgradeIndex[path]] != 0)
            {
                GetComponent<Shooting>().towerStats.speed *= (towerUpgrades.speed[upgradeIndex[path]] + 1);
            }
            if (towerUpgrades.lifetime[upgradeIndex[path]] != 0)
            {
                GetComponent<Shooting>().towerStats.lifetime *= (towerUpgrades.lifetime[upgradeIndex[path]] + 1);
            }
            if (towerUpgrades.numOfShots[upgradeIndex[path]] != 0)
            {
                GetComponent<Shooting>().towerStats.numOfShots += (towerUpgrades.numOfShots[upgradeIndex[path]]);
            }
            if (towerUpgrades.shotAngle[upgradeIndex[path]] != 0)
            {
                GetComponent<Shooting>().towerStats.shotAngle += (towerUpgrades.shotAngle[upgradeIndex[path]]);
            }
            if (towerUpgrades.rotationSpeed[upgradeIndex[path]] != 0)
            {
                if(Mathf.Abs(towerUpgrades.rotationSpeed[upgradeIndex[path]]) >= 1000)
                {
                    GetComponent<Shooting>().towerStats.rotationSpeed += (towerUpgrades.rotationSpeed[upgradeIndex[path]] / 1000);
                }
                else
                {
                    GetComponent<Shooting>().towerStats.rotationSpeed *= (towerUpgrades.rotationSpeed[upgradeIndex[path]] + 1);
                }
            }
            if (towerUpgrades.slowAmount[upgradeIndex[path]] != 0)
            {
                GetComponent<Shooting>().towerStats.slowAmount *= (towerUpgrades.slowAmount[upgradeIndex[path]] + 1);
            }

            upgradeIndex[path]++;

            changeButtonStuff();

        }
        
    }

}
