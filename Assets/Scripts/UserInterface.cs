using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    // Create an Image object called image
    public Image image;

    Transform objectPosition;

    private void Start()
    {
        // Grab the Image component found in the inspector
        image = GetComponent<Image>();
    }

    private void Update()
    {
        // Change the source sprite image to the sprite image found
        // in the scene that the Player is standing over.
        /*
         * Pseudocode:
         * 1. Check to see if targetPosition/destination clicked is
         *    any list of objects in question.
         * 2. Assign transform position of object in question.
         * 3. Use transform position to FindObject.
         * 4. if(name == 'object in question') display sprite/icon.
         */
        // objectPosition = ;
        //image.sprite = FindObjectOfType<GameObject>() ;


    }
}
