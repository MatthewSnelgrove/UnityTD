using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    public float[] damages;
    public int[] costs;
    public bool[] slows;
    public float[] ranges;
    public int[] pierces;
    public bool[] heatSeekings;
    public float[] frequencies;
    public Sprite[] towerSprites;
    public float[] lifetimes;
    public float[] speeds;
    public Sprite[] bulletSprites;
    public float[] rotationSpeeds;
    public int[] numsOfShots;
    public float[] shotAngles;
    public float[] sizes;
    public float[] colliderLengths;
    public float[] colliderHeights;
    public float[] colliderOffsetsX;
    public float[] colliderOffsetsY;
    public int[] ids;
    

    public float damage;
    public int cost;
    public bool slow;
    public float range;
    public int pierce;
    public bool heatSeeking;
    public float frequency;
    public SpriteRenderer sr;
    public Sprite towerSprite;
    public float lifetime;
    public float speed;
    public Sprite bulletSprite;
    public float rotationSpeed;
    public int numOfShots;
    public float shotAngle;
    public float size;
    public float colliderLength;
    public float colliderHeight;
    public float colliderOffsetX;
    public float colliderOffsetY;
    public float damageDone;
    public int id;
    public GameObject rangeDisplay;

    public GameController gameCon;


    void Start()
    {
        gameObject.transform.localScale= new Vector2(size, size);
        gameObject.GetComponent<BoxCollider2D>().size = new Vector2(colliderLength,colliderHeight);
        gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(colliderOffsetX, colliderOffsetY);

    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            selectTower();
            GameObject.Find("GameController").GetComponent<GameController>().overTower = true;
        }
    }

    void OnMouseExit()
    {
        GameObject.Find("GameController").GetComponent<GameController>().overTower = false;
    }
    void selectTower()
    {
        if (GameObject.Find("GameController").GetComponent<GameController>().activePhantomTower == -1 && Input.GetMouseButtonDown(0))
        {
            GameObject[] rangeDisplays = GameObject.FindGameObjectsWithTag("RangeDisplay");
            if (rangeDisplays.Length > 1)
            {
                Destroy(rangeDisplays[0]);
            }

        }
        var rangeDisplayVar = Instantiate(rangeDisplay, gameObject.transform.position, Quaternion.identity);
        rangeDisplayVar.transform.localScale = new Vector2(range, range);

        if (GameObject.Find("GameController").GetComponent<GameController>().activePhantomTower == -1 && Input.GetMouseButtonDown(0))
        {
            GameObject[] rangeDisplays = GameObject.FindGameObjectsWithTag("RangeDisplay");
            if (rangeDisplays.Length > 1)
            {
                Destroy(rangeDisplays[0]);
            }

        }

        //open upgrade menu

    }
}
