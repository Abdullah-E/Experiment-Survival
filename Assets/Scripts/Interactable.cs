using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public string promptMesaage;
    public int lol = 10;

    public void BaseInteract(){
        Debug.Log(promptMesaage);
        Interact();
    }
    protected virtual void Interact(){
        Debug.Log("Interacting with base class");
    }
}
