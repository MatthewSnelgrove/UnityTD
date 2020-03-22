using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New BulStatsSO", menuName = "BulStatsSO")]
public class BulStatsSO : ScriptableObject
{
    public float bulletSpeed;
    public float bulletDamage;
    public int bulletPierce;
    public bool slow;
    public float rotationSpeed;
    public Sprite bulSprite;

    public float timeLeft;

    public GameObject target;
    public GameObject towerParent;
    public GameObject firstTarget;
}
