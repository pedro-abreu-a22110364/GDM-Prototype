using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UponInteractDespawnObject : MonoBehaviour
{
    [SerializeField] private GameObject objectToDespawn; // Assign the object to despawn in the Inspector
    [SerializeField] private float interactRange = 2f;   // Range to interact

    void Update()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            if (distance <= interactRange && Input.GetKeyDown(KeyCode.E))
            {
                if (objectToDespawn != null)
                {
                    objectToDespawn.SetActive(false);
                }
                break; // Only despawn once per key press and player in range
            }
        }
    }
}