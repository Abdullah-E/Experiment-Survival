using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] Transform itemHoldPoint;
    [SerializeField] private GameObject heldItem;
    [SerializeField] float pickupRange = 3f;
    [SerializeField] LayerMask pickupLayer;
    [SerializeField] public Item Item;

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

            // Check if the item has an ItemController component
            ItemController itemController = item.GetComponent<ItemController>();
            if (itemController != null)
            {
                // Add the item to the InventoryManager
                InventoryManager.Instance.Add(itemController.Item);
                InventoryManager.Instance.Add(Item);

                // Deactivate the item in the scene
                item.SetActive(false);

                Debug.Log($"Picked up {itemController.Item.name} and added to inventory.");
            }
            else
            {
                Debug.LogWarning("The object does not have an ItemController component!");
            }
        }
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