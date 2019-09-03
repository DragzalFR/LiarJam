using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private const float DISTANCE_AUTO_CLOSE = 4.5f;

    public int KeysRequired;

    private GameObject AutoCloseDetection = null;
    private Vector3 StartPosition;
    private Quaternion StartRotation;

    void Start()
    {
        StartPosition = transform.position;
        StartRotation = transform.rotation;
    }

    void FixedUpdate()
    {
        if(AutoCloseDetection != null)
        {
            if(Vector2.Distance(transform.position, AutoCloseDetection.transform.position) > DISTANCE_AUTO_CLOSE)
            {
                Close();
            }
        }
    }

    public bool Open(int keyCount, GameObject autoClose = null) 
    {
        if(KeysRequired <= keyCount)
        {
            if (autoClose != null)
                AutoCloseDetection = autoClose;

            var colliders = this.GetComponents<Collider2D>();
            foreach (var collider in colliders)
                collider.enabled = false;

            ApplyOpenTransform();

            return true;
        }

        return false;
    }

    private void ApplyOpenTransform()
    {
        var doorSize = GetComponent<Renderer>().bounds.size;
        float halfDoorLength = Mathf.Max(doorSize.x, doorSize.y) / 2;

        transform.Rotate(0, 0, 90);
        transform.Translate(new Vector2(halfDoorLength, halfDoorLength));
    }

    private void Close()
    {
        transform.position = StartPosition;
        transform.rotation = StartRotation;

        var colliders = this.GetComponents<Collider2D>();
        foreach (var collider in colliders)
            collider.enabled = true;

        AutoCloseDetection = null;
    }

    private void ApplyCloseTranform()
    {
        var doorSize = GetComponent<Renderer>().bounds.size;
        float halfDoorLength = Mathf.Max(doorSize.x, doorSize.y) / 2;

        transform.Translate(new Vector2(-halfDoorLength, -halfDoorLength));
        transform.Rotate(0, 0, -90);
    }
}
