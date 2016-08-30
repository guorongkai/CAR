using UnityEngine;
using System.Collections;

public class AddVector : MonoBehaviour {

	// Use this for initialization
	public DrawVector Vec1;
	public DrawVector Vec2;
	public DrawVector Vec3;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vec3.XYZ = Vec1.XYZ + Vec2.XYZ;
	}
}
