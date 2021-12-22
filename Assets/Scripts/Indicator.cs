using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;


public class Indicator : Grabable {

	private Color[] _colors = new Color[15];
	private NetworkView nv;
	private Renderer _renderer;


	// Use this for initialization
	void Start ()
	{
		base.Start();
		_renderer = GetComponent<MeshRenderer>();
		nv = GetComponent<NetworkView>();
		_colors[0] = ColorFromIntRGB(235, 1, 64);
		_colors[1] = ColorFromIntRGB(239, 83, 42);
		_colors[2] = ColorFromIntRGB(247, 167, 20);
		_colors[3] = ColorFromIntRGB(254, 239, 0);
		_colors[4] = ColorFromIntRGB(190, 229, 1);
		_colors[5] = ColorFromIntRGB(127, 206, 0);
		_colors[6] = ColorFromIntRGB(63, 183, 0);
		_colors[7] = ColorFromIntRGB(0, 159, 1);
		_colors[8] = ColorFromIntRGB(0, 174, 95);
		_colors[9] = ColorFromIntRGB(1, 189, 190);
		_colors[10] = ColorFromIntRGB(1, 133, 205);
		_colors[11] = ColorFromIntRGB(0, 80, 221);
		_colors[12] = ColorFromIntRGB(43, 63, 212);
		_colors[13] = ColorFromIntRGB(85, 49, 201);
		_colors[14] = ColorFromIntRGB(71, 35, 159);
	}


	private void OnCollisionEnter(Collision other)
	{
		var probe = other.gameObject.GetComponent<TestedProbe>();
		if (probe != null)
		{
			ChangeMaterial(probe.phValue);
		}
	}

	public void ChangeMaterial(int ph)
	{
		nv.RPC("ChangeMaterialRPC", RPCMode.OthersBuffered, ph);
		_renderer.material.color = _colors[ph];
	}

    [RPC]
    public void ChangeMaterialRPC(int ph)
    {
	    _renderer.material.color = _colors[ph];
    }
    private Color ColorFromIntRGB(int r, int g, int b)
    {
	    return new Color((float) r / 255, (float) g / 255, (float) b / 255);
    }
}
