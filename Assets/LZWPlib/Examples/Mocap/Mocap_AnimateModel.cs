using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mocap_AnimateModel : MonoBehaviour
{
    [Tooltip("Assign based on avatar")]
    public bool autoAssign = true;
    [Tooltip("Transform component of each bone")]
    public Transform[] modelBoneTransforms;

    Animator animator;
    Avatar avatar;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component missing!");
            return;
        }

        avatar = animator.avatar;
        if (avatar == null)
        {
            Debug.LogError("Avatar missing!");
            return;
        }

        if (!avatar.isHuman)
        {
            Debug.LogError("Avatar is not humanoidal!");
            return;
        }

        if (autoAssign)
        {
            modelBoneTransforms = new Transform[21];
            modelBoneTransforms[0] = animator.GetBoneTransform(HumanBodyBones.Hips);
            modelBoneTransforms[1] = animator.GetBoneTransform(HumanBodyBones.Spine);
            modelBoneTransforms[2] = animator.GetBoneTransform(HumanBodyBones.Chest);
            modelBoneTransforms[3] = null;
            modelBoneTransforms[4] = animator.GetBoneTransform(HumanBodyBones.UpperChest);
            modelBoneTransforms[5] = animator.GetBoneTransform(HumanBodyBones.Neck);
            modelBoneTransforms[6] = animator.GetBoneTransform(HumanBodyBones.Head);
            modelBoneTransforms[7] = animator.GetBoneTransform(HumanBodyBones.LeftShoulder);
            modelBoneTransforms[8] = animator.GetBoneTransform(HumanBodyBones.LeftUpperArm);
            modelBoneTransforms[9] = animator.GetBoneTransform(HumanBodyBones.LeftLowerArm);
            modelBoneTransforms[10] = animator.GetBoneTransform(HumanBodyBones.LeftHand);
            modelBoneTransforms[11] = animator.GetBoneTransform(HumanBodyBones.RightShoulder);
            modelBoneTransforms[12] = animator.GetBoneTransform(HumanBodyBones.RightUpperArm);
            modelBoneTransforms[13] = animator.GetBoneTransform(HumanBodyBones.RightLowerArm);
            modelBoneTransforms[14] = animator.GetBoneTransform(HumanBodyBones.RightHand);
            modelBoneTransforms[15] = animator.GetBoneTransform(HumanBodyBones.LeftUpperLeg);
            modelBoneTransforms[16] = animator.GetBoneTransform(HumanBodyBones.LeftLowerLeg);
            modelBoneTransforms[17] = animator.GetBoneTransform(HumanBodyBones.LeftFoot);
            modelBoneTransforms[18] = animator.GetBoneTransform(HumanBodyBones.RightUpperLeg);
            modelBoneTransforms[19] = animator.GetBoneTransform(HumanBodyBones.RightLowerLeg);
            modelBoneTransforms[20] = animator.GetBoneTransform(HumanBodyBones.RightFoot);
        }
    }

    void Update()
    {
        if (!Lzwp.initialized || Lzwp.input.humans.Count < 1)
            return;

        LzwpInput.Human h = Lzwp.input.humans[0];

        for (int i = 0; i < h.joints.Length && i < modelBoneTransforms.Length; i++)
        {
            if (h.joints[i].tracked)
            {
                if (modelBoneTransforms[i] == null)
                    continue;

                if (i == 0)  // hips/pelvis
                    modelBoneTransforms[i].position = h.joints[0].position;

                modelBoneTransforms[i].rotation = h.joints[i].rotation;
            }
        }
    }

    void OnGUI()
    {
        GUI.Box(new Rect(5f, 5f, 150f, 50f), string.Format("Humans: {0}", Lzwp.input.humans.Count));
    }
}
