using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.IO;

public class Color_picker_tester : MonoBehaviour 
{

	public  Image image;
	public new Renderer renderer;
	public ColorPicker picker;

	public  Color Color = Color.red;

	// Use this for initialization
	void Start () 
	{
		
		image = GetComponent<Image>();

		picker.onValueChanged.AddListener(color =>
			{
				image.color =  color;
				 Color =  color;
			});

		renderer.material.color = picker.CurrentColor;

		picker.CurrentColor = Color;

	}
}

	// Update is called once per frame


