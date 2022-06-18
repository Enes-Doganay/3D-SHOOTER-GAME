using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class WeaponRecoil : MonoBehaviour
{
    [HideInInspector] public PlayerAiming playerAiming;
    [HideInInspector] public CinemachineImpulseSource cameraShake;

    public float verticalRecoil;
    public float horizontalRecoil;
    public float duration;
    public float recoilModifier = 1f;

    float time = 0;

    Animator animator;

    private void Awake()
    {
        cameraShake = GetComponent<CinemachineImpulseSource>();
        playerAiming = Object.FindObjectOfType<PlayerAiming>();
        animator = GetComponent<Animator>();
    }
    public void GenerateRecoil()
    {
        time = duration;
        cameraShake.GenerateImpulse(Camera.main.transform.forward);

        verticalRecoil = Random.Range(-10, 10);

        animator.Play("weapon_recoil_rifle");
    }

    private void Update()
    {
        if (time > 0)
        {
            playerAiming.yAxis.Value -= (((verticalRecoil / 10) * Time.deltaTime) / duration) * recoilModifier;
            playerAiming.xAxis.Value -= (((horizontalRecoil / 10) * Time.deltaTime) / duration) * recoilModifier;
            time -= Time.deltaTime;
        }
    }
}
