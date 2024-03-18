using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlaceManager : MonoBehaviour
{
    public PlaceUnit WhiteUnit;
    public PlaceUnit RedUnit;
    public PlaceUnit BlueUnit;
    public PlaceUnit ResourceUnit;
    public PlaceUnit PlacedWall;
    public PlaceUnit LandMine;
    public PlaceUnit Bomb;

    private PlaceUnit selectedPlaceUnit;
    //当前选择的炮台

    public void OnWhiteSelected(bool isOn)
    {
        if (isOn)
        {
            selectedPlaceUnit = WhiteUnit;
        }
    }
    public void OnRedSelected(bool isOn)
    {
        if (isOn)
        {
            selectedPlaceUnit = RedUnit;
        }
    }
    public void OnBlueSelected(bool isOn)
    {
        if (isOn)
        {
            selectedPlaceUnit = BlueUnit;
        }
    }
    public void OnResourceSelected(bool isOn)
    {
        if (isOn)
        {
            selectedPlaceUnit = ResourceUnit;
        }
    }
    public void OnWallSelected(bool isOn)
    {
        if (isOn)
        {
            selectedPlaceUnit = PlacedWall;
        }
    }
    public void OnMineSelected(bool isOn)
    {
        if (isOn)
        {
            selectedPlaceUnit = LandMine;
        }
    }
    public void OnBombSelected(bool isOn)
    {
        if (isOn)
        {
            selectedPlaceUnit = Bomb;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                bool isCollider = Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("Map")); //将所有的地砖放到map的layer层
                if (isCollider)
                {
                    GameObject MapBlock = hit.collider.gameObject;
                }
            }
        }
    }
}
