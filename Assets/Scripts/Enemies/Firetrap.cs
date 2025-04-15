using System.Collections;
using UnityEngine;

public class Firetrap : MonoBehaviour
{
    public float damage;
    [Header("Firetrap Timers")]
    public float activationDelay;
    public float activeTime;
    private Animator anim;
    private SpriteRenderer spriteRend;
    private bool triggered;
    private bool active;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("EVO");
        if (collision.tag == "Player")
        {
            if (!triggered)
            {
                StartCoroutine(ActivateFiretrap());
            }
            if (active)
            {
                collision.GetComponent<Health>().TakeDamage(damage);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        print("EVO STAY");
        if (collision.tag == "Player")
        {
            if (!triggered)
            {
                StartCoroutine(ActivateFiretrap());
            }
            if (active)
            {
                collision.GetComponent<Health>().TakeDamage(damage);
            }
        }
    }

    private IEnumerator ActivateFiretrap()
    {
        triggered = true;
        spriteRend.color = Color.red; // Notify player when trap is activated
        yield return new WaitForSeconds(activationDelay);

        spriteRend.color = Color.white; 
        active = true;
        anim.SetBool("activated", true);

        yield return new WaitForSeconds(activeTime);
        
        active = false;
        triggered = false;
        anim.SetBool("activated", false);
    }
}
