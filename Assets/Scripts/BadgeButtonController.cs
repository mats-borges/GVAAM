using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadgeButtonController : MonoBehaviour
{
    public Transform playerCamera; // Reference to the player's camera
    public Transform table; // Reference to the table GameObject
    public float rotationSpeed = 1f; // Speed of rotation

    // Method called when the button is pressed
    public void OnButtonPressed()
    {
        // Calculate the direction from the player's current position to the table's position
        Vector3 directionToTable = (table.position - playerCamera.position).normalized;
        directionToTable.y = 0f; // Make sure rotation is only around the y-axis

        // Calculate the rotation needed to look at the table
        Quaternion targetRotation = Quaternion.LookRotation(directionToTable);

        // Smoothly rotate the player's camera towards the table
        StartCoroutine(RotateCamera(targetRotation));
    }

    // Coroutine to smoothly rotate the camera towards the table
    IEnumerator RotateCamera(Quaternion targetRotation)
    {
        while (Quaternion.Angle(playerCamera.rotation, targetRotation) > 0.1f)
        {
            playerCamera.rotation = Quaternion.Slerp(playerCamera.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }
    }


}
