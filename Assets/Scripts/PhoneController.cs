using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneController : Grabable
{
    public Material[] materials;
    public MeshRenderer screenMesh;
    private NetworkView nv;


    // Use this for initialization
    void Start()
    {
        base.Start();
        nv = GetComponent<NetworkView>();
        ChangeMaterial(0);
    }


    public override void Grab(GameObject grabber)
    {
        base.Grab(grabber);
        ChangeMaterial(1);
    }

    public override void Release()
    {
        base.Release();
        ChangeMaterial(0);
    }

    public void ChangeMaterial(int index)
    {
        screenMesh.material = materials[index];
        nv.RPC("ChangeMaterialRPC", RPCMode.OthersBuffered, index);
    }

    [RPC]
    public void ChangeMaterialRPC(int index)
    {
        screenMesh.material = materials[index];
    }
}
