using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectWithinAreaInteract : MonoBehaviour
{
    [SerializeField] private GameObject objectToRemove; // Assign the object to remove in the Inspector

    private int objectsInArea = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Moveable_Object") || other.CompareTag("Player"))
        {
            objectsInArea++;
            if (objectToRemove != null)
                objectToRemove.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Moveable_Object") || other.CompareTag("Player"))
        {
            objectsInArea--;
            if (objectsInArea <= 0 && objectToRemove != null)
                objectToRemove.SetActive(true);
        }
    }
}