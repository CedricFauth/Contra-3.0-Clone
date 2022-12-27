using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneMovement : MonoBehaviour
{

    private Rigidbody2D body;
    private Transform drone;
    [SerializeField] private Transform player;


    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        drone = GetComponent<Transform>();

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
