using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class MatrixSlider : MonoBehaviour {
    public Button matrixSlider;

    //0-5: p, q, r, x, y, z
    public Button[] matrixButtons;
    public Button[] matrixResultButtons;

    private int activatingIndex = 0;
    private bool sliding = false;

    private float startPosition = 0;
    private bool dragFlag = false;

    public Button showInput;

    // Use this for initialization
    void Start () {
        matrixButtons[0].onClick.AddListener(() => ActivateMatrixSlider("matrixA", 0));
        matrixButtons[1].onClick.AddListener(() => ActivateMatrixSlider("matrixA", 1));
        matrixButtons[2].onClick.AddListener(() => ActivateMatrixSlider("matrixA", 2));
        matrixButtons[3].onClick.AddListener(() => ActivateMatrixSlider("matrixB", 3));
        matrixButtons[4].onClick.AddListener(() => ActivateMatrixSlider("matrixB", 4));
        matrixButtons[5].onClick.AddListener(() => ActivateMatrixSlider("matrixB", 5));

        matrixResultButtons[0].onClick.AddListener(() => ActivateMatrixSlider("resultMatrix", 0));
        matrixResultButtons[1].onClick.AddListener(() => ActivateMatrixSlider("resultMatrix", 1));
        matrixResultButtons[2].onClick.AddListener(() => ActivateMatrixSlider("resultMatrix", 2));

        matrixSlider.onClick.AddListener(() => DisableMatrixSlider());
    }
	
    void ActivateMatrixSlider(string matrixType, int activeButtonIndex)
    {
        //Debug.Log(matrixButtons[activeButtonIndex].GetComponentInChildren<Text>().text);
        matrixSlider.gameObject.SetActive(true);
        SwitchButtonsStaus(false);
        if (matrixType.Equals("matrixA") || matrixType.Equals("matrixB"))
            matrixSlider.GetComponentInChildren<Text>().text = matrixButtons[activeButtonIndex].GetComponentInChildren<Text>().text;
        else
            matrixSlider.GetComponentInChildren<Text>().text = matrixResultButtons[activeButtonIndex].GetComponentInChildren<Text>().text;
        sliding = true;
        activatingIndex = activeButtonIndex;
    }

    void DisableMatrixSlider()
    {
        sliding = false;
        SwitchButtonsStaus(true);
        matrixSlider.gameObject.SetActive(false);
    }

    void SwitchButtonsStaus(bool bottonFlag)
    {
        for(int i = 0; i < matrixButtons.Length; i++)
        {
            matrixButtons[i].interactable = bottonFlag;
        }
    }
    
    void UpdateMatrixValue(float y)
    {
        float sliderValue = (float)Convert.ToDouble(matrixSlider.GetComponentInChildren<Text>().text);
        sliderValue += y;
        matrixSlider.GetComponentInChildren<Text>().text = sliderValue.ToString();
        matrixButtons[activatingIndex].GetComponentInChildren<Text>().text = matrixSlider.GetComponentInChildren<Text>().text;
    }

    void UpdateMatrixResult()
    {
        float p = (float)Convert.ToDouble(matrixButtons[0].GetComponentInChildren<Text>().text);
        float q = (float)Convert.ToDouble(matrixButtons[1].GetComponentInChildren<Text>().text);
        float r = (float)Convert.ToDouble(matrixButtons[2].GetComponentInChildren<Text>().text);

        float x = (float)Convert.ToDouble(matrixButtons[3].GetComponentInChildren<Text>().text);
        float y = (float)Convert.ToDouble(matrixButtons[4].GetComponentInChildren<Text>().text);
        float z = (float)Convert.ToDouble(matrixButtons[5].GetComponentInChildren<Text>().text);

        matrixResultButtons[0].GetComponentInChildren<Text>().text = (x + p).ToString();
        matrixResultButtons[1].GetComponentInChildren<Text>().text = (y + q).ToString();
        matrixResultButtons[2].GetComponentInChildren<Text>().text = (z + r).ToString();
    }

    // Update is called once per frame
    void Update () {
        
        if (sliding)
        {
            if (Input.GetMouseButton(0))
            {
                //showInput.GetComponentInChildren<Text>().text = (Input.GetAxis("Mouse Y")).ToString();
                //UpdateMatrixValue(Input.GetAxis("Mouse Y"));
                //UpdateMatrixResult();

                if (!dragFlag)
                {
                    startPosition = Input.mousePosition.y;
                    dragFlag = true;
                }
                else {
                    //Debug.Log(Input.GetAxis("Mouse Y") + ", " + startPosition);
                    dragFlag = false;
                    showInput.GetComponentInChildren<Text>().text = (Input.mousePosition.y - startPosition).ToString();
                    UpdateMatrixValue((Input.mousePosition.y - startPosition)/10.0f);
                    UpdateMatrixResult();
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                dragFlag = false;
            }
        }
    }
}
