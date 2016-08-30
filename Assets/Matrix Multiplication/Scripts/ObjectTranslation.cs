using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class ObjectTranslation : MonoBehaviour {
    private Vector3 translation;
    public GameObject matrixController;

	// Use this for initialization
	void Start () {
	    
	}
	
    Vector3 GetTranslation()
    {
        float x = (float)Convert.ToDouble(matrixController.GetComponent<MatrixSlider>().matrixResultButtons[0].GetComponentInChildren<Text>().text);
        float y = (float)Convert.ToDouble(matrixController.GetComponent<MatrixSlider>().matrixResultButtons[1].GetComponentInChildren<Text>().text);
        float z = (float)Convert.ToDouble(matrixController.GetComponent<MatrixSlider>().matrixResultButtons[2].GetComponentInChildren<Text>().text);

        return new Vector3(x, y, z);
    }

	// Update is called once per frame
	void Update () {
        translation = GetTranslation();
        this.transform.position = translation;
    }
}
