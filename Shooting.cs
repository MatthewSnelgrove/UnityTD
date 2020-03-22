using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bullet;
    float shotDelay;
    float animDelay;
    public GameObject target;
    public bool active;
    public GameObject farthestEnemy;
    float rotation;
    Quaternion targetRotationQuaternion;
    float targetRotation;
    public TowerStatsSO baseTowerStats;
    public TowerStatsSO towerStats;
    public Animator animator;
    public AnimatorOverrideController baseAOC;
    public AnimatorOverrideController AOC;
    public Vector3 lastPos;
    



    /*state 0 = first
     * state 1 = last
     * state 2 = strong
     * state 3 = close
     * */

    // Start is called before the first frame update
    void Start()
    {
        active = false;
        towerStats = Instantiate(baseTowerStats);

        AOC = Instantiate(baseAOC);
        AOC["Tower1BuildAnim"] = towerStats.build;
        AOC["Tower1IdleAnim"] = towerStats.idle;
        AOC["Tower1ShootAnim"] = towerStats.shoot;
        animator.runtimeAnimatorController = AOC;
        


    }

    void doneBuild()
    {
        animator.speed = towerStats.frequency;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        target = FindTarget();
        if (target != null) 
        {
            animator.SetBool("isShooting", true);
            lastPos = target.transform.position;
        }
           
        else
        {
            animator.SetBool("isShooting", false);
        }
    }


   GameObject FindTarget()
   {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        farthestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            
            if (Vector2.Distance(transform.position, enemy.transform.position) <= towerStats.range && (farthestEnemy == null || enemy.GetComponent<EnemyScript>().distanceTraveled > farthestEnemy.GetComponent<EnemyScript>().distanceTraveled))
            {
                farthestEnemy = enemy;
            }
        }
        return farthestEnemy;
   }

    
    public void Shoot()
    {
        Vector3 targetPos;

        if (target == null)
        {
            target = FindTarget();
        }
        if (target == null)
        {
            targetPos = lastPos;
        }
        else
        {
            targetPos = target.transform.position;
        }

        targetRotationQuaternion = Quaternion.LookRotation(Vector3.forward, targetPos - gameObject.transform.position);
        targetRotation = targetRotationQuaternion.eulerAngles.z;
        
        if (towerStats.numOfShots == 1)
        {
            rotation = targetRotation;
            spawnBullet(rotation);
        }

        if (towerStats.numOfShots > 1)
        {
            if (towerStats.numOfShots % 2 == 0)
            {
                for (int i = -towerStats.numOfShots / 2; i < towerStats.numOfShots / 2 + 1; i++)
                {
                    if (i < 0)
                    {
                        rotation = targetRotation + (i * towerStats.shotAngle + towerStats.shotAngle / 2);
                        spawnBullet(rotation);
                    }
                    if (i > 0)
                    {
                        rotation = targetRotation + (i * towerStats.shotAngle - towerStats.shotAngle / 2);
                        spawnBullet(rotation);
                    }

                    
                }
            }
            else
            {
                for (int i = (towerStats.numOfShots - 1) / -2; i < (towerStats.numOfShots - 1) / 2 + 1; i++)
                {

                    rotation = targetRotation + (i * towerStats.shotAngle);

                    spawnBullet(rotation);
                }
            }
        }
        
    }
    
    void spawnBullet(float rotation)
    {
        var Bul = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, rotation));
        Bul.GetComponent<BulletScript>().bulletSpeed = towerStats.speed;
        Bul.GetComponent<BulletScript>().bulletDamage = towerStats.damage;
        Bul.GetComponent<BulletScript>().bulletPierce = towerStats.pierce;
        Bul.GetComponent<BulletScript>().slowAmount = towerStats.slowAmount;
        Bul.GetComponent<BulletScript>().rotationSpeed = towerStats.rotationSpeed;
        Bul.GetComponent<SpriteRenderer>().sprite = towerStats.bulletSprite;
        Bul.GetComponent<BulletScript>().timeLeft = towerStats.lifetime;
        Bul.GetComponent<BulletScript>().target = target;
        Bul.GetComponent<BulletScript>().firstTarget = target;
        Bul.GetComponent<BulletScript>().towerParent = gameObject;
        
    }
}
