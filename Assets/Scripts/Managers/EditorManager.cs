using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorManager : MonoBehaviour
{
    [SerializeField] private LayerMask layerForRaycastMouse;
    [SerializeField] private GameObject[] transformAxiesPrefabs;
    [SerializeField] private List<BehaviourObjects> objectsSelecteds;
    [SerializeField] private BehaviourObjects[] objectsToCreates;
    private CameraController cameraController;
    private GameObject[] axiesTransforms;
    private bool isInteractiveInWorldSpace;
    private UIManager uiManager;


    private void Awake()
    {
        uiManager = GetComponent<UIManager>();
        cameraController = Camera.main.GetComponent<CameraController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        InitializeEditor();
    }

    public void InitializeEditor()
    {
        CreateAxiesTransforms();
        RemoveDeselectedObject();
        ActiveInteractiveInWorldSpace();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && isInteractiveInWorldSpace)
            SelectFromRaycast();
        
    }

    private void SelectFromRaycast()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(cameraRay, out hit,500f,layerForRaycastMouse))
        {
            if (hit.collider.CompareTag("Objects"))
            {
                var obj = hit.collider.GetComponent<BehaviourObjects>();
                if (!obj.IsDrag)
                {
                    RemoveDeselectedObject();
                    AddObjectSelected(obj);
                }
            }
            else if (hit.collider.CompareTag("Axies"))
            {
                
            }
            else 
            {
                RemoveDeselectedObject();
            }
        }
    }

    private void AddObjectSelected(BehaviourObjects objectDrag)
    {
        var iComponent = objectDrag.GetComponent<IDraggreable>();
        print($"Adding: {objectDrag.name} in to the list ");
        objectsSelecteds.Add(objectDrag);
        iComponent.Drag();
        var axieTransforms = axiesTransforms[0];
        axieTransforms.SetActive(true);
        axieTransforms.transform.position = objectDrag.transform.position;
        uiManager.ActivePanel(Panels.atributes);
        uiManager.SetAttributeObject(objectDrag);
    }

    private void RemoveDeselectedObject()
    {
        for (int i = 0; i < objectsSelecteds.Count; i++)
        {
            var iComponent = objectsSelecteds[i].GetComponent<IDraggreable>();
            iComponent.OutDrag();
            objectsSelecteds.RemoveAt(i);
        }

        var axieTransforms = axiesTransforms[0];
        axieTransforms.SetActive(false);
        print($"Remove all objects in to the list ");
        uiManager.ActivePanel(Panels.creations);
    }

    private void CreateAxiesTransforms()
    {
        axiesTransforms = new GameObject[transformAxiesPrefabs.Length];
        for (int i = 0; i < transformAxiesPrefabs.Length; i++)
        {
            axiesTransforms[i] = Instantiate(transformAxiesPrefabs[i]);
            axiesTransforms[i].SetActive(false);
        }
    }

    public void CreateObject(string objectToCreate)
    {
        foreach (var objects in objectsToCreates)
        {
            if (objects.ObjectName == objectToCreate)
            {
                var newObj = Instantiate(objects, Vector3.zero, Quaternion.identity);
                RemoveDeselectedObject();
                AddObjectSelected(newObj);
                
            }
        }
    }

    public void CreateGrid()
    {
        
    }

    public void ActiveInteractiveInWorldSpace()
    {
        isInteractiveInWorldSpace = true;
    }
    public void DisactiveInteractiveInWorldSpace()
    {
        isInteractiveInWorldSpace = false;
    }
}
