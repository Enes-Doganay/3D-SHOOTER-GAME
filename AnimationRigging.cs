using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class AnimationRigging : MonoBehaviour
{
    public Rig aimPoseLayer;
    public Rig weaponPoseLayer;
    public Rig handLayer;
    public Rig handLayerPistol;

    public float aimDuration = 0.1f;

    public bool inGun = false;
    public bool inPistol = false;

    public bool beginStrafing = false;

    public GameObject handWeapon;
    public GameObject backWeapon;
    public GameObject gunPivot;

    public GameObject right_Hand;

    public GameObject right_Hint;
    public GameObject rightHintAimRef;
    public GameObject rightHintInArmsRef;

    public GameObject bulletStats;

    bool weightUp = false;
    Player playerMovement;

    public WeaponRecoil weaponRecoil;

    Animator animator;

    public GameObject Aim;
    ActiveWeapon activeWeapon;
    EquippableItem equippableItem;

    public GameObject denemePistolRightHand;

    private void Start()
    {
        playerMovement = Object.FindObjectOfType<Player>();
        activeWeapon = Object.FindObjectOfType<ActiveWeapon>();
        animator = GetComponent<Animator>();
        bulletStats.SetActive(inGun);
        equippableItem=Object.FindObjectOfType<EquippableItem>();
    }

    private void Update()
    {
        /*if (weightUp == true)
        {
            weaponPoseLayer.weight += Time.deltaTime / aimDuration;
            handLayer.weight += Time.deltaTime / aimDuration;
        }
        else
        {
            weaponPoseLayer.weight -= Time.deltaTime / aimDuration;
            handLayer.weight -= Time.deltaTime / aimDuration;
        }*/
    }
    public void WeightUpTrue()
    {
        activeWeapon.weightUp = true;
    }

    public void WeightUpFalse()
    {
        activeWeapon.weightUp = false;
    }
}
