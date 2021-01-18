using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TabButton : MonoBehaviour , IPointerClickHandler , IPointerEnterHandler , IPointerExitHandler
{
    private int tabIndex;

    [SerializeField]
    private Image image;

    [SerializeField]
    private TabController tabControlller;

    private bool isActive = false;

    private void Awake()
    {
        tabControlller = FindObjectOfType<TabController>();
        image = GetComponent<Image>();
    }

    public void SetIndex(int index)
    {
        tabIndex = index;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
       // Debug.Log("this is onpointer click");
        tabControlller.ButtonMouseClick(tabIndex);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isActive)
        {
            image.color = tabControlller.mouseEnterColor;
            //Debug.Log("this is onpointer enter");
        }

        tabControlller.ButtonMouseEnter(tabIndex);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isActive)
        {
            image.color = tabControlller.normalColor;
            //Debug.Log("this is on pointer exit");
        }

        tabControlller.ButtonMouseExit(tabIndex);

    }

    public void ToggleActive()
    {
        isActive = !isActive;

        if(isActive)
        {
            Debug.Log("toggleActive " + isActive);
            image.color = tabControlller.mouseClickColor;
        }
        else
        {
            Debug.Log("toggleActive " + isActive);
            image.color = tabControlller.normalColor;
        }
    }
}
