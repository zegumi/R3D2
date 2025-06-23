using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController2 : MonoBehaviour
{

    public int hiz;
    public float ziplamaKuvveti;
    private float hareketYonu;
    private Rigidbody2D rgb;
    private bool karakterSagYuz = true;

    private bool zemin;
    public Transform zeminKontrol;
    public float yaricapKontrol;
    public LayerMask zeminne;

    private int ekstraZiplama;
    public int ekstraZiplamaSayisi;
    public Animator animator;
    public HealthBar healthBar;

    
        

    private void Start()
    {
        rgb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        CharacterAttack();

        if (Input.GetKeyDown(KeyCode.Space) && ekstraZiplama > 0)
        {
            rgb.velocity = Vector2.up * ziplamaKuvveti;
            ekstraZiplama--;
            animator.SetBool("IsJumping", true);
        }

        if (animator.GetBool("IsJumping") && ekstraZiplama > 0)
        {
            animator.SetBool("IsJumping", false);
        }
        
        if(zemin == true)
        {
            ekstraZiplama = ekstraZiplamaSayisi;
        }
        else if(Input.GetKeyDown(KeyCode.Space) && ekstraZiplama == 0 && zemin == true)
        {
            rgb.velocity = Vector2.up * ziplamaKuvveti;
        }
    }

    


    private void FixedUpdate()
    {
        zemin = Physics2D.OverlapCircle(zeminKontrol.position, yaricapKontrol, zeminne);


        hareketYonu = Input.GetAxis("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")));

        rgb.velocity = new Vector2(hareketYonu * hiz, rgb.velocity.y);
        

        if (karakterSagYuz == false && hareketYonu > 0)
        {
            Flip();
        }else if (karakterSagYuz == true && hareketYonu < 0)
        {
            Flip();
        }
    }

    private void Flip()
    {
        karakterSagYuz = !karakterSagYuz;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    void CharacterAttack()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            animator.SetTrigger("IsAttack");
        }
    }

     private void OnTriggerStay2D(Collider2D collision)
    {
        
         if(collision.tag == "diken")
        {
            healthBar.Damage(0.002f);
        }
        
    }
    
    
    


}
