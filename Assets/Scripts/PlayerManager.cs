using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public CameraFollow cameraFollow; // Reference to your CameraFollow script

    private GameObject activePlayer;

    void Start()
    {
        SetActivePlayer(player1);
    }

    void Update()
    {
        // Only allow switch if the active player is grounded
        PortalMovement activeMovement = activePlayer.GetComponent<PortalMovement>();
        if (Input.GetKeyDown(KeyCode.P) && activeMovement != null && activeMovement.isGrounded)
        {
            if (activePlayer == player1)
                SetActivePlayer(player2);
            else
                SetActivePlayer(player1);
        }
    }

    void SetActivePlayer(GameObject player)
    {
        activePlayer = player;

        // Enable input on the active player, disable on the other
        player1.GetComponent<PortalMovement>().enabled = (player == player1);
        player2.GetComponent<PortalMovement>().enabled = (player == player2);

        // Set Rigidbody kinematic state: inactive player is kinematic, active is not
        Rigidbody rb1 = player1.GetComponent<Rigidbody>();
        Rigidbody rb2 = player2.GetComponent<Rigidbody>();
        if (rb1 != null) rb1.isKinematic = (player != player1);
        if (rb2 != null) rb2.isKinematic = (player != player2);

        // Update camera follow target
        cameraFollow.player = activePlayer.transform;
    }
}