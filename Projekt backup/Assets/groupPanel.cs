using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class groupPanel : MonoBehaviour {
    public GameObject groupPrefab;
    public void addGroup(Group group)
    {
        GameObject tmp = Instantiate<GameObject>(groupPrefab);
        tmp.GetComponent<infoDisplayer>().thisGroup = group;
        tmp.GetComponent<infoDisplayer>().nametxt.GetComponent<Text>().text = "Group" + group.groupNumber.ToString();
        tmp.transform.SetParent(this.transform);
    }
}
