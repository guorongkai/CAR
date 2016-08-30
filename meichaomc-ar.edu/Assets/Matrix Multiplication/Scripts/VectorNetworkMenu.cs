using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class VectorNetworkMenu : MonoBehaviour {
    public string IP = "10.0.0.149";
    public int Port = 25001;

    public GameObject vectorSliderGUI;
    public GameObject targetObject;

    private bool questionFlag = false;
    private bool questionAllowed = false;

    private Vector3 translation;

    private Vector3 origPosition;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(Network.peerType == NetworkPeerType.Server)
        {
            if (!questionAllowed)
            {
                translation = GetTranslation();
                GetComponent<NetworkView>().RPC("UpdateObject", RPCMode.All, translation);
            }
            else
            {
                //Debug.Log(targetObject.transform.position);
                SetMatrix(targetObject.transform.position);
            }
        }

        if (Network.peerType == NetworkPeerType.Client)
        {
            if (questionAllowed)
            {
                translation = GetTranslation();
                GetComponent<NetworkView>().RPC("UpdateObject", RPCMode.All, translation);
            }
            else
            {
                //Debug.Log(targetObject.transform.position);
                SetMatrix(targetObject.transform.position);
            }
        }

    }

    Vector3 GetTranslation()
    {
        //float x = (float)Convert.ToDouble(vectorSliderGUI.GetComponent<VectorSlider>().VectorsButtons[0].GetComponentInChildren<Text>().text);
        //float y = (float)Convert.ToDouble(vectorSliderGUI.GetComponent<VectorSlider>().VectorsButtons[1].GetComponentInChildren<Text>().text);
        //float z = (float)Convert.ToDouble(vectorSliderGUI.GetComponent<VectorSlider>().VectorsButtons[2].GetComponentInChildren<Text>().text);

        //return new Vector3(x, y, z);
        return new Vector3(0, 0, 0);
    }

    void SetMatrix(Vector3 newPosition)
    {
        //vectorSliderGUI.GetComponent<VectorSlider>().matrixResultButtons[0].GetComponentInChildren<Text>().text = newPosition.x.ToString();
        //vectorSliderGUI.GetComponent<VectorSlider>().matrixResultButtons[1].GetComponentInChildren<Text>().text = newPosition.y.ToString();
        //vectorSliderGUI.GetComponent<VectorSlider>().matrixResultButtons[2].GetComponentInChildren<Text>().text = newPosition.z.ToString();

        float matrixB_0 = (float)Convert.ToDouble(vectorSliderGUI.GetComponent<VectorSlider>().VectorsButtons[3].GetComponentInChildren<Text>().text);
        float matrixB_1 = (float)Convert.ToDouble(vectorSliderGUI.GetComponent<VectorSlider>().VectorsButtons[4].GetComponentInChildren<Text>().text);
        float matrixB_2 = (float)Convert.ToDouble(vectorSliderGUI.GetComponent<VectorSlider>().VectorsButtons[5].GetComponentInChildren<Text>().text);
        vectorSliderGUI.GetComponent<VectorSlider>().VectorsButtons[0].GetComponentInChildren<Text>().text = (newPosition.x - matrixB_0).ToString();
        vectorSliderGUI.GetComponent<VectorSlider>().VectorsButtons[1].GetComponentInChildren<Text>().text = (newPosition.y - matrixB_1).ToString();
        vectorSliderGUI.GetComponent<VectorSlider>().VectorsButtons[2].GetComponentInChildren<Text>().text = (newPosition.z - matrixB_2).ToString();
    }

    [RPC]
    void UpdateObject(Vector3 targetTranslation)
    {
        targetObject.transform.position = targetTranslation;
    }

    void OnGUI()
    {
        if (Network.peerType == NetworkPeerType.Disconnected)
        {
            if (GUI.Button(new Rect(100, 100, 200, 50), "Start Client"))
            {
                Network.Connect(IP, Port);
            }
            if (GUI.Button(new Rect(100, 150, 200, 50), "Start Server"))
            {
                Network.InitializeServer(10, Port);
            }
        }
        else {
            if (Network.peerType == NetworkPeerType.Client)
            {
                if (!questionAllowed)
                {
                    GUI.Label(new Rect(100, 100, 100, 25), "Client");

                    if (GUI.Button(new Rect(100, 125, 220, 50), "Question!"))
                    {
                        GetComponent<NetworkView>().RPC("Questions", RPCMode.All);
                    }

                    if (GUI.Button(new Rect(100, 175, 220, 50), "Logout"))
                    {
                        Network.Disconnect(250);
                    }
                }
                else
                {
                    vectorSliderGUI.GetComponent<Canvas>().enabled = true;
                }
            }

            if (Network.peerType == NetworkPeerType.Server)
            {
                GUI.Label(new Rect(100, Screen.height - 300, 100, 25), "Server");
                GUI.Label(new Rect(100, Screen.height - 200, 100, 25), "Connections: " + Network.connections.Length);

                if (GUI.Button(new Rect(100, Screen.height - 100, 200, 50), "Logout"))
                {
                    Network.Disconnect(250);
                }

                if (questionFlag)
                {
                    if (GUI.Button(new Rect(Screen.width / 2 - 110, Screen.height / 2 - 25, 220, 50), "Please!"))
                    {
                        GetComponent<NetworkView>().RPC("QuestionAllowed", RPCMode.All);
                    }
                }

                if (questionAllowed)
                {
                    if (GUI.Button(new Rect(Screen.width / 2 - 110, Screen.height / 2 - 25, 220, 50), "Finished!"))
                    {
                        GetComponent<NetworkView>().RPC("QuestionFinished", RPCMode.All);
                    }
                }

                vectorSliderGUI.GetComponent<Canvas>().enabled = true;
            }
        }
    }

    [RPC]
    void Questions()
    {
        questionFlag = true;
    }

    [RPC]
    void QuestionAllowed()
    {
        origPosition = GetTranslation();
        questionFlag = false;
        questionAllowed = true;
    }

    [RPC]
    void QuestionFinished()
    {
        questionAllowed = false;
        SetMatrix(origPosition);
    }
}
