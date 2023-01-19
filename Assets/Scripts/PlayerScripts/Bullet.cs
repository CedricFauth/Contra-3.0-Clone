using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float damage;
    private Vector3 shootDir;
    public LayerMask groundLayer;

    [SerializeField] private float moveSpeed = 100f;
    public void Setup(Vector3 shootDir, float dmg)
    {
        this.shootDir = shootDir;
        this.damage = dmg;
        transform.eulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(shootDir));
        Destroy(gameObject, 2f);
        //Debug.Log("Damage: "+ damage);
    }

    private void FixedUpdate()
    {
        transform.position += shootDir * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();
        if (player == null)
        {
            if (!collision.CompareTag("Bullet"))
            {
                Destroy(gameObject);
            }
            
        }
    }
    public static float GetAngleFromVectorFloat (Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }

}

