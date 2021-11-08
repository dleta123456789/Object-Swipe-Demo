using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [Header("Forces")]
    public float minForce, maxForce;

    [Header("Torque")]
    public float minTorque, maxTorque;
    [Header("Spawn Location")]
    public float minXRange;
    public float maxXRange;
    public float yStart;

    [Header("Scoring")]
    public int ScoreVal;

    [Header("Text Score")]
    private GameManager gameManager;

    [Header("Particle System ")]
    public ParticleSystem explosionParticle;

    private Rigidbody targetRb;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        targetRb =GetComponent<Rigidbody>();
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(),RandomTorque(), ForceMode.Impulse);
        transform.position = RandomSpawnPos();

    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive == false)
        {
            gameObject.SetActive(false);
        }
    }
    private Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minForce, maxForce);
    }
    float RandomTorque()
    {
        return Random.Range(minTorque, maxTorque);
    }
    private Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(minXRange, maxXRange), yStart);
    }
    private void OnMouseDown()
    {
        
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("Good"))
        {
            
            gameManager.UpdateScore(-ScoreVal);
            gameManager.RemoveLife();
            //gameManager.GameOver();
        }

    }
    public void DestroyTarget()
    {
        if (gameManager.GameState())
        {
            Destroy(gameObject);
            if (gameObject.CompareTag("Bad"))
            {
                gameManager.RemoveLife();
            }
            gameManager.UpdateScore(ScoreVal);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
        }
    }

}
