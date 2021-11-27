using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneController : Grabable
{
    public Material[] materials;
    public MeshRenderer screenMesh;

    // Use this for initialization
    void Start()
    {
        base.Start();
        ChangeMaterial(materials[0]);
    }


    public override void Grab(GameObject grabber)
    {
        base.Grab(grabber);
        ChangeMaterial(materials[1]);
    }

    public override void Release()
    {
        base.Release();
        ChangeMaterial(materials[0]);
    }

    public void ChangeMaterial(Material material)
    {
        screenMesh.material = material;
    }
}
