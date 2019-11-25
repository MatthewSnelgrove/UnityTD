using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhantomTowerScript : MonoBehaviour
{
    public GameObject towerScriptHolder;
    public SpriteRenderer sr;
    public Sprite towerSprite;
    public float size;
    public float colliderLength;
    public float colliderHeight;
    public float colliderOffsetX;
    public float colliderOffsetY;
    public float damageDone;
    public GameController gameCon;
    public GameObject rangeDisplayPrefab;
    public int id;
    public float range;
    public Sprite[] rangeSprites;

    public bool canPlace = true;
    // Start is called before the first frame update
    void Start()
    {
        sr.sortingOrder = 2;
        canPlace = true;
        gameCon = GameObject.Find("GameController").GetComponent<GameController>();
        gameObject.transform.localScale = new Vector2(size, size);
        var rangeDisplayVar = Instantiate(rangeDisplayPrefab, gameObject.transform.position, Quaternion.identity);    
        rangeDisplayVar.transform.parent = gameObject.transform;
        rangeDisplayVar.transform.localScale = new Vector2(range/size, range/size);
        gameObject.GetComponent<BoxCollider2D>().size = new Vector2(colliderLength, colliderHeight);
        gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(colliderOffsetX, colliderOffsetY);

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = new Vector2(mousePos.x, mousePos.y);
        gameObject.transform.position = mousePos;
        gameCon.GetComponent<GameController>().placeable = canPlace;


       if (gameCon.GetComponent<GameController>().activePhantomTower == -1)
        {
            Destroy(gameObject);
        }
    }



    private void OnTriggerExit2D(Collider2D col)
    {
        canPlace = true;
        gameObject.GetComponent<SpriteRenderer>().sprite = towerScriptHolder.GetComponent<TowerScript>().towerSprites[id * 3 - 2];
        GameObject rangeObject = gameObject.transform.Find("Range(Clone)").gameObject;
        rangeObject.GetComponent<SpriteRenderer>().sprite = rangeSprites[0];
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        canPlace = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = towerScriptHolder.GetComponent<TowerScript>().towerSprites[id * 3 - 1];
        GameObject rangeObject = gameObject.transform.Find("Range(Clone)").gameObject;
        rangeObject.GetComponent<SpriteRenderer>().sprite = rangeSprites[1];
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        canPlace = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = towerScriptHolder.GetComponent<TowerScript>().towerSprites[id * 3 - 1];
        GameObject rangeObject = gameObject.transform.Find("Range(Clone)").gameObject;
        rangeObject.GetComponent<SpriteRenderer>().sprite = rangeSprites[1];
    }
}
