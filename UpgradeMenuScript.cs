using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenuScript : MonoBehaviour
{
    public GameObject activeTower;
    public Text damageTxt;
    public Text sellTxt;
    public Image towSprite;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(activeTower != null)
        {
            damageTxt.text = ("Damage: " + Mathf.Round(activeTower.GetComponent<TowerScript>().damageDone).ToString());
            sellTxt.text = ("Sell for: " + Mathf.Round(activeTower.GetComponent<TowerScript>().moneySpent * 0.75f).ToString());
            towSprite.sprite = activeTower.GetComponent<SpriteRenderer>().sprite;
        }
    }
}
