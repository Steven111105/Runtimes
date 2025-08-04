using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour
{
    int cameraIndex = 0;
    public bool canMoveCamera = true; // Flag to control camera movement
    // Start is called before the first frame update
    void Start()
    {
        cameraIndex = 0; // Initialize camera index

    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.LeftArrow))
        // {
        //     LookLeft();
        // }
        // else if (Input.GetKeyDown(KeyCode.RightArrow))
        // {
        //     LookRight();
        // }
    }

    public void LookRight()
    {
        if (!canMoveCamera) return;
        if (cameraIndex <= -1) return; // Prevent going below index -1
        cameraIndex--;
        StartCoroutine(MoveCamera(cameraIndex));
    }

    public void LookLeft()
    {
        if (!canMoveCamera) return; // Check if camera movement is allowed
        if (cameraIndex >= 1) return; // Prevent going above index 1
        cameraIndex++;
        StartCoroutine(MoveCamera(cameraIndex));
    }

    private IEnumerator MoveCamera(int index)
    {
        SliderScript.instance.CancelSlider();
        canMoveCamera = false; // Prevent further camera movement until the current move is complete
        Vector2 startPosition = transform.localPosition;
        Vector2 targetPosition = new Vector2(index * 1920f, 0); // Adjust the position based on index
        float elapsedTime = 0f;
        float duration = 1f;

        Debug.Log("Moving cam");
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            transform.localPosition = Vector2.Lerp(startPosition, targetPosition, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = targetPosition;
        Debug.Log("Camera moved to index: " + index);
        canMoveCamera = true; // Allow camera movement again
    }
}
