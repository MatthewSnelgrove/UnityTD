using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhantomTowerScript : MonoBehaviour
{

    public TowerStatsSO towerStats;
    public SpriteRenderer sr;
    public GameController gameCon;
    public GameObject rangeDisplayPrefab;
    public PolygonCollider2D water;
    public PolygonCollider2D road;
    public Sprite[] rangeSprites;
    public bool canPlace;
    // Start is called before the first frame update
    void Start()
    {
        sr.sortingOrder = 2;
        sr.sprite = towerStats.towerSprite;
        canPlace = true;
        gameCon = GameObject.Find("GameController").GetComponent<GameController>();
        var rangeDisplayVar = Instantiate(rangeDisplayPrefab, gameObject.transform.position, Quaternion.identity);
        rangeDisplayVar.transform.parent = gameObject.transform;
        rangeDisplayVar.transform.localScale = new Vector2(towerStats.range, towerStats.range);
        gameObject.GetComponent<BoxCollider2D>().size = new Vector2(towerStats.colliderLength, towerStats.colliderHeight);
        gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(towerStats.colliderOffsetX, towerStats.colliderOffsetY);

        water = GameObject.Find("Water").GetComponent<PolygonCollider2D>();
        road = GameObject.Find("Road").GetComponent<PolygonCollider2D>();

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
        GetComponent<SpriteRenderer>().sprite = towerStats.towerSprite;
        gameObject.transform.Find("Range(Clone)").gameObject.GetComponent<SpriteRenderer>().sprite = rangeSprites[0];
    }

    private void OnTriggerStay2D(Collider2D col)
    {

        if (col.CompareTag("Unplaceable") || col.CompareTag("Tower"))
        {
            canPlace = false;
            GetComponent<SpriteRenderer>().sprite = towerStats.towerSpriteRed;
            gameObject.transform.Find("Range(Clone)").gameObject.GetComponent<SpriteRenderer>().sprite = rangeSprites[1];
        }
    }

}
