using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualizeInfo : MonoBehaviour
{
    public GameObject members;
    public GameObject inventory;
    public GameObject medlemPrefab;
    public GameObject itemPrefab;
    public GameObject databaseObject;

    public Group currentGroup;

    public void showGroup(Group newgroup)
    {
        if (currentGroup.invTable.Length <= 1);
        {
            //PUSH OLD GROUP
            databaseObject.GetComponent<DatabaseHandler>().updateitems(currentGroup.inventory, currentGroup.invTable);
        }

        Transform[] oldmembers = members.GetComponentsInChildren<Transform>();
        Transform[] oldinventory = inventory.GetComponentsInChildren<Transform>();

        //Out with the old
        foreach (Transform t in oldmembers)
        {
            if (t.gameObject != members)
                Destroy(t.gameObject);
        }

        foreach (Transform t in oldinventory)
        {
            if (t.gameObject != inventory)
                Destroy(t.gameObject);
        }


        //In with new group
        currentGroup = newgroup;

        //In with the new
        foreach (Student s in currentGroup.members)
        {
            GameObject tmp = Instantiate<GameObject>(medlemPrefab);
            tmp.GetComponentInChildren<Text>().text = s.name;
            tmp.transform.SetParent(members.transform);
        }

        //Retrieve inventory
        currentGroup.inventory = databaseObject.GetComponent<DatabaseHandler>().getitems(currentGroup.invTable);

        foreach (Item t in currentGroup.inventory)
        {
            GameObject tmp = Instantiate<GameObject>(itemPrefab);
            tmp.GetComponent<itemHandler>().itemName.GetComponentInChildren<Text>().text = t.id;
            tmp.GetComponent<itemHandler>().amount.GetComponentInChildren<Text>().text = t.amount.ToString();
            tmp.transform.SetParent(inventory.transform);
        }
    }

    public void addItem(string item)
    {
        foreach (Item t in currentGroup.inventory)
        {
            if (t.id == item)
            {
                t.amount += 1;
            }
            else
            {
                List<Item> temp = DatabaseHandler.itemDict;
                foreach (Item g in temp)
                {
                    if (g.id == item)
                    {
                        Item nt = g;
                        currentGroup.inventory.Add(nt);
                    }
                }
            }
        }
        showGroup(currentGroup);
    }

    public void deleteItem(string item)
    {
        foreach (Item t in currentGroup.inventory)
        {
            if(t.id == item)
            {
                t.amount -= 1;
                if (t.amount <= 0)
                    currentGroup.inventory.Remove(t);
            }
        }
        showGroup(currentGroup);
    }
}
