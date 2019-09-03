using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject Focus = default;
    [SerializeField]
    private float LimitTop = float.MaxValue;
    [SerializeField]
    private float LimitBot = float.MinValue;
    [SerializeField]
    private float LimitRight = float.MaxValue;
    [SerializeField]
    private float LimitLelft = float.MinValue;

    // Update is called once per frame
    void Update()
    {
        if (Focus != default)
        {
            var xPosition = Focus.transform.position.x;
            if (xPosition > LimitRight) xPosition = LimitRight;
            if (xPosition < LimitLelft) xPosition = LimitLelft;

            var yPosition = Focus.transform.position.y;
            if (yPosition > LimitTop) yPosition = LimitTop;
            if (yPosition < LimitBot) yPosition = LimitBot;

            var zPosition = transform.position.z;

            transform.position = new Vector3(xPosition, yPosition, zPosition);
        }

    }
}
