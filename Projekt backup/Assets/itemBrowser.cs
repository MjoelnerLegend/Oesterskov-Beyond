using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemBrowser : MonoBehaviour {

    public GameObject infopanel;
    public GameObject dropDown;
    public GameObject selectedItem;

    float timepoint= 0;
    float cooldown = 2;

    private void LateUpdate()
    {
        if(Time.time > timepoint + cooldown)
        {
            timepoint = Time.time;
            updateoptions();
        }
    }

    private void updateoptions()
    {
        dropDown.GetComponent<Dropdown>().ClearOptions();
        List<string> newList = new List<string>();
        foreach (Item t in DatabaseHandler.itemDict.ToArray())
        {
            newList.Add(t.id);
        }
        dropDown.GetComponent<Dropdown>().AddOptions(newList);
    }

    public void addnewitem()
    {
        infopanel.GetComponent<VisualizeInfo>().addItem(selectedItem.GetComponent<Text>().text);
    }

}
