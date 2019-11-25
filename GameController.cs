using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject[] turns;
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
    public bool overTower = false;

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
        spawnDeltaTime = Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Time.timeScale += 1;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Time.timeScale /= 2;
        }


        //Debug.Log(1 / Time.deltaTime);
        GameObject[] remainingEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        Debug.Log(remainingEnemies.Length);
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
            Destroy(GameObject.FindGameObjectsWithTag("PhantomTower")[0]);
            activePhantomTower = -1;
        }

        /*
        if (Input.GetKey(KeyCode.Return))
        { if (SBSpawnMode < 4)
            {
                SBSpawnMode += 1;
            }
            else
            {
                SBSpawnMode = 0;
            }

            if (SBSpawnMode == 1)
            {
                field.SetActive(true);
            }
        }
        
            int SBEnemyType;
            int SBEnemyCount;
            float SBEnemySpacing;
            field.SetActive(true);

            int SBEnemyType;
            int SBEnemyCount;
            float SBEnemySpacing;




            StartCoroutine(spawnEnemies(0, 5, 1f));
            */





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
                for (int i = 0; i < remainingBullets.Length; i++)
                {
                    Destroy(remainingBullets[i]);
                }

            }
        }

        if (activePhantomTower > -1 && Input.GetMouseButtonDown(0))
        {
            towerSpawn(activePhantomTower);
        }

        if (activePhantomTower == -1 && Input.GetMouseButtonDown(0) && overTower == false)
        {
            GameObject[] rangeDisplays = GameObject.FindGameObjectsWithTag("RangeDisplay");
            foreach (GameObject i in rangeDisplays)
            {
                Destroy(i);
            }

        }
        moneyText.text = money.ToString();
        livesText.text = lives.ToString();

    }



    public void phantomTowerSpawn(int towerType)
    {
        if (money >= tower.GetComponent<TowerScript>().costs[towerType]) {
            GameObject[] existingPhantomTower = GameObject.FindGameObjectsWithTag("PhantomTower");
            for (int i = 0; i < existingPhantomTower.Length; i++)
            {
                Destroy(existingPhantomTower[i]);
            }

            GameObject[] rangeDisplays = GameObject.FindGameObjectsWithTag("RangeDisplay");
            foreach (GameObject i in rangeDisplays)
            {
                Destroy(i);
            }

            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos = new Vector2(mousePos.x, mousePos.y);
            var PhantomTower = Instantiate(phantomTower, mousePos, Quaternion.identity);
            PhantomTower.GetComponent<SpriteRenderer>().sprite = tower.GetComponent<TowerScript>().towerSprites[towerType];
            PhantomTower.GetComponent<PhantomTowerScript>().id = towerType;
            PhantomTower.GetComponent<PhantomTowerScript>().range = tower.GetComponent<TowerScript>().ranges[towerType];
            PhantomTower.GetComponent<PhantomTowerScript>().size = tower.GetComponent<TowerScript>().sizes[towerType];
            PhantomTower.GetComponent<PhantomTowerScript>().size = tower.GetComponent<TowerScript>().sizes[towerType];
            PhantomTower.GetComponent<PhantomTowerScript>().colliderLength = tower.GetComponent<TowerScript>().colliderLengths[towerType];
            PhantomTower.GetComponent<PhantomTowerScript>().colliderHeight = tower.GetComponent<TowerScript>().colliderHeights[towerType];
            PhantomTower.GetComponent<PhantomTowerScript>().colliderOffsetX = tower.GetComponent<TowerScript>().colliderOffsetsX[towerType];
            PhantomTower.GetComponent<PhantomTowerScript>().colliderOffsetY = tower.GetComponent<TowerScript>().colliderOffsetsY[towerType];
            activePhantomTower = towerType;
            placeable = false;
        }
    }


    void towerSpawn(int towerType)
    {
        GameObject mapCol = GameObject.Find("Map1");
        

        if (placeable==true)
        {
            money -= tower.GetComponent<TowerScript>().costs[towerType];
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos = new Vector2(mousePos.x, mousePos.y);
            var Tower = Instantiate(tower, mousePos, Quaternion.identity);
            Tower.GetComponent<TowerScript>().damage = Tower.GetComponent<TowerScript>().damages[towerType];
            Tower.GetComponent<TowerScript>().cost = Tower.GetComponent<TowerScript>().costs[towerType];
            Tower.GetComponent<TowerScript>().slow = Tower.GetComponent<TowerScript>().slows[towerType];
            Tower.GetComponent<TowerScript>().range = Tower.GetComponent<TowerScript>().ranges[towerType];
            Tower.GetComponent<TowerScript>().pierce = Tower.GetComponent<TowerScript>().pierces[towerType];
            Tower.GetComponent<TowerScript>().heatSeeking = Tower.GetComponent<TowerScript>().heatSeekings[towerType];
            Tower.GetComponent<TowerScript>().frequency = Tower.GetComponent<TowerScript>().frequencies[towerType];
            Tower.GetComponent<TowerScript>().lifetime = Tower.GetComponent<TowerScript>().lifetimes[towerType];
            Tower.GetComponent<TowerScript>().speed = Tower.GetComponent<TowerScript>().speeds[towerType];
            Tower.GetComponent<TowerScript>().bulletSprite = Tower.GetComponent<TowerScript>().bulletSprites[towerType];
            Tower.GetComponent<SpriteRenderer>().sprite = Tower.GetComponent<TowerScript>().towerSprites[towerType];
            Tower.GetComponent<TowerScript>().numOfShots = Tower.GetComponent<TowerScript>().numsOfShots[towerType];
            Tower.GetComponent<TowerScript>().shotAngle = Tower.GetComponent<TowerScript>().shotAngles[towerType];
            Tower.GetComponent<TowerScript>().rotationSpeed = Tower.GetComponent<TowerScript>().rotationSpeeds[towerType];
            Tower.GetComponent<TowerScript>().size = Tower.GetComponent<TowerScript>().sizes[towerType];
            Tower.GetComponent<TowerScript>().size = Tower.GetComponent<TowerScript>().sizes[towerType];
            Tower.GetComponent<TowerScript>().colliderLength = Tower.GetComponent<TowerScript>().colliderLengths[towerType];
            Tower.GetComponent<TowerScript>().colliderHeight = Tower.GetComponent<TowerScript>().colliderHeights[towerType];
            Tower.GetComponent<TowerScript>().colliderOffsetX = Tower.GetComponent<TowerScript>().colliderOffsetsX[towerType];
            Tower.GetComponent<TowerScript>().colliderOffsetY = Tower.GetComponent<TowerScript>().colliderOffsetsY[towerType];
            Tower.GetComponent<TowerScript>().id = Tower.GetComponent<TowerScript>().ids[towerType];
            activePhantomTower = -1;
        }
    }

}
   


