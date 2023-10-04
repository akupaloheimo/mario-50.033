using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    private float originalX;
    private float maxOffset = 5.0f;
    private float enemyPatroltime = 2.0f;
    private int moveRight = -1;
    private Vector2 velocity;
    public Animator goombaAnimator;
    public GameManagerWeek3 gameManager;
    private Rigidbody2D enemyBody;
    public Vector3 startPosition = new Vector3(0.0f, 0.0f, 0.0f);
    public Transform enemyLocation;


    void Start()
    {
        goombaAnimator.enabled = false;
        enemyBody = GetComponent<Rigidbody2D>();
        // get the starting position
        originalX = transform.position.x;
        ComputeVelocity();
        gameManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManagerWeek3>();

    }
    void ComputeVelocity()
    {
        velocity = new Vector2((moveRight) * maxOffset / enemyPatroltime, 0);
    }
    void Movegoomba()
    {
        enemyBody.MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.transform.position.y > 2.20f)
        {
            gameManager.IncreaseScore(1);
            goombaAnimator.enabled = true;
            goombaAnimator.Play("stomp");
            enemyBody.constraints = RigidbodyConstraints2D.FreezePositionX;
            this.gameObject.transform.position = this.gameObject.transform.position + new Vector3(0, -0.4f, 0);
        }
    }
    void RemoveGoomba()
    {
        this.gameObject.SetActive(false);
    }
    void Update()
    {
        if (Mathf.Abs(enemyBody.position.x - originalX) < maxOffset)
        {// move goomba
            Movegoomba();
        }
        else
        {
            // change direction
            moveRight *= -1;
            ComputeVelocity();
            Movegoomba();
        }
    }

    public void GameRestart()
    {
        this.gameObject.SetActive(true);
        goombaAnimator.SetTrigger("gameRestart");
        goombaAnimator.enabled = false;
        enemyBody.constraints = RigidbodyConstraints2D.None;
        transform.localPosition = startPosition;
        originalX = transform.position.x;
        moveRight = -1;
        ComputeVelocity();
    }

}