using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform m_Player;
    public Vector3 m_Offset;

    void FixedUpdate()
    {
        transform.position = m_Player.position + m_Offset;
    }
}
