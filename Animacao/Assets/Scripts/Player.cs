using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed;
    public float JumpForce;
    private float timeAttack; // Contador
    public float startTimeAttack; // Tempo da animação
    private bool isGrounded;

    private Rigidbody2D rigidbody;
    private Animator animator;
    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Lógica de movimentação do personagem
        if (Input.GetKey(KeyCode.LeftArrow))
        {            
            rigidbody.velocity = new Vector2(-Speed, rigidbody.velocity.y);

            // Incluindo animação da caminhada
            animator.SetBool("isWalking", true);

            // Rotacionar player para esquerda
            sprite.flipX = true;
        }
        else
        {
            // Assim que soltar a seta esquerda, parar ao invés de continuar deslizando
            rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);

            // Não tem tecla pressionada, desativa walking
            animator.SetBool("isWalking", false);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            rigidbody.velocity = new Vector2(Speed, rigidbody.velocity.y);

            // Incluindo animação da caminhada
            animator.SetBool("isWalking", true);

            // Rotacionar player para direita
            sprite.flipX = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            animator.SetBool("isJumping", true);
        }

        if (timeAttack <= 0)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                Debug.Log("atacou");
                animator.SetTrigger("isAttacking"); // Iniciou animação
                timeAttack = startTimeAttack; // Tempo para colocar no Unity
            }            
        }
        else
        {            
            timeAttack -= Time.deltaTime; // decrescer em tempo real            
            if (timeAttack <= 0)
            {
                animator.SetTrigger("isAttacking"); // Terminou animação
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        // Personagem está tocando o chão
        if (col.gameObject.layer == 8)
        {
            isGrounded = true;
            animator.SetBool("isJumping", false);
        }
    }
}
