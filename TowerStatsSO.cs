using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerStatsSO", menuName = "TowerStatsSO")]
public class TowerStatsSO : ScriptableObject
{
    public int cost;
    public float range;
    public float damage;
    public float frequency;
    public int pierce;
    public float speed;
    public float lifetime;
    public int numOfShots;
    public float shotAngle;
    public float slowAmount;
    public float rotationSpeed;

    public Sprite towerSprite;
    public Sprite towerSpriteRed;
    public Sprite bulletSprite;   
    public float size;
    public float colliderLength;
    public float colliderHeight;
    public float colliderOffsetX;
    public float colliderOffsetY;
    public int id;
    public string[] upgradeStat;
    public AnimationClip build;
    public AnimationClip idle;
    public AnimationClip shoot;


}
