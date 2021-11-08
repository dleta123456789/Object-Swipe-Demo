using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer), typeof(BoxCollider))]
public class ClickAndSwipe : MonoBehaviour
{
    [Header ("Mouse Settings")]
    [SerializeField] private Vector3 mousePos;

    private GameManager gameManager;
    private Camera cam;
    private TrailRenderer trail;
    private BoxCollider boxCol;
    private bool swiping = false;
    // Start is called before the first frame update
    void Awake()
    {
        cam = Camera.main;
        trail = GetComponent<TrailRenderer>();
        boxCol = GetComponent<BoxCollider>();
        trail.enabled = false;
        boxCol.enabled = false;
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive && gameManager.pause==false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                swiping = true;
                UpdateComponents();
            }
            if (Input.GetMouseButtonUp(0))
            {
                swiping = false;
                UpdateComponents();
            }
            if (swiping)
            {
                UpdateMousePosition();
            }
        }
    }
    void UpdateMousePosition()
    {
        mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y, 10.0f));
        //used 10f on z cause camera is -10f
        transform.position = mousePos;
    }
    void UpdateComponents()
    {
        trail.enabled = swiping;
        boxCol.enabled = swiping;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Target>())
        {
            collision.gameObject.GetComponent<Target>().DestroyTarget();
        }
    }
}
