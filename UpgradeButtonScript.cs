using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButtonScript : MonoBehaviour
{
    public Image tooltip;
    public string tooltipText;
    public GameController gameCon;
    public bool maxed;
    public bool afford;
    private float[] red;
    private float[] green;
    private float[] grey;
    public int cost;

    // Start is called before the first frame update
    void Start()
    {
        gameCon = GameObject.Find("GameController").GetComponent<GameController>();

        red = new float[] {255f / 255, 45f / 255, 45f / 255 };
        green = new float[] {60f / 255, 255f / 255, 60f / 255};
        grey = new float[] {130f / 255, 130f / 255, 130f / 255};
    }

    public void click(int path)
    {
        if(maxed == false)
        {
            gameCon.selectedTower.GetComponent<TowerScript>().upgrade(path);
        }
       
    }

    public void Update()
    {

        {
            if (maxed)
            {
                GetComponentInChildren<Text>().text = ("MAXED\n");
                GetComponent<Image>().color = new Color(grey[0], grey[1], grey[2]);
            }
            else
            {
                if (cost <= gameCon.money)
                {
                    GetComponent<Image>().color = new Color(green[0], green[1], green[2]);
                    
                }
                else
                {
                    GetComponent<Image>().color = new Color(red[0], red[1], red[2]);
                }
            }
        }
        
    }


    public void OnMouseEnter()
    {
        Debug.Log("Over");
        tooltip.gameObject.SetActive(true);
    }

    public void OnMouseOver()
    {
        if (maxed)
        {
            tooltip.GetComponentInChildren<Text>().text = ("Maxed");
        }
        else
        {
            tooltip.GetComponentInChildren<Text>().text = tooltipText;
        }
        
    }

    public void OnMouseExit()
    {
        Debug.Log("NOTOVER");
        tooltip.gameObject.SetActive(false);

    }
}
