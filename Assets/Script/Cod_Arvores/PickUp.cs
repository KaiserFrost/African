using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : InventoryItemBase {

    public GameObject invPanel;
    public GameObject[] invIcons;//array pos icons

    void OnTriggerStay(Collider collision)
    {
        //look through children for existing icon
        foreach(Transform child in invPanel.transform)
        {
            //if item already in inventory
            if(child.gameObject.tag == collision.gameObject.tag)
            {
                string c = child.Find("Text").GetComponent<Text>().text;
                int tcount = System.Int32.Parse(c) + 1;
                child.Find("Text").GetComponent<Text>().text = "" + tcount;
                return;
            }
        }

      /*  //ta um bocado à preguiçoso, mas depois vesse
        GameObject i;
        if (collision.gameObject.tag == "Fruta1")
        {
            i = Instantiate(invIcons[0]);
            i.transform.SetParent(invPanel.transform);

        }
        else if (collision.gameObject.tag == "Fruta2")
        {
            i = Instantiate(invIcons[1]);
            i.transform.SetParent(invPanel.transform);

        }
        else if (collision.gameObject.tag == "Fruta3")
        {
            i = Instantiate(invIcons[2]);
            i.transform.SetParent(invPanel.transform);

        }
        */

    }





	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
