using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class infoDisplayer : MonoBehaviour {

    GameObject infobox;
    VisualizeInfo vinf;
    public Group thisGroup;
    public GameObject nametxt;

	// Use this for initialization
	void Start () {
        infobox = transform.parent.parent.GetComponent<GroupManager>().infobox;
        vinf = infobox.GetComponent<VisualizeInfo>();
    }

    public void showInfo()
    {
        vinf.showGroup(thisGroup);
    }

}
