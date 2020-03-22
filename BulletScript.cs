using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    


    public float bulletSpeed;
    public float bulletDamage;
    public int bulletPierce;
    public float slowAmount;
    public float rotationSpeed;

    public float timeLeft;

    public GameObject target;
    public GameObject towerParent;
    public GameObject firstTarget;
    



    public List<GameObject> hitEnemies;


    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up * bulletSpeed;
    }


    private void OnTriggerEnter2D(Collider2D enemy)
    {

        if(enemy.gameObject.CompareTag("Enemy") && bulletPierce > -1 &&! hitEnemies.Contains(enemy.gameObject))
        {
            enemy.GetComponent<EnemyScript>().health -= bulletDamage;
            hitEnemies.Add(enemy.gameObject);
            bulletPierce -= 1;
            towerParent.GetComponent<TowerScript>().damageDone += bulletDamage;

            if (slowAmount != 0)
            {
                if (enemy.GetComponent<EnemyScript>().currentSpeed > enemy.GetComponent<EnemyScript>().speed / 3)
                {
                    enemy.GetComponent<EnemyScript>().currentSpeed *= (1 / Mathf.Pow(1.08f, slowAmount));
                }
                else
                {
                    enemy.GetComponent<EnemyScript>().currentSpeed = enemy.GetComponent<EnemyScript>().speed / 3;
                }
            }
            

            if (bulletPierce < 0)
            {
                Destroy(gameObject);
            }
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //Set target to closest enemy every frame
        /* if (heatSeeking == true && (firstTarget==null || hitEnemies.Contains(firstTarget)))
        {
            target=heatSeekingNewTarget();
        }
        */
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            Destroy(gameObject);
        }

        //Set target to closest enemy only after current target is hit or killed
        if (rotationSpeed != 0 && (target == null || hitEnemies.Contains(target)))
        {
            target = heatSeekingNewTarget();
        }

        //Add upgrade that allows seeking targeted seeking - first, last, strong, close


        if (rotationSpeed != 0 && target != null)
        {
            heatSeekingRotate();
            GetComponent<Rigidbody2D>().velocity = transform.up * bulletSpeed;
        }

    }

    GameObject heatSeekingNewTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject enemy in enemies)
        {
            if (hitEnemies.Contains(enemy) == false)
            {
                Vector3 directionToTarget = enemy.transform.position - position;
                float dSqrToTarget = directionToTarget.sqrMagnitude;
                if (dSqrToTarget < closestDistanceSqr)
                {
                    closestDistanceSqr = dSqrToTarget;
                    closestEnemy = enemy;
                }
            }
        }  
        return closestEnemy;
    }

    void heatSeekingRotate()
    {
        Vector3 targetPos = target.transform.position;


        Quaternion goalQuaternion = Quaternion.LookRotation(Vector3.forward, targetPos - transform.position);
        float goalRotation = goalQuaternion.eulerAngles.z;
        float currentRotation = transform.rotation.eulerAngles.z;


        float degreeRotation = 0;
        if (Mathf.Abs(goalRotation - currentRotation) > rotationSpeed)
        {
            if (goalRotation >= currentRotation && Mathf.Abs(goalRotation - currentRotation) < 180)
            {
                degreeRotation = rotationSpeed;
            }
            if (goalRotation <= currentRotation && Mathf.Abs(goalRotation - currentRotation) < 180)
            {
                degreeRotation = -rotationSpeed;
            }
            if (goalRotation <= currentRotation && Mathf.Abs(goalRotation - currentRotation) >= 180)
            {
                degreeRotation = rotationSpeed;
            }
            if (goalRotation >= currentRotation && Mathf.Abs(goalRotation - currentRotation) >= 180)
            {
                degreeRotation = -rotationSpeed;
            }

        }
        else
        {
            degreeRotation = goalRotation - currentRotation;
        }
        transform.rotation = Quaternion.Euler(0, 0, currentRotation + degreeRotation);
    }

}

