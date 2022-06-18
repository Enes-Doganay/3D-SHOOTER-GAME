using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Weapon/properties")]
public class Weapon : ScriptableObject
{
    public int id;
    public Vector3 rHandPos;
    public Vector3 rHandRot;

    public Vector3 lHandPos;
    public Vector3 lHandRot;

    public Vector3 rHandHint;
    public Vector3 lHandHint;

    //public int damage = Random.Range(1, 20);

    public GameObject weaponPrefab;

    public ParticleSystem muzzleFlash;
    public AudioClip fireSound;
    public AudioClip reloadSound;

    public GameObject rayPoint;

    public float fireRate;
    public float reloadRate;
    public float damage;
    public GameObject impactEffect;
    public GameObject bloodEffect;


    public int AmmoInGun;
    public int AmmoInPocket;
    public int AmmoMax;
}
