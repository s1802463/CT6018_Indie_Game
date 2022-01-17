using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public Transform[] allGround;
    public List<MeshCollider> groundColliders;
    public GameObject m_Player;

    public static class CollisionOutcome
    {
        public static bool collided;
    }

    public void Awake()
    {
        allGround = GameObject.Find("Floor").GetComponentsInChildren<Transform>();

        foreach(Transform ground in allGround)
        {
            groundColliders.Add(ground.gameObject.GetComponent<MeshCollider>());
        }

        m_Player = GameObject.Find("Player");
    }

    private void OnCollisionEnter(Collision collision)
    {      
        if (collision.collider.gameObject.layer == 3)
        {
            Debug.Log("collision");
            CollisionOutcome.collided = true;
        }
        else if (collision.gameObject.tag == "ground")
        {
            foreach (Collider ground in groundColliders)
            {
                Physics.IgnoreCollision(ground, m_Player.GetComponent<Collider>());
            }
        }
        else
        {
            CollisionOutcome.collided = false;
        }
    }
}

