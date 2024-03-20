using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PlaceUnit
{
    [Header("UI")] 
    public int level;
    public int cost;
    public int HPMax;
    public Slider HP;
    public Slider time;

    [Header("PlaceUnit attack")]
    public GameObject PlaceUnitPrefab;

    [Header("Upgrade")] 
    public GameObject PlaceUnitUpGradePrefab;
    public int levelupcost;

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
