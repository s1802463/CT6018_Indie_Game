using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnemies : MonoBehaviour
{

    public GameObject m_Enemy;
    public float m_xPos;
    public float m_zPos;
    public int m_enemyCount;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while(m_enemyCount < 10)
        {
            m_xPos = Random.Range(-96.31f, -88.65f);
            m_zPos = Random.Range(-30.62f, -2.8f);
            Instantiate(m_Enemy, new Vector3(m_xPos, -33.15f, m_zPos), Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
            m_enemyCount++;
        }
    }

}
