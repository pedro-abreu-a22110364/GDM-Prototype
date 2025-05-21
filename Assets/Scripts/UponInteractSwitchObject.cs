using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UponInteractSwitchObject : MonoBehaviour
{
    [SerializeField] private GameObject objectToHide;   // Assign the object to hide in the Inspector
    [SerializeField] private GameObject objectToShow;   // Assign the object to show in the Inspector
    [SerializeField] private float interactRange = 2f;  // Range to interact

    private bool isShowing = false;

    void Start()
    {
        if (objectToShow != null)
            objectToShow.SetActive(false);
        if (objectToHide != null)
            objectToHide.SetActive(true);
        isShowing = false;
    }

    void Update()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            if (distance <= interactRange && Input.GetKeyDown(KeyCode.E))
            {
                isShowing = !isShowing;
                if (objectToHide != null) objectToHide.SetActive(!isShowing);
                if (objectToShow != null) objectToShow.SetActive(isShowing);
                break; // Only switch once per key press and player in range
            }
        }
    }
}