using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperties : MonoBehaviour
{
    public int health = 100;
    private PlayerMovement movement;
    private PlayerShootProjectiles ammo;
    private PlayerAim weapon;


    private void Awake()
    {
        ammo = GetComponent<PlayerShootProjectiles>();
        movement = GetComponent<PlayerMovement>();
        weapon = GetComponent<PlayerAim>();
    }
    void Start()
    {
        movement.setSpeed(10f);
        ammo.setDamage(10f);
        weapon.setCooldown(0.6f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
