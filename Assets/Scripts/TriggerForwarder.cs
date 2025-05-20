using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Place this on the child with the trigger collider
public class TriggerForwarder : MonoBehaviour
{
    public CyclingObject cyclingObject;

    void OnTriggerEnter(Collider other)
    {
        cyclingObject.OnPlayerTriggerEnter(other);
    }
}
