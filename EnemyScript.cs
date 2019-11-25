using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    public float[] healths;
    public int[] damages;
    public float[] speeds;
    public int[] values;
    public Sprite[] spriteList;
    public float currentSpeed;
    public float timer = 0f;

    public int moveState;
    public GameObject goal;
    private Vector3 goalPosition;
    private float offsetX;
    private float offsetY;
    public GameController gameCon;
    public float distanceTraveled;

    public float health;
    public int damage;
    public float speed;
    public int value;
    public SpriteRenderer sr;






    // Start is called before the first frame update
    void Start()
    {
        gameCon = GameObject.Find("GameController").GetComponent<GameController>();
        moveState = 0;
        goal = gameCon.turns[0];
        offsetX = ((Random.value - 0.5f) / 2.5f*25);
        offsetY = ((Random.value - 0.5f) / 2.5f * 25);
        transform.position = new Vector2(goal.transform.position.x + offsetX, goal.transform.position.y + offsetY);
        sr = GetComponent<SpriteRenderer>();
        sr.sortingOrder = 2;
        currentSpeed = speed;
        timer = 0.5f;

    }

    //      (enemy.x-tower.x)/mathf.abs(enemy.x-tower.x+enemy.y-tower.y)

    // Update is called once per frame
    void FixedUpdate()
    {
        if (moveState == 20)
        {
            Destroy(gameObject);
            GameObject gameCon = GameObject.Find("GameController");
            gameCon.GetComponent<GameController>().lives -= damage;
        }
        goalPosition = new Vector2(goal.transform.position.x + offsetX, goal.transform.position.y + offsetY);

        transform.position = Vector2.MoveTowards(transform.position, goalPosition, currentSpeed);

        if (transform.position == goalPosition)
        {
            moveState += 1;
            goal = gameCon.turns[moveState];
        }
        if (health < 1)
        {

            Destroy(gameObject);
            GameObject gameCon = GameObject.Find("GameController");
            gameCon.GetComponent<GameController>().money += value;
        }
        distanceTraveled += currentSpeed * Time.deltaTime;

        if (currentSpeed < speed)
        {
            currentSpeed *= 1.02f;
        }
        if (currentSpeed > speed)
        {
            currentSpeed = speed;
        }

        timer += Time.deltaTime;
        if (timer*currentSpeed > 0.75f)
        {
            for (int i = 0; i < 10; i++)
            {
                if (GetComponent<SpriteRenderer>().sprite == spriteList[i])
                {
                    GetComponent<SpriteRenderer>().sprite = spriteList[i + 10];
                    
                    timer = 0f;
                }
            }
            if (timer*currentSpeed > 0.75f)
            {
                for (int i = 10; i < 20; i++)
                {
                    if (GetComponent<SpriteRenderer>().sprite == spriteList[i])
                    {
                        GetComponent<SpriteRenderer>().sprite = spriteList[i - 10];
                        
                        timer = 0f;
                    }
                }
            }



        }

    }
}