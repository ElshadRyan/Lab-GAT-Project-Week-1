using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private enum Mode
    {
        LookAt,
        LookInverted,
        LookForward,
    }

    [SerializeField] private Mode mode;

    private void LateUpdate()
    {
        switch (mode)
        {
            case Mode.LookAt:
                transform.LookAt(Camera.main.transform);
                break;

            case Mode.LookInverted:
                Vector3 DirectionFromCamera = transform.position - Camera.main.transform.position;
                transform.LookAt(transform.position + DirectionFromCamera);
                break;
            case Mode.LookForward:
                transform.forward =  Camera.main.transform.forward;
                break;
        }
    }
}
