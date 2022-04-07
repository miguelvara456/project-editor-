using System;
using TMPro;
using UnityEngine;


public class UIAttributes : MonoBehaviour
{
    [SerializeField] private BehaviourObjects behaviourObject;
    [SerializeField] private TMP_InputField inputFieldTextName;

    private void Awake()
    {
        inputFieldTextName.onEndEdit.AddListener(SetAttributeNameObject);   
    }

    private void SetAttributeNameUI(string name)
    {
        inputFieldTextName.text = name;
    }
    
    public void SetAttributeNameObject(string name)
    {
        behaviourObject.SetObjectName(name);
    }
    
    public void SetBehaviourObject(BehaviourObjects newBehaviourObject)
    {
        behaviourObject = newBehaviourObject;
        SetAttributeNameUI(behaviourObject.ObjectName);
    }
}
