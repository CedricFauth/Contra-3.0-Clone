using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootProjectiles : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform pfBullet;
    private float damage;

    private void Awake()
    {
        GetComponent<PlayerAim>().OnShoot += PlayerShootProjectiles_OnShoot;
    }

    private void PlayerShootProjectiles_OnShoot(object sender, PlayerAim.OnShootEventArgs e)
    {
        Transform bulletTransform = Instantiate(pfBullet, e.gunEndPointPosition, Quaternion.identity);
        Vector3 shootDir = (e.shootPosition - e.gunEndPointPosition).normalized;
        bulletTransform.GetComponent<Bullet>().Setup(shootDir, damage);

    }

    public void setDamage(float value)
    {
        this.damage = value;
    }
    public float getDamage()
    {
        return this.damage;
    }

}
