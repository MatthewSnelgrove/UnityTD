using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public GameController gameCon;
    // Start is called before the first frame update
    void Start()
    {
        gameCon = GameObject.Find("GameController").GetComponent<GameController>();
    }
    
    public void click(int id)
    {
        gameCon.phantomTowerSpawn(id);
    }
    
}
