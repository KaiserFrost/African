using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : MonoBehaviour {

    public List<buildObjects> objects = new List<buildObjects>();
    public Vector3 currentpos;
    public buildObjects currentObject;
    public Transform currentPreview;
    public Transform cam;
    public RaycastHit hit;
    public LayerMask layer;
    public float gridSize = 1.0f;
    public float offset = 1.0f;
    public bool isBuilding;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isBuilding)
            startPreview();
	}

    public void ChangeCurrentBuilding()
    {
        GameObject curprev = Instantiate(currentObject.preview, currentpos, Quaternion.identity) as GameObject;
        currentPreview = curprev.transform;
    }

    public void startPreview()
    {
        if(Physics.Raycast(cam.position,cam.forward,out hit,10,layer))
        {
            if(hit.transform != this.transform)
            {
                ShowPreview(hit);
            }
        }
    }

    public void ShowPreview(RaycastHit hit2)
    {
       
        currentpos = hit2.point;
        currentpos -= Vector3.one * offset;
        currentpos /= gridSize;
        currentpos = new Vector3(Mathf.Round(currentpos.x), Mathf.Round(currentpos.y), Mathf.Round(currentpos.z));
        currentpos *= gridSize;
        currentpos += Vector3.one * offset;
        currentPreview.position = currentpos;
    }
}
[System.Serializable]
public class buildObjects
{
    public string name;
    public GameObject preview;
}
