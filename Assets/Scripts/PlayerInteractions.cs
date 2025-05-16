using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    public float checkRadius = 1.5f; // Radius to check for collectibles
    public List<string> inventory = new List<string>(); // Player's inventory

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryCollectNearby();
        }
    }

    private void TryCollectNearby()
    {
        // Check for nearby objects with tag "Collectible"
        Collider[] colliders = Physics.OverlapSphere(transform.position, checkRadius);
        foreach (Collider col in colliders)
        {
            if (col.CompareTag("Collectible"))
            {
                var collectible = col.GetComponent<MonoBehaviour>();
                if (collectible != null)
                {
                    var method = collectible.GetType().GetMethod("PlayerInteract");
                    if (method != null)
                    {
                        object result = method.Invoke(collectible, null);
                        if (result is string str)
                        {
                            inventory.Add(str);
                            Debug.Log("Added to inventory: " + str);
                        }
                    }
                }
                break; // Only interact with the first collectible found
            }
        }
    }
}