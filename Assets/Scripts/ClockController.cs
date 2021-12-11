using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockController : MonoBehaviour {

    public TextMesh TextMesh;
    private float timer = 0.0f;
    private float timerCode = 0.0f;
    private int seconds;
    private int minutes = 59;
    private bool code = false;



    // Use this for initialization
    void Start () {
        TextMesh.text = "60:00";
	}
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime;

        if (code)
        {
            timerCode += Time.deltaTime;

            showCode();
            if(timerCode>5)
            {
                timerCode = 0;
                code = false;
            }
        }
        else
        {
            changeTime();

        }


    }

    public void changeTime()
    {
        seconds = 59 - (int)timer;

        if (timer > 60)
        {
            timer = 0;
            minutes -= 1;
        }

        if (seconds < 10)
        {
            TextMesh.text = minutes.ToString() + ":0" + seconds.ToString();
        }
        else
        {
            TextMesh.text = minutes.ToString() + ":" + seconds.ToString();
        }
    }

    public void showCode()
    {
        if (!code) code = true;
        TextMesh.text = "21:37";

    }
}
