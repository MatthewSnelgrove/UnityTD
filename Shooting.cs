using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bullet;
    float shotDelay;
    public GameObject target;
    public GameObject farthestEnemy;
    float rotation;
    Quaternion targetRotationQuaternion;
    float targetRotation;
    public int towerType;
    



    /*state 0 = first
     * state 1 = last
     * state 2 = strong
     * state 3 = close
     * */

    // Start is called before the first frame update
    void Start()
    {
        towerType=GetComponent<TowerScript>().id;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        target = FindTarget();

        shotDelay -= Time.deltaTime;
        if (target != null)
        {
            Vector3 targetPos = target.transform.position;
            targetRotationQuaternion = Quaternion.LookRotation(Vector3.forward, targetPos - gameObject.transform.position);
            targetRotation = targetRotationQuaternion.eulerAngles.z;

            if (shotDelay < 0)
            {
                shotDelay = 1 / GetComponent<TowerScript>().frequency;
                Shoot(target);
                GetComponent<SpriteRenderer>().sprite = GetComponent<TowerScript>().towerSprites[towerType * 3 - 2];
            }
        } else
        {
            GetComponent<SpriteRenderer>().sprite = GetComponent<TowerScript>().towerSprites[towerType * 3 - 3];
        }
    }

   GameObject FindTarget()
   {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        farthestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            if (Vector2.Distance(transform.position, enemy.transform.position) <= GetComponent<TowerScript>().range && (farthestEnemy == null || enemy.GetComponent<EnemyScript>().distanceTraveled > farthestEnemy.GetComponent<EnemyScript>().distanceTraveled))
            {
                farthestEnemy = enemy;
            }
        }
        return farthestEnemy;
   }

    void Shoot(GameObject target)
    {

        if (gameObject.GetComponent<TowerScript>().numOfShots == 1)
        {
            rotation = targetRotation;
            spawnBullet();
        }

        if (gameObject.GetComponent<TowerScript>().numOfShots > 1)
        {
            if (gameObject.GetComponent<TowerScript>().numOfShots % 2 == 0)
            {
                for (int i = -gameObject.GetComponent<TowerScript>().numOfShots / 2; i < gameObject.GetComponent<TowerScript>().numOfShots / 2 + 1; i++)
                {
                    if (i < 0)
                    {
                        rotation = targetRotation + (i * gameObject.GetComponent<TowerScript>().shotAngle + gameObject.GetComponent<TowerScript>().shotAngle / 2);
                        spawnBullet();
                    }
                    if (i > 0)
                    {
                        rotation = targetRotation + (i * gameObject.GetComponent<TowerScript>().shotAngle - gameObject.GetComponent<TowerScript>().shotAngle / 2);
                        spawnBullet();
                    }

                    
                }
            }
            if (gameObject.GetComponent<TowerScript>().numOfShots % 2 == 1 && gameObject.GetComponent<TowerScript>().numOfShots != 1)
            {


                for (int i = (gameObject.GetComponent<TowerScript>().numOfShots - 1) / -2; i < (gameObject.GetComponent<TowerScript>().numOfShots - 1) / 2 + 1; i++)
                {

                    rotation = targetRotation + (i * gameObject.GetComponent<TowerScript>().shotAngle);

                    spawnBullet();
                }
            }
        }
    }

    void spawnBullet()
    {
        var Bul = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, rotation));
        Bul.GetComponent<BulletScript>().heatSeeking = GetComponent<TowerScript>().heatSeeking;
        Bul.GetComponent<BulletScript>().bulletSpeed = GetComponent<TowerScript>().speed;
        Bul.GetComponent<BulletScript>().bulletDamage = GetComponent<TowerScript>().damage;
        Bul.GetComponent<BulletScript>().bulletPierce = GetComponent<TowerScript>().pierce;
        Bul.GetComponent<BulletScript>().heatSeeking = GetComponent<TowerScript>().heatSeeking;
        Bul.GetComponent<SpriteRenderer>().sprite = GetComponent<TowerScript>().bulletSprite;
        Bul.GetComponent<BulletScript>().slow = GetComponent<TowerScript>().slow;
        Destroy(Bul.gameObject, GetComponent<TowerScript>().lifetime);
        Bul.GetComponent<BulletScript>().target = target;
        Bul.GetComponent<BulletScript>().firstTarget = target;
        Bul.GetComponent<BulletScript>().towerParent = gameObject;
        Bul.GetComponent<BulletScript>().rotationSpeed = GetComponent<TowerScript>().rotationSpeed;
    }
}
