using System;
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

    public Slider moneySlider;
    public Text moneyText;

    public Vector3 originalCameraPosition;
    public Quaternion originalCameraRotation;
    public Animator moneyAnimator;

    private int money = 1000;

    public int maxMoney;

    public GameObject upgradeCanvas;

    private Animator upgradeCanvasAnimator;

    public Button buttonUpgrade;

    void ChangeMoney(int change=0)
    {
        money += change;
        if (money >= maxMoney)
        {
            money = maxMoney;
        }

        moneySlider.value = (float)money / maxMoney;
        moneyText.text = "Resources：" + money;
    }

    private void Awake()
    {
        originalCameraPosition = Camera.main.transform.position;
        originalCameraRotation = Camera.main.transform.rotation;
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
    IEnumerator SlowDownTimeAndMoveCamera()
    {
        float originalTimeScale = Time.timeScale;
        Time.timeScale = 0.5f; // 将时间缩放为原来的一半，即减慢游戏速度

        // 移动摄像机向右并放大
        Vector3 originalCameraPosition = Camera.main.transform.position;
        Quaternion originalCameraRotation = Camera.main.transform.rotation;

        Vector3 targetCameraPosition = originalCameraPosition - new Vector3(1f, -2f, 6f); // 向右移动摄像机
        Quaternion targetCameraRotation = Quaternion.Euler(45f, Mathf.Clamp(Camera.main.transform.rotation.eulerAngles.y + 5f, 0f, 5f), 0f); // 限制摄像机的旋转角度

        float duration = 0.5f; // 过渡时间

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            Camera.main.transform.position = Vector3.Lerp(originalCameraPosition, targetCameraPosition, elapsedTime / duration);
            Camera.main.transform.rotation = Quaternion.Lerp(originalCameraRotation, targetCameraRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Camera.main.transform.position = targetCameraPosition;
        Camera.main.transform.rotation = targetCameraRotation;

        yield return null;
    }

    void ShowUpgradeUI(Vector3 pos, bool isDisableUpgrade=false)
    {
        StopCoroutine("HideUpgradeUI");
        StartCoroutine("SlowDownTimeAndMoveCamera"); // 开始减慢游戏时间和移动摄像机
        upgradeCanvas.SetActive(false);
        upgradeCanvas.SetActive(true);
        upgradeCanvas.transform.position = pos;
        buttonUpgrade.interactable = !isDisableUpgrade;
    }
    
    IEnumerator RestoreOriginalCameraState()
    {
        Vector3 originalCameraPosition = Camera.main.transform.position;
        Quaternion originalCameraRotation = Camera.main.transform.rotation;

        Vector3 targetCameraPosition = originalCameraPosition + new Vector3(1f, -2f, 6f); // 向右移动摄像机
        Quaternion targetCameraRotation = Quaternion.Euler(70f, Mathf.Clamp(Camera.main.transform.rotation.eulerAngles.y - 5f, 0f, 5f), 0f); // 限制摄像机的旋转角度

        float duration = 0.5f; // 过渡时间

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            Camera.main.transform.position = Vector3.Lerp(originalCameraPosition, targetCameraPosition, elapsedTime / duration);
            Camera.main.transform.rotation = Quaternion.Lerp(originalCameraRotation, targetCameraRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Camera.main.transform.position = targetCameraPosition;
        Camera.main.transform.rotation = targetCameraRotation;

        yield return null;
    }


    IEnumerator HideUpgradeUI()
    {
        upgradeCanvasAnimator.SetTrigger("Hide");
        yield return new WaitForSeconds(0.8f);
        StartCoroutine("RestoreOriginalCameraState");
        Time.timeScale = 1f; // 恢复游戏时间正常
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
