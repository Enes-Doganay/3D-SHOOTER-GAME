using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoil : MonoBehaviour
{
    public float minX, maxX, minY, maxY;
    public Transform camera;
    Vector3 rotation;

    public void RecoilSystem()
    {
        float recoilX = Random.Range(minX, maxX);
        float recoilY = Random.Range(minY, maxY);
        camera.transform.localRotation = Quaternion.Euler(rotation.x - recoilY, rotation.y + recoilX, rotation.z);
    }
}
