using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    public event EventHandler<OnShootEventArgs> OnShoot;
    public class OnShootEventArgs : EventArgs
    {
        public Vector3 gunEndPointPosition;
        public Vector3 shootPosition;
    }

    private Transform gunEndTransform;
    private Transform aimTransform;
    private Transform body;
    private float cooldown = 0.2f;
    [SerializeField]private float cdReset;

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
        aimTransform = transform.Find("Shoulder");
        gunEndTransform = aimTransform.Find("GunPointEnd");
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
            HandleShooting();
        }
        
    }

    private void HandleAiming()
    {
        Vector3 mousePosition = GetMouseWorldPosition();

        Vector3 aimDirection = (mousePosition - transform.position).normalized;
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
    
    private void HandleShooting()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = GetMouseWorldPosition();
            OnShoot?.Invoke(this, new OnShootEventArgs
            {
                gunEndPointPosition = gunEndTransform.position,
                shootPosition = mousePosition,
            }) ;
            cooldown = cdReset;
        }
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
