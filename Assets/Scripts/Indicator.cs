using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;


public class Indicator : MonoBehaviour {

	public GameObject tube;
	public Material material;
	private MeshRenderer rend;
	private NetworkView nv;


	// Use this for initialization
	void Start () {
		nv = GetComponent<NetworkView>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject == tube)
		{
			rend = tube.GetComponent<MeshRenderer>();
			ChangeMaterial(rend);
		}
	}

	public void ChangeMaterial(Renderer rend)
	{
		nv.RPC("ChangeMaterialRPC", RPCMode.OthersBuffered, rend);
		rend.material = material;
	}

    [RPC]
    public void ChangeMaterialRPC(Renderer rend)
    {
        rend.material = material;
    }
}
