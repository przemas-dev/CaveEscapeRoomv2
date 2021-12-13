using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour,ISwitchable {
    public Light Light;
    public MeshRenderer LightMesh;
    public Material MaterialOn;
    public Material MaterialOff;

    private bool isEnabled = true;
    private NetworkView nv;
    public void Switch()
    {
        isEnabled = !isEnabled;
        nv.RPC("SwitchRPC", RPCMode.AllBuffered, isEnabled);
    }

    [RPC]
    private void SwitchRPC(bool isEnabled)
    {
        Light.enabled = isEnabled;
        LightMesh.material = isEnabled ? MaterialOn : MaterialOff;
    }

    // Use this for initialization
    void Start () {
        LightMesh.material = MaterialOn;
        nv = GetComponent<NetworkView>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
