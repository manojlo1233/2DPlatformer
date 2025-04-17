using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    public float startingHealth;
    public float currentHealt { get; private set; }
    private Animator anim;
    private bool dead;

    [Header("iFrames")]
    public float iFramesDuration;
    public int numberOfFlashes;
    SpriteRenderer spriteRend;

    [Header("Components")]
    public Behaviour[] components;
    [Header("Death sound")]
    public AudioClip deathSound;
    public AudioClip hurtSound;
    private bool invulnerable;

    private void Awake()
    {
        dead = false;
        currentHealt = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float _damage)
    {
        if (invulnerable) return;
        currentHealt = Mathf.Clamp(currentHealt - _damage, 0, startingHealth);
      
        if (currentHealt > 0)
        {
            anim.SetTrigger("hurt");
            SoundManager.instance.PlaySound(hurtSound);
            StartCoroutine(Invulnerability());
        }
        else
        {
            if (!dead)
            {
                anim.SetTrigger("die");
                // Deactivate all atached components
                foreach (Behaviour comp in components)
                {
                    comp.enabled = false;
                }
                dead = true;
                SoundManager.instance.PlaySound(deathSound);
            }
        }
    }

    public void addHealth(float _value)
    {
        currentHealt = Mathf.Clamp(currentHealt + _value, 0, startingHealth);
    }

    private IEnumerator Invulnerability()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(8, 9, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(8, 9, false);
        invulnerable = false;
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
