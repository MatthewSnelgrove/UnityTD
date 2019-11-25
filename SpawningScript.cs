using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningScript : MonoBehaviour
{
    float spawnDeltaTime;
    public GameObject enemy;
    void Update()
    {
        spawnDeltaTime = Time.deltaTime;
    }
    void FixedUpdate()
    {

    }

    public void spawn(int enemyType)
    {
        var Enemy = Instantiate(enemy, gameObject.GetComponent<GameController>().turns[0].transform.position, Quaternion.identity);
        Enemy.GetComponent<EnemyScript>().health = Enemy.GetComponent<EnemyScript>().healths[enemyType];
        Enemy.GetComponent<EnemyScript>().damage = Enemy.GetComponent<EnemyScript>().damages[enemyType];
        Enemy.GetComponent<EnemyScript>().speed = Enemy.GetComponent<EnemyScript>().speeds[enemyType];
        Enemy.GetComponent<EnemyScript>().value = Enemy.GetComponent<EnemyScript>().values[enemyType];
        Enemy.GetComponent<SpriteRenderer>().sprite = Enemy.GetComponent<EnemyScript>().spriteList[enemyType];
    }

    IEnumerator spawnEnemies(int spawnEnemyType, int spawnCount, float spawnSpacing)
    {
        float privSpawnDeltaTime;
        float partialEnemy = 0;
        int numSpawned = 0;

        while (numSpawned < spawnCount)
        {
            privSpawnDeltaTime = spawnDeltaTime;

            if (spawnSpacing >= privSpawnDeltaTime)
            {
                if (numSpawned < spawnCount)
                {
                    spawn(spawnEnemyType);
                    numSpawned++;
                }
            }
            else if (spawnSpacing > 0)
            {
                for (int i = 0; i < Mathf.Floor(privSpawnDeltaTime / spawnSpacing); i++)
                {
                    if (numSpawned < spawnCount)
                    {
                        spawn(spawnEnemyType);
                        numSpawned++;
                    }
                }
                partialEnemy += (privSpawnDeltaTime / spawnSpacing) - Mathf.Floor(privSpawnDeltaTime / spawnSpacing);
                if (partialEnemy >= 1 && numSpawned < spawnCount)
                {
                    spawn(spawnEnemyType);
                    partialEnemy -= 1;
                    numSpawned++;
                }
            }
            else
            {
                for (int i = 0; i < spawnCount; i++)
                {
                    if (numSpawned < spawnCount)
                    {
                        spawn(spawnEnemyType);
                        numSpawned++;
                    }
                }
            }
            yield return new WaitForSeconds(spawnSpacing);
        }
    }


    public void nextWave(int wave)
    {
        if (wave == 1)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 5, 1f));
                yield return new WaitForSeconds(5);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }

        if (wave == 2)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 10, 1f));
                yield return new WaitForSeconds(10);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 3)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 10, 0.5f));
                yield return new WaitForSeconds(5);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 4)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 20, 0.2f));
                yield return new WaitForSeconds(4);
                StartCoroutine(spawnEnemies(1, 5, 1f));
                yield return new WaitForSeconds(5);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 5)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 10, 0.1f));
                yield return new WaitForSeconds(1);
                StartCoroutine(spawnEnemies(1, 10, 0.5f));
                yield return new WaitForSeconds(5);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 6)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(1, 10, 0.5f));
                yield return new WaitForSeconds(5);
                StartCoroutine(spawnEnemies(2, 1, 0));
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 7)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(2, 5, 1f));
                yield return new WaitForSeconds(5);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 8)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(2, 5, 1f));
                yield return new WaitForSeconds(5);
                StartCoroutine(spawnEnemies(3, 5, 0.5f));
                yield return new WaitForSeconds(2.5f);
                StartCoroutine(spawnEnemies(2, 3, 1f));
                StartCoroutine(spawnEnemies(3, 6, 0.5f));
                yield return new WaitForSeconds(3);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 9)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(3, 20, 0.2f));
                yield return new WaitForSeconds(4);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 10)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(4, 25, 1));
                yield return new WaitForSeconds(25);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 11)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 40, 0.5f));
                yield return new WaitForSeconds(10);
                StartCoroutine(spawnEnemies(1, 10, 1));
                StartCoroutine(spawnEnemies(3, 5, 2));
                yield return new WaitForSeconds(10);
                StartCoroutine(spawnEnemies(0, 400, 0.05f));
                yield return new WaitForSeconds(10);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 12)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(5, 50, 0.05f));
                yield return new WaitForSeconds(2.5f);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 13)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(2, 500, 0.1f));
                yield return new WaitForSeconds(50);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 14)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 15)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 16)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 17)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 18)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 19)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 20)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 21)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 22)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 23)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 24)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 25)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 26)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 27)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 28)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 29)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 30)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 31)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 32)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 33)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 34)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 35)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 36)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 37)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 38)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 39)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 40)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 41)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 42)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 43)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 44)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 45)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 46)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 47)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 48)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 49)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 50)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 51)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 52)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 53)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 54)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 55)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 56)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 57)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 58)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 59)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 60)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 61)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 62)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 63)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 64)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 65)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 66)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 67)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 68)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 69)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 70)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 71)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 72)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 73)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 74)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 75)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 76)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 77)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 78)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 79)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 80)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 81)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 82)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 83)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 84)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 85)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 86)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 87)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 88)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 89)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 90)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 91)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 92)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 93)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 94)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 95)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 96)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 97)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 98)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 99)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }
        if (wave == 100)
        {
            gameObject.GetComponent<GameController>().waveActive = true;
            IEnumerator sendWave()
            {
                StartCoroutine(spawnEnemies(0, 0, 0f));
                yield return new WaitForSeconds(0);
                gameObject.GetComponent<GameController>().allEnemiesSent = true;
            }
            StartCoroutine(sendWave());
        }




    }
}
