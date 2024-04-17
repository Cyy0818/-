using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Swipe_Menu : MonoBehaviour
{
    public GameObject scrollBar;

    private float scrollPos=0;

    private float[] pos;

    // Update is called once per frame
    void Update()
    {
        pos = new float[transform.childCount];
        float distance = 1.0f / (pos.Length - 1.0f);
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
            
        }

        if (Input.GetMouseButton(0))
        {
            scrollPos = scrollBar.GetComponent<Scrollbar>().value;
            
        }
        else
        {
            for (int i = 0; i < pos.Length; i++)
            {
                if (scrollPos<pos[i]+(distance/2)&&scrollPos>pos[i]-(distance/2))
                {
                    scrollBar.GetComponent<Scrollbar>().value =
                        Mathf.Lerp(scrollBar.GetComponent<Scrollbar>().value, pos[i], 0.1f);
                }
            }
        }

        for (int i = 0; i < pos.Length; i++)
        {
            if (scrollPos < pos[i] + (distance / 2) && scrollPos > pos[i] - (distance / 2))
            {
                transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(1.0f, 1.0f), 0.1f);
            }
            else
            {
                transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(0.8f, 0.8f), 0.1f);
            }
        }

    }
}
