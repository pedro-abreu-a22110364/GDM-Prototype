using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField]
    private GameObject foreignObject; // Settable in the Unity Editor

    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Player") || other.CompareTag("Moveable_Object")) && foreignObject != null)
        {
            // Try to call Interact() on the foreign object
            var interactable = foreignObject.GetComponent<MonoBehaviour>();
            if (interactable != null)
            {
                var method = interactable.GetType().GetMethod("Interact");
                if (method != null)
                {
                    method.Invoke(interactable, null);
                }
            }
        }
    }
}