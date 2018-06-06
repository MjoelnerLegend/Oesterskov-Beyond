using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemHandler : MonoBehaviour {

    public GameObject addButton;
    public GameObject deleteButton;
    public GameObject amount;
    public GameObject itemName;
    Group group;

	// Use this for initialization
	void Start () {
        group = this.transform.parent.parent.gameObject.GetComponent<VisualizeInfo>().currentGroup;
    }

    public void plus()
    {
        this.transform.parent.parent.GetComponent<VisualizeInfo>().addItem(itemName.GetComponent<Text>().text);
    }

    public void minus()
    {
        this.transform.parent.parent.GetComponent<VisualizeInfo>().deleteItem(itemName.GetComponent<Text>().text);
    }
}