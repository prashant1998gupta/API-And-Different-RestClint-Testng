using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TabController : MonoBehaviour
{
    [Header("Panel")]
    public Transform buttonPanel;
    public Transform PanelsPanel;


    [Header("Button State Color")]
    public Color mouseClickColor;
    public Color mouseEnterColor;
    public Color normalColor;

   /* [Header("Events")]
    public UnityEvent tabSelectionChangeEvent;
*/
    [SerializeField]
    private int selectedIndex;

    [SerializeField]
    private TabButton selectedButton;

    [SerializeField]
    private List<TabButton> buttons = new List<TabButton>();
    [SerializeField]
    private List<Transform> panels = new List<Transform>();

    private void Awake()
    {

    }

    private void Start()
    {
        for (int i = 0; i < buttonPanel.transform.childCount; i++)
        {
            GameObject buttonGo = buttonPanel.transform.GetChild(i).gameObject;

            TabButton button = buttonGo.GetComponent<TabButton>();

            button.SetIndex(i);

            buttons.Add(button);
        }

        foreach (Transform item in PanelsPanel.transform)
        {
            panels.Add(item);
        }

        ButtonMouseClick(0);
    }

    public void ButtonMouseClick(int tabIndex)
    {
        if (selectedButton != null)
        {
            selectedButton.ToggleActive();
        }

        selectedIndex = tabIndex;
        selectedButton = buttons[selectedIndex];
        selectedButton.ToggleActive();
        HideAllPanels();

    }

    public void ButtonMouseExit(int tabIndex)
    {
        //Debug.Log("this is mouse exit");

    }

    public void ButtonMouseEnter(int tabIndex)
    {
       // Debug.Log("this is ButtonMoseEnter");

    }

    

    private void HideAllPanels()
    {
        for (int i = 0; i < panels.Count; i++)
        {
            if(i == selectedIndex)
            {
                panels[i].gameObject.SetActive(true);
            }
            else
            {
                panels[i].gameObject.SetActive(false);
            }
        }

        /*if(tabSelectionChangeEvent != null)
        {
            tabSelectionChangeEvent.Invoke();
        }*/
    }
}
