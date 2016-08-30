using UnityEngine;
using System.Collections;

public class DrawVector : MonoBehaviour {

	// Use this for initialization
	public GameObject stick;
	public GameObject cone;
	public Vector3 XYZ;
	void Start () {
		draw (XYZ);
	}
	
	// Update is called once per frame
	void Update () {
		draw (XYZ);
	}

	public void draw(Vector3 veco){
		Vector3 vec = new Vector3 (veco.x, veco.y, -veco.z);
		cone.transform.localPosition = vec;
		cone.transform.localRotation = Quaternion.LookRotation (vec, Vector3.up);
		stick.transform.localPosition = vec * 0.5f;
		stick.transform.localRotation = Quaternion.LookRotation (vec, Vector3.forward);
		stick.transform.Rotate (new Vector3 (90f, 0f, 0f));
		stick.transform.localScale = new Vector3 (stick.transform.localScale.x, vec.magnitude/2f , stick.transform.localScale.z);

	}
}
