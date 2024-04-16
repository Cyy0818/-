using System.Collections;
using System.Collections.Generic;
using Path;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Vector3 = System.Numerics.Vector3;

public class MapCube : MonoBehaviour {
    [HideInInspector]
    public GameObject PlaceUnitGo;//保存当前cube身上的炮台
    [HideInInspector]
    public PlaceUnitData PlaceUnitData;
    [HideInInspector]
    public bool isUpgraded = false;

    public GameObject buildEffect;

    private Renderer renderer;

    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    public void BuildPlaceUnit(PlaceUnitData PlaceUnitData)
    {
        this.PlaceUnitData = PlaceUnitData;
        isUpgraded = false;
        PlaceUnitGo = GameObject.Instantiate(PlaceUnitData.PlaceUnitPrefab, transform.position+new UnityEngine.Vector3(0,1,0), Quaternion.identity);
        GameObject effect = GameObject.Instantiate(buildEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1.5f);
        //增加权重
        var x = (int)Mathf.Round(transform.position.x);
        var y = (int)Mathf.Round(transform.position.z);
        NodeManager.Increase(x, y, 1, 1, 1);
    }

    public void UpgradePlaceUnit()
    {
        if(isUpgraded==true)return;

        Destroy(PlaceUnitGo);
        isUpgraded = true;
        PlaceUnitGo = GameObject.Instantiate(PlaceUnitData.PlaceUnitUpGradePrefab, transform.position, Quaternion.identity);
        GameObject effect = GameObject.Instantiate(buildEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1.5f);
    }
    
    public void DestroyPlaceUnit()
    {
        Destroy(PlaceUnitGo);
        isUpgraded = false;
        PlaceUnitGo = null;
        PlaceUnitData=null;
        GameObject effect = GameObject.Instantiate(buildEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1.5f);
        //减少权重
        var x = (int)Mathf.Round(transform.position.x);
        var y = (int)Mathf.Round(transform.position.z);
        NodeManager.Reduce(x, y, 1, 1, 1);
    }

    void OnMouseEnter()
    {
        if (PlaceUnitGo == null && EventSystem.current.IsPointerOverGameObject()==false)
        {
            renderer.material.color = Color.red;
        }
    }
    void OnMouseExit()
    {
        renderer.material.color = Color.white;
    }
}