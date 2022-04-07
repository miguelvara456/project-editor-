using System;
using UnityEngine;
public class BehaviourObjects : MonoBehaviour,IDraggreable,IRotate,IScale,ITranslate
{
    [SerializeField] private bool isdrag;
    [SerializeField] private string objectName;
    private Collider col;
    public string ObjectName => objectName;
    public bool IsDrag => isdrag;

    private void Awake()
    {
        col = GetComponent<Collider>();
    }

    public Transform Drag()
    {
        isdrag = true;
        col.enabled = false;
        return transform;
    }

    public void OutDrag()
    {
        col.enabled = true;
        isdrag = false;
    }

    public Quaternion Rotate()
    {
        throw new System.NotImplementedException();
    }

    public Vector3 Scale()
    {
        throw new System.NotImplementedException();
    }

    public Vector3 translate()
    {
        throw new System.NotImplementedException();
    }

    public void SetObjectName(string newName)
    {
        objectName = newName;
    }
}
