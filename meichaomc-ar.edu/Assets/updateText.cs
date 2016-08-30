using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class updateText : MonoBehaviour {

	// Use this for initialization
	public Button[] buttons;
	public DrawVector vec1;
	public DrawVector vec2;
	public DrawVector vec3;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		changeButtonText (buttons[0], vec1.XYZ.x.ToString());
		changeButtonText (buttons[1], vec1.XYZ.y.ToString());
		changeButtonText (buttons[2], vec1.XYZ.z.ToString());
		changeButtonText (buttons[3], vec2.XYZ.x.ToString());
		changeButtonText (buttons[4], vec2.XYZ.y.ToString());
		changeButtonText (buttons[5], vec2.XYZ.z.ToString());
		changeButtonText (buttons[6], vec3.XYZ.x.ToString());
		changeButtonText (buttons[7], vec3.XYZ.y.ToString());
		changeButtonText (buttons[8], vec3.XYZ.z.ToString());
	}

	void changeButtonText (Button b, string tex){
		Text t = b.GetComponentInChildren<Text> ();
		t.text = tex;

	}
}
