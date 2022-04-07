using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Panels
{
    atributes,creations
}
public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] panels;
    [SerializeField] private UIAttributes uiAttributes;

    public void CloseAllPanels()
    {
        foreach (var panel in panels)
        {
            panel.SetActive(false);
        }
    }

    public void ActivePanel(Panels panel)
    {
        CloseAllPanels();
        switch (panel)
        {
            case Panels.creations:
                panels[0].SetActive(true);
                break ;
            case Panels.atributes:
                panels[1].SetActive(true);
                break ;
        }
    }


    public void SetAttributeObject(BehaviourObjects newBehaviourObject)
    {
        uiAttributes.SetBehaviourObject(newBehaviourObject);
    }
}
