using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintController : MonoBehaviour, IInteractable
{
    public Material[] materials;
    private Renderer rend;
    private float timer = 0.0f;
    private bool hintOnScreen = false;
    private NetworkView nv;


    void Start()
    {
        rend = GetComponent<MeshRenderer>();
        rend.material = materials[0];
        nv = GetComponent<NetworkView>();
    }

    // Update is called once per frame
    void Update()
    {
        if(hintOnScreen)
        {
            timer += Time.deltaTime;
        }

        if (timer > 30.0f) 
        {
            ChangeMaterial(0);
            hintOnScreen = false;
            timer = 0;
        }
    }

    public bool Interact()
    {
        int rand = Random.Range(1, 7);
        ChangeMaterial(rand);
        hintOnScreen = true;
        timer = 0.0f;
        return false;
    }

    public void ChangeMaterial(int index)
    {
        nv.RPC("ChangeMaterialRPC", RPCMode.OthersBuffered, index);
        rend.material = materials[index];
    }

    [RPC]
    public void ChangeMaterialRPC(int index)
    {
        rend.material = materials[index];
    }


}
