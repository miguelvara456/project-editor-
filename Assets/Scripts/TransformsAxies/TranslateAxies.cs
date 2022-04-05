using System;
using UnityEngine;

public enum Axies
{
    x,y,z
}
public class TranslateAxies : MonoBehaviour,ITranslate
{
    [SerializeField] private Axies axies;
    public Vector3 translate()
    {
        return transform.position;
    }
}
