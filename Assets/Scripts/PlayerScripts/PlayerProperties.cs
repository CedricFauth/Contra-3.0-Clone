using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperties : MonoBehaviour
{
    public int health = 3;
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
        movement.setSpeed(5f);
        ammo.setDamage(1f);
        weapon.setCooldown(0.2f);
    }

    public void Damage()
    {
        health -= 1;
    }

    public void DamageWithVal(int n)
    {
        health -= n;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
