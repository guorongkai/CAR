using UnityEngine;
using System.Collections;

public class Coord2Control : MonoBehaviour {

	// Use this for initialization
	public GameObject coord1;
	public DrawVector coord1Vector;
	void Start () {
		gameObject.transform.position = coord1.transform.position + new Vector3(coord1Vector.XYZ.x, coord1Vector.XYZ.y, -1 * coord1Vector.XYZ.z) * coord1.transform.localScale.x;
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position = coord1.transform.position + new Vector3(coord1Vector.XYZ.x, coord1Vector.XYZ.y, -1 * coord1Vector.XYZ.z) * coord1.transform.localScale.x;
	}
}
