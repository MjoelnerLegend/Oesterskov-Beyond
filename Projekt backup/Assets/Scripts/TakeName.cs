using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakeName : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.gameObject.GetComponent<Text>().text = this.gameObject.transform.parent.name;
	}

}
