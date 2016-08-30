using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {
	
	public string IP = "127.0.0.1";
	public int Port = 25001;
	
	public GameObject target;
    public GameObject bullet;

    private float _sensitivity;
    private Vector3 _mouseReference;
    private Vector3 _mouseOffset;
    private Vector3 _rotation;
    private float _rotX;
    private float _rotY;
    private float _rotZ;
    private bool _isRotating;
    private bool _rotatingX = false;

    void Start()
    {
        _sensitivity = 0.4f;
        _rotation = Vector3.zero;
    }

    void Update()
    {
        if (Network.peerType == NetworkPeerType.Client)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                GetComponent<NetworkView>().RPC("Fire", RPCMode.All);
            }

            if (Input.GetKey(KeyCode.W))
            {
                this.transform.Translate(0, 0, -0.2f);
            }

            if (Input.GetMouseButtonDown(0))
                OnMouseDown();

            if (Input.GetMouseButtonUp(0))
                OnMouseUp();

            if (_isRotating && !_rotatingX)
            {
                // offset
                _mouseOffset = (Input.mousePosition - _mouseReference);

                // apply rotation
                //_rotation.y = -(_mouseOffset.x + _mouseOffset.y) * _sensitivity;
                _rotX = _mouseOffset.x * _sensitivity;
                _rotY = -_mouseOffset.y * _sensitivity;

                // rotate
                //transform.Rotate(_rotation);
                transform.RotateAround(target.transform.position, Vector3.right, _rotX);
                transform.RotateAround(target.transform.position, Vector3.up, _rotY);

                // store mouse
                _mouseReference = Input.mousePosition;
            }
            else if (_isRotating && _rotatingX)
            {

            }
        }
    }

    void OnMouseDown()
    {
        // rotating flag
        _isRotating = true;

        // store mouse
        _mouseReference = Input.mousePosition;
    }

    void OnMouseUp()
    {
        // rotating flag
        _isRotating = false;
    }

    void OnGUI()
	{
		if(Network.peerType == NetworkPeerType.Disconnected)
		{
			if(GUI.Button(new Rect(100,100,100,25),"Start Client"))
			{
				Network.Connect(IP,Port);
			}
			if(GUI.Button(new Rect(100,125,100,25),"Start Server"))
			{
				Network.InitializeServer(10,Port);
			}
		}
		else {
            if (Network.peerType == NetworkPeerType.Client)
            {
                GUI.Label(new Rect(100, 100, 100, 25), "Client");

                if (GUI.Button(new Rect(100, 125, 110, 25), "Change Color"))
                {
                    GetComponent<NetworkView>().RPC("ChangeColor", RPCMode.All);
                }

                if (GUI.Button(new Rect(100, 150, 110, 25), "Logout"))
                {
                    Network.Disconnect(250);
                }
            }
				
			if(Network.peerType == NetworkPeerType.Server)
			{
				GUI.Label(new Rect(100,100,100,25),"Server");
				GUI.Label(new Rect(100,125,100,25),"Connections: " + Network.connections.Length);

                if (GUI.Button(new Rect(100, 150, 110, 25), "Change Size"))
                {
                    GetComponent<NetworkView>().RPC("DoubleSize", RPCMode.All);
                }

                if (GUI.Button(new Rect(100,175,100,25),"Logout"))
				{
					Network.Disconnect(250);	
				}
			}
		}
	}
	
    [RPC]
    void ChangeColor()
    {
        target.GetComponent<Renderer>().material.color = Color.green;
    }

    [RPC]
    void DoubleSize()
    {
        target.transform.localScale = (target.transform.localScale * 2);
    }

    [RPC]
    void Fire()
    {
        GameObject instantiatedProjectile = Instantiate(bullet,
        new Vector3(transform.position.x, transform.position.y + 1, transform.position.z + 1),
        transform.rotation) as GameObject;
        instantiatedProjectile.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * 6f);
        Destroy(instantiatedProjectile, 2f);
    }
}
