using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{

    private Camera cam;
    [SerializeField]
    private float distance = 3f;
    [SerializeField]
    private LayerMask layerMask;
    
    void Start()
    {
        cam = GetComponent<PlayerLook>().cam;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new(cam.transform.position, cam.transform.forward);
        // Physics.Raycast(ray, out RaycastHit hit, distance);

        // Debug.Log(ray);
        // Debug.DrawRay(ray.origin, ray.direction * distance);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit,distance,layerMask)){
            //get interactable in a variable:
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            // Debug.Log("hit");
            // Debug.Log(hit.collider.name);
            // Debug.Log(interactable);
            if(interactable != null){
                Debug.Log(interactable.promptMessage);
            }
        }
    }
}