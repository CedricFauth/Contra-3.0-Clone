using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    private Rigidbody2D body;
    private Transform player;

    bool isGrounded;

    public Transform groundCheck;
    private Vector3 right = new Vector3(5, 5, 1);
    private Vector3 left = new Vector3(-5, 5, 1);
    public LayerMask groundlayer;
    public bool fallThrough;


    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        player = GetComponent<Transform>();

    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundlayer);
    }

    private void Update()
    {
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);
        Vector3 mouseLocation = GetMouseWorldPosition();

        if (player.position.x > mouseLocation.x)
        {
            player.localScale = left;
        }
        else if (player.position.x < mouseLocation.x)
        {
            player.localScale = right;
        }

        if (isGrounded && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space)))
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
        }
        //Skript f?r OneWay Plattformen
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            fallThrough = true;
        } 
        else 
        {
            fallThrough = false;
        }
    }
    
    public float getSpeed()
    {
        return this.speed;
    }
    public void setSpeed(float value)
    {
        this.speed = value;
    }
    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }
    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
}
