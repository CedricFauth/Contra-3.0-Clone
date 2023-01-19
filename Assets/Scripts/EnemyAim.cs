using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAim : MonoBehaviour
{
    public event EventHandler<OnShootEventArgs> EnemyShoot;
    public class OnShootEventArgs : EventArgs
    {
        public Vector3 gunEndPointPosition;
        public Vector3 shootPosition;
    }

    private Transform gunEndTransform;
    private Transform aimTransform;
    private Transform body;
    private float cooldown = 1f;
    [SerializeField] private float cdReset;
    [SerializeField] private Transform EnemyShoulder;
    [SerializeField] private Transform EnemyGunPointEnd;

    public float getCooldown()
    {
        return cdReset;
    }

    public void setCooldown(float value)
    {
        cdReset = value;
        if (cdReset <= 0.02f)
        {
            cdReset = 0.02f; //Maximale Schussgeschwindigkeit
        }
    }

    void Awake()
    {
        aimTransform = EnemyShoulder;
        gunEndTransform = EnemyGunPointEnd;
        body = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleAiming();
        
        if(cooldown >= 0)
        {
            cooldown -= Time.deltaTime;
        } else
        {
            EnemyFire();
        }
        
    }

    private void HandleAiming()
    {
        Vector3 playerPosition = (GameObject.FindGameObjectWithTag("Player").transform.position);
        Vector3 aimDirection = (playerPosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);
        if (body.transform.localScale.x > 0)
        {
            aimTransform.localScale = new Vector3(0.2f, 0.2f, aimTransform.localScale.z);
        }
        else if (body.transform.localScale.x < 0)
        {
            aimTransform.localScale = new Vector3(-0.2f, -0.2f, aimTransform.localScale.z);
        }

    }
    
    private void EnemyFire()
    {
        Vector3 mousePosition = GetMouseWorldPosition();
        EnemyShoot?.Invoke(this, new OnShootEventArgs
        {
            gunEndPointPosition = gunEndTransform.position,
            shootPosition = mousePosition,
        }) ;
        cooldown = cdReset;
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
