using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintController : MonoBehaviour, IInteractable
{
    public Material[] materials;
    private Renderer rend;
    private float timer = 0.0f;
    private bool hintOnScreen = false;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<MeshRenderer>();
        rend.enabled = true;
        rend.material = materials[0];
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
            ChangeMaterial(materials[0]);
            hintOnScreen = false;
            timer = 0;
        }
    }

    public bool Interact()
    {
        int rand = Random.Range(1, 7);
        ChangeMaterial(materials[rand]);
        hintOnScreen = true;
        timer = 0.0f;
        return false;
    }

    private void ChangeMaterial(Material material)
    {
        rend.material = material;
    }
}
