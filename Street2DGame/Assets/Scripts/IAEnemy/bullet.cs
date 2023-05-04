using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{ 
  public float Speed = 10f;




private Rigidbody2D rigidbody;
public GameObject enemy;
public Transform enemyTtan;

private void Awake()
{
    rigidbody = GetComponent<Rigidbody2D>();
        //Player = GameObject.FindGameObjectWithTag("Player");
        enemyTtan = enemy.transform;

}

// Start is called before the first frame update
void Start()
{
    if (enemyTtan.localScale.x > 0)  //valor+ = player mira derecha
    {
        rigidbody.velocity = new Vector2(Speed, rigidbody.velocity.y);
        transform.localScale = new Vector3(1, 1, 1); // Orientar Bullet segun Player
    }
    else
    {
        rigidbody.velocity = new Vector2(-Speed, rigidbody.velocity.y);
        transform.localScale = new Vector3(-1, 1, 1);
            Debug.Log("penes");
    }
  
   }
}