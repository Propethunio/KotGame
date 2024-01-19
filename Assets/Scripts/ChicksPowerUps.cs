using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Chick Power Up", menuName = "ChickPowerUp")]
public class ChicksPowerUps : ScriptableObject
{
    public float durationTime;
    public GameObject prefab;
    public Tags tagPrefab;

    public void Start()
    {
        switch (tagPrefab)
        {
            case Tags.Protection:
                prefab.tag = "Protection";
                break;
            case Tags.Bomb:
                prefab.tag = "Bomb";
                break;
            case Tags.Speed:
                prefab.tag = "Speed";
                break;
            case Tags.Strength:
                prefab.tag = "Strength";
                break;
        }
    }
}

public enum Tags
{
    Protection,
    Bomb,
    Speed,
    Strength
}