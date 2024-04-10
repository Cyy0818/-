using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PlaceUnitData
{
    public int cost;
    [Header("PlaceUnit")]
    public GameObject PlaceUnitPrefab;
    
    [Header("Upgrade")] 
    public GameObject PlaceUnitUpGradePrefab;
    public int costUpgraded;

    [Header("attack range")]
    public GameObject attackRangeIndicatorPrefab;
}   
public enum PlaceUnitType
{
    WhitePlaceUnit,
    RedPlaceUnit,
    BluePlaceUnit,
    ResourceUnit,
    PlacedWall,
    LandMine,
    Bomb
}