using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public string sceneToLoad; // Set this in the Inspector for each door
    public float interactionRange = 2f; // How close the player must be

    [SerializeField]
    private bool isInteractible = true; // Can be set in the Inspector

    private Transform playerTransform;

    void Start()
    {
        // Find the player by tag at start
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            playerTransform = playerObj.transform;
        }
    }

    void Update()
    {
        if (playerTransform != null && isInteractible)
        {
            float distance = Vector3.Distance(transform.position, playerTransform.position);

            if (distance <= interactionRange && Input.GetKeyDown(KeyCode.E))
            {
                if (!string.IsNullOrEmpty(sceneToLoad))
                {
                    SceneManager.LoadScene(sceneToLoad);
                }
            }
        }
    }

    // Setter for isInteractible
    public void SetInteractible(bool value)
    {
        isInteractible = value;
    }

    public void Interact(bool value)
    {
        SetInteractible(true);
    }

}