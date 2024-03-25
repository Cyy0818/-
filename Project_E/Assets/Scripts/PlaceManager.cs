using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class PlaceManager : MonoBehaviour
{
    public PlaceUnitData WhiteUnit;
    public PlaceUnitData RedUnit;
    public PlaceUnitData BlueUnit;
    public PlaceUnitData ResourceUnit;
    public PlaceUnitData PlacedWall;
    public PlaceUnitData LandMine;
    public PlaceUnitData Bomb;
    private PlaceUnitData selectedPlaceUnitData; //当前选择的炮台
    
    //表示当前选择的炮台(场景中的游戏物体)
    private MapCube selectedMapCube;

    public Text moneyText;

    public Animator moneyAnimator;

    private int money = 1000;

    public GameObject upgradeCanvas;

    private Animator upgradeCanvasAnimator;

    public Button buttonUpgrade;

    void ChangeMoney(int change=0)
    {
        money += change;
        moneyText.text = "￥" + money;
    }

    void Start()
    {
        upgradeCanvasAnimator = upgradeCanvas.GetComponent<Animator>();
    }

    void Update()
    {
        if ( Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject()==false)
            {
                //开发炮台的建造
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                bool isCollider = Physics.Raycast(ray,out hit, 1000, LayerMask.GetMask("MapCube"));
                if (isCollider)
                {
                    MapCube mapCube = hit.collider.GetComponent<MapCube>();
                    if (selectedPlaceUnitData != null && mapCube.PlaceUnitGo == null)
                    {
                        //可以创建 
                        if (money > selectedPlaceUnitData.cost)
                        {
                            ChangeMoney(-selectedPlaceUnitData.cost);
                            mapCube.BuildPlaceUnit(selectedPlaceUnitData);
                        }
                        else
                        {
                            //提示钱不够
                            moneyAnimator.SetTrigger("Flicker");
                        }
                    }
                    else if (mapCube.PlaceUnitGo != null)
                    {
                        
                        // 升级处理
                        
                        //if (mapCube.isUpgraded)
                        //{
                        //    ShowUpgradeUI(mapCube.transform.position, true);
                        //}
                        //else
                        //{
                        //    ShowUpgradeUI(mapCube.transform.position, false);
                        //}
                        if (mapCube == selectedMapCube && upgradeCanvas.activeInHierarchy)
                        {
                            StartCoroutine(HideUpgradeUI());
                        }
                        else
                        {
                            ShowUpgradeUI(mapCube.transform.position, mapCube.isUpgraded);
                        }
                        selectedMapCube = mapCube;
                    }

                }
            }
        }
    }
    
//判断选择什么单位
    public void OnWhiteSelected(bool isOn)
    {
        if (isOn)
        {
            selectedPlaceUnitData = WhiteUnit;
        }
    }
    public void OnRedSelected(bool isOn)
    {
        if (isOn)
        {
            selectedPlaceUnitData = RedUnit;
        }
    }
    public void OnBlueSelected(bool isOn)
    {
        if (isOn)
        {
            selectedPlaceUnitData = BlueUnit;
        }
    }
    public void OnResourceSelected(bool isOn)
    {
        if (isOn)
        {
            selectedPlaceUnitData = ResourceUnit;
        }
    }
    public void OnWallSelected(bool isOn)
    {
        if (isOn)
        {
            selectedPlaceUnitData = PlacedWall;
        }
    }
    public void OnMineSelected(bool isOn)
    {
        if (isOn)
        {
            selectedPlaceUnitData = LandMine;
        }
    }
    public void OnBombSelected(bool isOn)
    {
        if (isOn)
        {
            selectedPlaceUnitData = Bomb;
        }
    }
    void ShowUpgradeUI(Vector3 pos, bool isDisableUpgrade=false)
    {
        StopCoroutine("HideUpgradeUI");
        upgradeCanvas.SetActive(false);
        upgradeCanvas.SetActive(true);
        upgradeCanvas.transform.position = pos;
        buttonUpgrade.interactable = !isDisableUpgrade;
    }

    IEnumerator HideUpgradeUI()
    {
        upgradeCanvasAnimator.SetTrigger("Hide");
        //upgradeCanvas.SetActive(false);
        yield return new WaitForSeconds(0.8f);
        upgradeCanvas.SetActive(false);
    }

    public void OnUpgradeButtonDown()
    {
        if (money >= selectedMapCube.PlaceUnitData.costUpgraded)
        {
            ChangeMoney(-selectedMapCube.PlaceUnitData.costUpgraded);
            selectedMapCube.UpgradePlaceUnit();
        }
        else
        {
            moneyAnimator.SetTrigger("Flicker");
        }

        StartCoroutine(HideUpgradeUI());
    }
    public void OnDestroyButtonDown()
    {
        selectedMapCube.DestroyPlaceUnit();
        StartCoroutine(HideUpgradeUI());
    }
    
}
