using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject[] turns;
    public TowerStatsSO[] towerStats;
    public TowerUpgradesSO[] towerUpgrades;
    public float sellPercentage = 0.67f;
    public GameObject enemy;
    public GameObject tower;
    public GameObject phantomTower;
    public int wave = 0;
    public bool waveActive=false;
    public bool allEnemiesSent = true;
    public Text moneyText;
    public Text livesText;
    public bool placeable = true;
    public GameObject SBSpawnInputField;
    public int SBSpawnMode=0;
    public float spawnDeltaTime;
    public int activePhantomTower = -1;
    public GameObject upgradeMenu;
    public GameObject selectedTower;

    public int money;
    public int lives;

    // Start is called before the first frame update
    void Start()
    {
        placeable = true;
        Time.timeScale = 2;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }
    void Update()
    {
        
        /*Vector3 upgradeMenuV = upgradeMenu.transform.position;
        upgradeMenuV.y++;
        upgradeMenu.transform.position = upgradeMenuV;*/
        if (Application.isFocused == false)
        {
            activePhantomTower = -1;
        }

        spawnDeltaTime = Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Time.timeScale += 1;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Time.timeScale /= 2;
        }

        GameObject[] remainingEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (allEnemiesSent == true)
        {
            if (remainingEnemies.Length == 0)
            {
                waveActive = false;
            }
        }

        if (Input.GetKey(KeyCode.Q))
        {
            gameObject.GetComponent<SpawningScript>().spawn(0);
        }
        if (Input.GetKey(KeyCode.W))
        {
            gameObject.GetComponent<SpawningScript>().spawn(1);
        }
        if (Input.GetKey(KeyCode.E))
        {
            gameObject.GetComponent<SpawningScript>().spawn(2);
        }
        if (Input.GetKey(KeyCode.R))
        {
            gameObject.GetComponent<SpawningScript>().spawn(3);
        }
        if (Input.GetKey(KeyCode.T))
        {
            gameObject.GetComponent<SpawningScript>().spawn(4);
        }
        if (Input.GetKey(KeyCode.Y))
        {
            gameObject.GetComponent<SpawningScript>().spawn(5);
        }
        if (Input.GetKey(KeyCode.U))
        {
            gameObject.GetComponent<SpawningScript>().spawn(6);
        }
        if (Input.GetKey(KeyCode.I))
        {
            gameObject.GetComponent<SpawningScript>().spawn(7);
        }
        if (Input.GetKey(KeyCode.O))
        {
            gameObject.GetComponent<SpawningScript>().spawn(8);
        }
        if (Input.GetKey(KeyCode.P))
        {
            gameObject.GetComponent<SpawningScript>().spawn(9);
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            if(activePhantomTower != -1)
            {
                Destroy(GameObject.FindGameObjectsWithTag("PhantomTower")[0]);
                activePhantomTower = -1;
            }
            
        }

       


        /*if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.A))
        {
            if (placeable == true)
            {
                towerSpawn(0);
            } 
        }*/

        if (Input.GetKeyDown(KeyCode.A))
        {
            phantomTowerSpawn(0);
        }

        if (Input.GetKey(KeyCode.S))
        {
            phantomTowerSpawn(1);
        }
        if (Input.GetKey(KeyCode.D))
        {
            phantomTowerSpawn(2);
        }
        if (Input.GetKey(KeyCode.F))
        {
            phantomTowerSpawn(3);
        }
        if (Input.GetKey(KeyCode.G))
        {
            phantomTowerSpawn(4);
        }
        if (Input.GetKey(KeyCode.H))
        {
            phantomTowerSpawn(5);
        }
        if (Input.GetKey(KeyCode.J))
        {
            phantomTowerSpawn(6);
        }
        if (Input.GetKey(KeyCode.K))
        {
            phantomTowerSpawn(7);
        }
        if (Input.GetKey(KeyCode.L))
        {
            phantomTowerSpawn(8);
        }
        if (Input.GetKey(KeyCode.Semicolon))
        {
            phantomTowerSpawn(9);
        }
        if (Input.GetMouseButtonDown(1))
        {
            SceneManager.LoadScene("SampleScene");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (waveActive == false)
            {
                wave++;
                allEnemiesSent = false;
                gameObject.GetComponent<SpawningScript>().nextWave(wave);
                GameObject[] remainingBullets = GameObject.FindGameObjectsWithTag("Bullet");
                foreach(GameObject i in remainingBullets)
                {
                    Destroy(i);
                }

            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPoint.z = Camera.main.transform.position.z;
            Ray ray = new Ray(worldPoint, new Vector3(0, 0, 1));
            RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(ray);
            bool[] colType = new bool[3];
            for(int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.CompareTag("PhantomTower"))
                {
                    colType[0] = true;
                }
                else if (hits[i].collider.CompareTag("Tower"))
                {
                    selectedTower = hits[i].collider.gameObject;
                    colType[1] = true;
                }
                else if (hits[i].collider.CompareTag("Map"))
                {
                    colType[2] = true;
                }
            }

            //clicked phantom tower
            if (colType[0] == true)
            {
                towerSpawn(activePhantomTower);
            }
            //clicked tower
            else if(colType[1] == true)
            {
                selectedTower.SendMessage("selectTower");
            }
            //clicked map
            else if(colType[2] == true)
            {
                GameObject[] rangeDisplays = GameObject.FindGameObjectsWithTag("RangeDisplay");
                foreach (GameObject i in rangeDisplays)
                {
                    Destroy(i);
                }

                Vector3 upgradeMenuV = upgradeMenu.transform.position;
                upgradeMenuV.y = -96;
                upgradeMenu.transform.position = upgradeMenuV;
                selectedTower = null;

            }
        }
        moneyText.text = money.ToString();
        livesText.text = lives.ToString();

    }

    public void phantomTowerSpawn(int towerType)
    {
        if (money >= towerStats[towerType].cost) {
            GameObject[] existingPhantomTower = GameObject.FindGameObjectsWithTag("PhantomTower");
            foreach (GameObject i in existingPhantomTower)
            {
                Destroy(i);
            }

            GameObject[] rangeDisplays = GameObject.FindGameObjectsWithTag("RangeDisplay");
            foreach (GameObject i in rangeDisplays)
            {
                Destroy(i);
            }

            Vector3 upgradeMenuV = upgradeMenu.transform.position;
            upgradeMenuV.y = -96;
            upgradeMenu.transform.position = upgradeMenuV;

            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos = new Vector2(mousePos.x, mousePos.y);
            var PhantomTower = Instantiate(phantomTower, mousePos, Quaternion.identity);
            PhantomTower.GetComponent<PhantomTowerScript>().towerStats = GetComponent<GameController>().towerStats[towerType];
            activePhantomTower = towerType;
            placeable = false;
        }
    }


    public void towerSpawn(int towerType)
    {
        GameObject mapCol = GameObject.Find("Map1");

        if (placeable == true)
        {
            money -= towerStats[towerType].cost;
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos = new Vector2(mousePos.x, mousePos.y);
            var Tower = Instantiate(tower, mousePos, Quaternion.identity);
            Tower.GetComponent<Shooting>().baseTowerStats = towerStats[towerType];
            Tower.GetComponent<TowerScript>().towerUpgrades = towerUpgrades[towerType];
            activePhantomTower = -1;
        }
    }

}
   


