using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockController : MonoBehaviour, ISwitchable
{

    public TextMesh TextMesh;
    private float timer = 0.0f;
    private float timerCode = 0.0f;
    private int seconds;
    private int minutes = 59;
    private bool code = false;
    private NetworkView nv;
    public bool end = false;



    // Use this for initialization
    void Start()
    {
        nv = GetComponent<NetworkView>();
        TextMesh.text = "60:00";
    }

    // Update is called once per frame
    void Update()
    {
        if (Lzwp.sync.isMaster)
        {
            timer += Time.deltaTime;

            if (minutes < 0)
            {
                if (code && !end)
                {
                    timerCode += Time.deltaTime;

                    if (timerCode > 5)
                    {
                        timerCode = 0;
                        code = false;
                    }
                }
                else SetText("00:00");
            }
            else if (code && !end)
            {
                timerCode += Time.deltaTime;

                if (timerCode > 5)
                {
                    timerCode = 0;
                    code = false;
                }
            }
            else if (!end)
            {
                changeTime();
            }

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

        if (seconds < 10 && minutes < 10)
        {
            SetText("0" + minutes.ToString() + ":0" + seconds.ToString());
        }
        else if (seconds < 10)
        {
            SetText(minutes.ToString() + ":0" + seconds.ToString());
        }
        else if (minutes < 10)
        {
            SetText("0" + minutes.ToString() + ":" + seconds.ToString());
        }
        else
        {
            SetText(minutes.ToString() + ":" + seconds.ToString());
        }
    }

    public void showCode(string text)
    {
        if (!code) code = true;
        SetText(text);
    }

    private void SetText(string text)
    {
        TextMesh.text = text;
        nv.RPC("SetTextRPC", RPCMode.OthersBuffered, text);
    }
    [RPC]
    private void SetTextRPC(string text)
    {
        TextMesh.text = text;
    }

    public void Switch()
    {
        showCode("95:00");
    }


}
