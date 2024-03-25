using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlaceManager : MonoBehaviour
{
    public PlaceUnit WhiteUnit;
    public PlaceUnit RedUnit;
    public PlaceUnit BlueUnit;
    public PlaceUnit ResourceUnit;
    public PlaceUnit PlacedWall;
    public PlaceUnit LandMine;
    public PlaceUnit Bomb;
    public int Resources = 100;
    private PlaceUnit selectedPlaceUnit; //当前选择的炮台

    public Text ResourcesText;
    private void UpdateResources(int change=0)
    {
        Resources += change;
        ResourcesText.text = "resources:"+Resources;
    }
//判断选择什么单位
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
                bool isCollider = Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("Map")); //将所有的地砖放到map（layer）层
                if (isCollider)
                {
                    MapCube mapCube = hit.collider.GetComponent<MapCube>();
                    if (mapCube.PlaceUnitGo == null)
                    {
                        if (Resources >= selectedPlaceUnit.cost)
                        {
                            //钱够
                            Resources -= selectedPlaceUnit.cost;
                            mapCube.place(selectedPlaceUnit.PlaceUnitPrefab);
                            UpdateResources(-selectedPlaceUnit.cost);
                        }
                        else
                        {
                            //钱不够
                        }
                        
                        
                    }
                    else
                    {
                        Debug.Log("升级");
                    }
                }
            }
        }
    }
}
