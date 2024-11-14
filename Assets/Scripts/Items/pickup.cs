using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup : MonoBehaviour
{
    [SerializeField] Transform itemHoldPoint;
    [SerializeField] private GameObject heldItem;
    [SerializeField] float pickupRange = 3f;
    [SerializeField] LayerMask pickupLayer;

    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = new PlayerInput();
        playerInput.Standing.Pickup.performed += context => HandlePickup();
    }

    private void OnEnable() => playerInput.Enable();
    private void OnDisable() => playerInput.Disable();

    private void HandlePickup()
    {
        if (heldItem)
        {
            DropItem();
        }
        else
        {
            TryPickupItem();
        }
    }

    private void TryPickupItem()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, pickupRange, pickupLayer))
        {
            GameObject item = hit.collider.gameObject;

            if (item != null)
            {
                heldItem = item;
                AttachItem();
            }
        }
    }

    private void AttachItem()
    {
        Rigidbody itemRigidbody = heldItem.GetComponent<Rigidbody>();
        if (itemRigidbody != null)
            itemRigidbody.isKinematic = true;

        heldItem.transform.SetParent(itemHoldPoint);
        heldItem.transform.localPosition = Vector3.zero; // Align position
        heldItem.transform.localRotation = Quaternion.identity; // Align rotation
    }

    private void DropItem()
    {
        if (heldItem == null) return;

        Rigidbody itemRigidbody = heldItem.GetComponent<Rigidbody>();
        if (itemRigidbody != null)
            itemRigidbody.isKinematic = false;

        heldItem.transform.SetParent(null);
        heldItem = null;
    }
}
