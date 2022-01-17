using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static CollisionDetection;

public class PlayerController : MonoBehaviour
{
    private readonly VectorPID angularVelocityController = new VectorPID(33.7766f, 0, 0.2553191f);
    private readonly VectorPID headingController = new VectorPID(9.244681f, 0, 0.06382979f);

    public Transform target;

    public GameObject m_Player;
    public Transform m_Bullet;

    private bool move;

    int m_Speed;

    public float m_Torque;

    void Start()
    {
        m_Speed = 10;
    }

    void Awake()
    {
        GameStateManager.Instance.onGameStateChanged += OnGameStateChanged;
        OnGameStateChanged(GameStateManager.Instance.CurrentGameState);
    }

    void OnDestroy()
    {
        GameStateManager.Instance.onGameStateChanged -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState newGameState)
    {
        enabled = newGameState == GameState.Gameplay;

        switch(newGameState)
        {
            case GameState.Gameplay:
                move = true;
                break;
            case GameState.Paused:
                move = false;
                break;
        }
    }

    private void Fire(Vector3 fireDir)
    {
        Transform projectileTransform = Instantiate(m_Bullet, m_Player.transform.position, transform.rotation * Quaternion.Euler(0f, 0f, 90f));
        projectileTransform.GetComponent<ProjectileScript>().SetUp(fireDir);
    }

    public void FixedUpdate()
    {
        Rigidbody playerBody = GameObject.Find("Player").GetComponent<Rigidbody>();

        RaycastHit hit = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 targetLoc = new Vector3(hit.point.x, -33.15f, hit.point.z);
            //Vector3 lookDir = new Vector3(targetLoc.x, -33.15f, targetLoc.z).normalized;
            Vector3 dir = (targetLoc - m_Player.transform.position).normalized;

            Vector3 angularVelocityError = playerBody.angularVelocity * -1;
            Debug.DrawRay(m_Player.transform.position, playerBody.angularVelocity * 10, Color.black);

            Vector3 angularVelocityCorrection = angularVelocityController.Update(angularVelocityError, Time.deltaTime);
            Debug.DrawRay(m_Player.transform.position, angularVelocityCorrection, Color.green);

            playerBody.AddTorque(angularVelocityCorrection);

            Vector3 dirNormal = (targetLoc - m_Player.transform.position);
            Debug.DrawRay(m_Player.transform.position, dirNormal, Color.magenta);

            Vector3 currentDir = m_Player.transform.up;
            Debug.DrawRay(m_Player.transform.position, currentDir * 15, Color.blue);

            Vector3 directionError = Vector3.Cross(currentDir, dirNormal);
            Vector3 directionCorrection = headingController.Update(directionError, Time.deltaTime);

            playerBody.AddTorque(directionCorrection);

            if(move == true)
            {
                if (Input.GetKey(KeyCode.W))
                {

                    if (hit.collider.gameObject.tag == "ground")
                    {
                        //Quaternion rotation = Quaternion.LookRotation(dir, Vector3.up);

                        //m_Player.transform.rotation = rotation;

                        // if(!CollisionOutcome.collided)
                        //{
                        //m_Player.transform.Translate(dir * Time.deltaTime * m_Speed, Space.World);

                        playerBody.MovePosition(m_Player.transform.position + dir * m_Speed * Time.deltaTime);
                        //}

                        //m_Player.transform.forward = hit.point;
                        //m_Player.transform.position = hit.point;
                    }
                }
                if (Input.GetMouseButtonDown(0))
                {
                    Fire(dir);
                }
            }
        }
           
    }

}

public class VectorPID
{
    public float pFactor, iFactor, dFactor;

    private Vector3 integral;
    private Vector3 lastError;

    public VectorPID(float pFactor, float iFactor, float dFactor)
    {
        this.pFactor = pFactor;
        this.iFactor = iFactor;
        this.dFactor = dFactor;
    }

    public Vector3 Update(Vector3 currentError, float timeFrame)
    {
        integral += currentError * timeFrame;

        Vector3 deriv = (currentError - lastError) / timeFrame;

        lastError = currentError;

        return currentError * pFactor + integral * iFactor + deriv * dFactor;
    }
}
