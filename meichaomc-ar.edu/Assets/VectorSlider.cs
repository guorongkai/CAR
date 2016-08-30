using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class VectorSlider : MonoBehaviour {

	// Use this for initialization
	bool sliding = false;
	bool dragFlag = false;
	float startPosition = 0;
	public Button[] VectorsButtons;
	Dictionary<Button,int> buttonMap = new Dictionary<Button,int>();
	int CurrentButton = -1;
	public DrawVector vec1;
	public DrawVector vec2;
	void Start () {
		for (int i = 0; i < VectorsButtons.Length; i++) {
			buttonMap.Add (VectorsButtons[i], i);
			Button curt = VectorsButtons [i];
			VectorsButtons[i].onClick.AddListener(() => activeSliding(curt));
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (sliding)
		{
			if (Input.GetMouseButton(0))
			{

				if (!dragFlag)
				{
					startPosition = Input.mousePosition.y;
					dragFlag = true;
				}
				else {
					dragFlag = false;
					UpdateVectorValue((Input.mousePosition.y - startPosition)/10.0f);
				}
			}
			else if (Input.GetMouseButtonUp(0))
			{
				dragFlag = false;
				//sliding = false;
			}
		}
	}

	public void UpdateVectorValue(float delta){
		//Debug.Log ("delta is " + delta);
		switch (CurrentButton)
		{
		case 0:
			vec1.XYZ += new Vector3 (delta, 0f, 0f);
			break;
		case 1:
			vec1.XYZ += new Vector3 (0f, delta, 0f);
			break;
		case 2:
			vec1.XYZ += new Vector3 (0f, 0f, delta);
			break;
		case 3:
			vec2.XYZ += new Vector3 (delta, 0f, 0f);
			break;
		case 4:
			vec2.XYZ += new Vector3 (0f, delta, 0f);
			break;
		case 5:
			vec2.XYZ += new Vector3 (0f, 0f, delta);
			break;
		}
	}

	public void activeSliding(Button b){
		CurrentButton = buttonMap[b];
		sliding = true;
		//Debug.Log ("click button: " + b.ToString() + " current button : " + CurrentButton);
		for (int i = 0; i < VectorsButtons.Length; i++) {
			ColorBlock cb = VectorsButtons [i].colors;
			cb.normalColor = Color.white;
			cb.highlightedColor = Color.yellow;
			VectorsButtons [i].colors = cb;
		}
		ColorBlock cbc =  b.colors;
		cbc.normalColor = Color.yellow;
		cbc.highlightedColor = Color.yellow;
		b.colors = cbc;

	}


}
