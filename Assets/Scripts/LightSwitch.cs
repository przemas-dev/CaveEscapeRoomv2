using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour, IInteractable
{

    public Material[] materials;
    private Renderer rend;
    public GameObject light;
    public GameObject plane;
    public GameObject switch_up;
    public GameObject switch_down;
    public AudioClip ClickSound;
    private AudioSource _audioSource;


    private bool isOn = true;

	// Use this for initialization
	void Start ()
    {
        rend = plane.gameObject.GetComponent<MeshRenderer>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {
		
	}

    public bool Interact()
    {
        _audioSource.clip = ClickSound;
        _audioSource.Play();

        isOn = !isOn;
        light.gameObject.SetActive(isOn);
        if (isOn)
        {
            switch_up.gameObject.SetActive(true);
            switch_down.gameObject.SetActive(false);

            ChangeMaterial(materials[0]);
        }
        else
        {
            switch_up.gameObject.SetActive(false);
            switch_down.gameObject.SetActive(true);
            ChangeMaterial(materials[1]);
        }

        return false;
    }

    private void ChangeMaterial(Material material)
    {
        rend.material = material;
    }
}
