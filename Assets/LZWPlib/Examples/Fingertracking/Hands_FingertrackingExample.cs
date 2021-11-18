using UnityEngine;

public class Hands_FingertrackingExample : MonoBehaviour
{
    public Transform handL;
    public Transform handR;
    Transform[] trFingersPh = new Transform[30];

    public bool moveHands = true;
    public bool disableWhenNotTracked = false;

    void Start()
    {
        for (int i = 0; i < 5; i++)
            for (int j = 0; j < 3; j++)
            {
                trFingersPh[i * 3 + j] = handL.Find(string.Format("fin{0}ph{1}", i, j));
                trFingersPh[15 + i * 3 + j] = handR.Find(string.Format("fin{0}ph{1}", i, j));
            }
    }
    
    void Update()
    {
        if (!Lzwp.initialized)
            return;

        for (int i = 0; i < Lzwp.input.hands.Count; i++)  // for each hand
        {
            Transform tr = handR;
            if (Lzwp.input.hands[i].leftHand)
                tr = handL;

            if (Lzwp.input.hands[i].pose.tracked)
            {
                if (moveHands)
                {
                    tr.position = Lzwp.input.hands[i].pose.position;
                    tr.rotation = Lzwp.input.hands[i].pose.rotation;
                }

                for (int j = 0; j < 5; j++)  // for each finger
                {
                    LzwpInput.FingertrackingFinger f = Lzwp.input.hands[i].fingers[j];

                    for (int k = 0; k < 3; k++)  // for each phalanx
                    {
                        Transform trP = GetFingerPhalanx(Lzwp.input.hands[i].rightHand, j, k);

                        trP.localScale = new Vector3(f.phalanxLength[k], f.tipRadius * 2f, f.tipRadius * 2f);
                        trP.localPosition = f.joints[k].positionInHandCoordSystem + 
                            Vector3.left * (f.phalanxLength[k] / 2f) +  // half finger length
                            Vector3.right * 0.06f;  // half palm length
                        
                        if (k == 0)
                        {
                            trP.localRotation = f.tipPose.rotation;  // rotationLocal???
                        }
                        else if (k == 1)
                        {
                            trP.localRotation = f.tipPose.rotation * Quaternion.Euler(0f, -f.phalanxAngle[0], 0f);
                        }
                        else if (k == 2)
                        {
                            trP.localRotation = f.tipPose.rotation * Quaternion.Euler(0f, -f.phalanxAngle[0], 0f) * Quaternion.Euler(0f, -f.phalanxAngle[1], 0f);
                        }
                    }
                }
            }

            if (disableWhenNotTracked)
            {
                tr.gameObject.SetActive(Lzwp.input.hands[i].pose.tracked);
            }
        }
    }

    Transform GetFingerPhalanx(bool handRight, int fin, int ph)
    {
        return trFingersPh[(handRight ? 1 : 0) * 15 + fin * 3 + ph];
    }
}
