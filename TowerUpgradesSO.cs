using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerUpgradesSO", menuName = "TowerUpgradesSO")]
public class TowerUpgradesSO : ScriptableObject
{
    public int[] cost;
    public float[] range;
    public float[] damage;
    public float[] frequency;
    public int[] pierce;
    public float[] speed;
    public float[] lifetime;    
    public int[] numOfShots;
    public float[] shotAngle;
    public float[] rotationSpeed;
    public float[] slowAmount;
    public string[] descriptions;
    }
