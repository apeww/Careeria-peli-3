using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class die : MonoBehaviour
{
    
    public int health;
    public bool isAlive;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      if (isAlive == false)
            Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("EnemyBullet"))
            TakeDamage(10);
    }

    public void TakeDamage(int damage)
    {
        
        health -= damage;


        if (health <= 0) Invoke(nameof(DestroyEnemy), .5f);
    }

    private void DestroyEnemy()
    {
        
        GetComponent<CapsuleCollider>().enabled = false;
        isAlive = false;









    }
}
