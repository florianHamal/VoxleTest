using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class NewBehaviourScript : MonoBehaviour
{
    private GameObject item = null;
    private bool fire1Released = true;
    public float grabDistanceDown = 1f;
    public float grabPositionHeightOffset = -0.5f;
    private GameObject markedGameObject = null;
    private Material markedGameObjectPrevMaterial = null;
    public Material markMaterial;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fire1Released == false && !(Input.GetAxis("Fire1") > 0))
        {
            fire1Released = true;
        }
        if(fire1Released)handleInteraction();
    }

    private void handleInteraction()
    {
        Vector3 rayVector = transform.position;
        rayVector.y -= 0.5f;
        RaycastHit hit;


        if (markedGameObject != null) markedGameObject.GetComponent<MeshRenderer>().material = markedGameObjectPrevMaterial;

        if (Physics.Raycast(rayVector, -transform.up, out hit, grabDistanceDown, LayerMask.GetMask("interactable")))
        {
            markedGameObject = hit.transform.gameObject;
            markedGameObjectPrevMaterial = markedGameObject.GetComponent<MeshRenderer>().material;
            markedGameObject.GetComponent<MeshRenderer>().material = markMaterial;
            if(Input.GetAxis("Fire1") > 0)handleMouseclick(hit);
        }


        
    }
    public void handleMouseclick(RaycastHit hit)
    {
        if (item == null)
        {
            item = hit.transform.gameObject;
            item.transform.parent = transform;
            item.transform.position = transform.position;
            item.transform.position += new Vector3(0, grabPositionHeightOffset, 0);
            item.transform.rotation = transform.rotation;

        }
        else
        {
            item.transform.parent = null;
            item.transform.position = hit.transform.position;
            item.transform.position += new Vector3(0, 1, 0);
            item.transform.rotation = new Quaternion(0, 0, 0, 0);             
            item = null;
        }
        fire1Released = false;
    }
}
