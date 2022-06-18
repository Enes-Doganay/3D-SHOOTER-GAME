using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInventory : MonoBehaviour
{
    public Weapon firstWeapon;
    public Weapon secondWeapon;

    [SerializeField] GameObject inventory;

    private void Start()
    {
        HideMouseCursor();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventory.SetActive(!inventory.activeSelf);
            if (inventory.activeSelf)
            {
                ShowMouseCursor();
            }
            else
            {
                HideMouseCursor();
            }
        }
    }
    public void ShowMouseCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void HideMouseCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
