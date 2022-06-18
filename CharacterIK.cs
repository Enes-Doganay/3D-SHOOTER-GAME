using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterIK : MonoBehaviour
{
    public Animator anim;
    CharacterInventory characterInventory;
    Player playerMovement;

    public Transform targetLook;

    public Transform lHand;
    public Transform lHandTarget;
    public Transform rHand;

    public Quaternion lhRot;

    public float rh_Weight;

    public Transform shoulder;
    public Transform aimPivot;
    private void Start()
    {
        characterInventory = Object.FindObjectOfType<CharacterInventory>();
        playerMovement = Object.FindObjectOfType<Player>();

        shoulder = anim.GetBoneTransform(HumanBodyBones.RightShoulder).transform;

        aimPivot = new GameObject().transform;
        aimPivot.name = "aim pivot";
        aimPivot.transform.parent = transform;

        rHand = new GameObject().transform;
        rHand.name = "right hand";
        rHand.transform.parent = aimPivot;

        lHand = new GameObject().transform;
        lHand.name = "left hand";
        lHand.transform.parent = aimPivot;

        rHand.localPosition = characterInventory.firstWeapon.rHandPos;
        Quaternion rotRight = Quaternion.Euler(characterInventory.firstWeapon.rHandRot.x, characterInventory.firstWeapon.rHandRot.y, characterInventory.firstWeapon.rHandRot.z);
        rHand.localRotation = rotRight;

    }
    private void Update()
    {
        lhRot = lHand.rotation;
        lHand.position = lHandTarget.position;
        lHand.rotation = lhRot;

        if (playerMovement.movementType == Player.MovementType.BeginStrafe)
            rh_Weight += Time.deltaTime * 2;
        else
            rh_Weight -= Time.deltaTime * 2;
        rh_Weight = Mathf.Clamp(rh_Weight, 0, 1);
    }
    private void OnAnimatorIK(int layerIndex)
    {
        aimPivot.position = shoulder.position;
        if(playerMovement.movementType == Player.MovementType.BeginStrafe)
        {
            anim.SetLookAtWeight(1f, 0f, 1f);

            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
            anim.SetIKPosition(AvatarIKGoal.LeftHand, lHand.position);
            anim.SetIKRotation(AvatarIKGoal.LeftHand, lhRot);

            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
            anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
            anim.SetIKPosition(AvatarIKGoal.RightHand, rHand.position);
            anim.SetIKRotation(AvatarIKGoal.RightHand, rHand.rotation);
        }
        else
        {
            anim.SetLookAtWeight(.3f, .3f, .3f);
            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
            anim.SetIKPosition(AvatarIKGoal.LeftHand, lHand.position);
            anim.SetIKRotation(AvatarIKGoal.LeftHand, lhRot);

            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
            anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
            anim.SetIKPosition(AvatarIKGoal.RightHand, rHand.position);
            anim.SetIKRotation(AvatarIKGoal.RightHand, rHand.rotation);
        }
    }
}
