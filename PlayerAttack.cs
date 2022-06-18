using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class PlayerAttack : FireSystem
{
    WeaponRecoil weaponRecoil;
    Player player;
    public Text ammoInGun, ammoInPacket;
    public GameObject aimLookAt;
    ActiveWeapon activeWeapon;
    AnimationRigging animationRigging;
    [SerializeField] CinemachineFreeLook cinemachine;
    protected override void Start()
    {
        activeWeapon = Object.FindObjectOfType<ActiveWeapon>();
        animationRigging = Object.FindObjectOfType<AnimationRigging>();
        weapon = activeWeapon.activeWeapon;

        ammoInGun.text = weapon.AmmoInGun.ToString();
        ammoInPacket.text = weapon.AmmoInPocket.ToString();
        base.Start();
        weaponRecoil = Object.FindObjectOfType<WeaponRecoil>();
        player = Object.FindObjectOfType<Player>();
    }
    protected override void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1) || Input.GetKey(KeyCode.Mouse0))
        {
            player.movementType = Player.MovementType.BeginStrafe;
            player.animator.SetBool("beginStrafing", true);
            cinemachine.GetComponent<Animator>().SetBool("isAiming", true);

            weaponRecoil.recoilModifier = 0.3f;

            if (activeWeapon.activeWeapon.equipmentType == EquipmentType.Gun)
            {
                animationRigging.aimPoseLayer.weight = 1;
                animationRigging.weaponPoseLayer.weight = 0;
                animationRigging.right_Hint.transform.position = animationRigging.rightHintAimRef.transform.position;
            }
        }
        else
        {
            animationRigging.aimPoseLayer.weight = 0;
            animationRigging.weaponPoseLayer.weight = 1;
            animationRigging.right_Hint.transform.position = animationRigging.rightHintInArmsRef.transform.position;

            player.movementType = Player.MovementType.inArms;
            player.animator.SetBool("beginStrafing", false);
            cinemachine.GetComponent<Animator>().SetBool("isAiming", false);
            weaponRecoil.recoilModifier = 1f;
        }
        CheckFire();
        CheckReload();
    }
    protected override void Fire()
    {
        animator.SetTrigger("recoil");
        weapon.rayPoint.transform.LookAt(aimLookAt.transform);
        weaponRecoil.GenerateRecoil();
        base.Fire();
        ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.tag == "Untagged")
            {
                GameObject impact = Instantiate(weapon.impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impact, 0.3f);
            }
            if (hit.transform.tag == "AI")
            {
                GameObject blood = Instantiate(weapon.bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
                hit.transform.GetComponentInParent<Health>().GetDamage(weapon.DamageBonus, ray.direction);
                Destroy(blood, 0.3f);
            }
        }
        ammoInGun.text = weapon.AmmoInGun.ToString();
        if (hit.transform.tag == "AI" && hit.transform.GetComponentInParent<Health>().currentHealth > 0)
            Object.FindObjectOfType<AIAgent>().stateMachine.ChangeState(AIStateID.AIAttack);
    }
    protected override void CheckReload()
    {
        base.CheckReload();
        ammoInGun.text = weapon.AmmoInGun.ToString();
        ammoInPacket.text = weapon.AmmoInPocket.ToString();
    }

}
