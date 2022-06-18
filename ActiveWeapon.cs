using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ActiveWeapon : MonoBehaviour
{

    public EquippableItem primaryWeapon;
    public EquippableItem secondaryWeapon;

    public GameObject[] BackWeapon;
    public GameObject[] HandWeapon;

    public GameObject[] BackPistol;
    public GameObject[] HandPistol;

    public EquippableItem activeWeapon;

    public GameObject handWeapon;
    public GameObject backWeapon;

    AnimationRigging animationRigging;
    Animator animator;

    public bool inGun = false;
    public bool inPistol = false;

    public bool weightUp = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animationRigging = Object.FindObjectOfType<AnimationRigging>();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1) && primaryWeapon != null)
        {
            if (!inPistol)
            {
                inGun = !inGun;
                animator.SetBool("inGun", inGun);
            }
            else
            {
                inPistol = !inGun;
                animator.SetBool("inPistol", inPistol);
                inGun = !inGun;
                animator.SetBool("inGun", inGun);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && secondaryWeapon != null)
        {
            if (!inGun)
            {
                inPistol = !inPistol;
                animator.SetBool("inPistol", inPistol);
            }
            else
            {
                inGun = !inGun;
                animator.SetBool("inGun", inGun);
                inPistol = !inGun;
                animator.SetBool("inPistol", inPistol);
            }
        }

        if (weightUp == true)
        {
            animationRigging.weaponPoseLayer.weight += Time.deltaTime / animationRigging.aimDuration;
            if (activeWeapon != null && activeWeapon.equipmentType == EquipmentType.Gun)
                animationRigging.handLayer.weight += Time.deltaTime / animationRigging.aimDuration;
            else if (activeWeapon !=null && activeWeapon.equipmentType == EquipmentType.Pistol)
                animationRigging.handLayerPistol.weight += Time.deltaTime / animationRigging.aimDuration;
        }
        else
        {
            animationRigging.weaponPoseLayer.weight -= Time.deltaTime / animationRigging.aimDuration;
            animationRigging.handLayer.weight -= Time.deltaTime / animationRigging.aimDuration;
            animationRigging.handLayerPistol.weight -= Time.deltaTime / animationRigging.aimDuration;
        }
    }

    public void EquipWeaponPrimary()
    {
        activeWeapon = primaryWeapon;

        animationRigging.right_Hand.transform.localPosition = activeWeapon.rHandPos;
        animationRigging.right_Hand.transform.localEulerAngles = activeWeapon.rHandRot;

        backWeapon = BackWeapon[activeWeapon.id];
        handWeapon = HandWeapon[activeWeapon.id];

        handWeapon.SetActive(true);
        backWeapon.SetActive(false);
    }
    public void EquipWeaponSecondary()
    {
        activeWeapon = secondaryWeapon;

        animationRigging.right_Hand.transform.localPosition = activeWeapon.rHandPos;
        animationRigging.right_Hand.transform.localEulerAngles = activeWeapon.rHandRot;

        backWeapon = BackPistol[activeWeapon.id];
        handWeapon = HandPistol[activeWeapon.id];

        handWeapon.SetActive(true);
        backWeapon.SetActive(false);
    }

    public void UnequipWeapon()
    {
        activeWeapon = null;
        handWeapon.SetActive(false);
        backWeapon.SetActive(true);
    }
}
