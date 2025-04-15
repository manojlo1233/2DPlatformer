using UnityEngine;

public class Health : MonoBehaviour
{

    public float startingHealth;
    public float currentHealt { get; private set; }
    private Animator anim;
    private bool dead;

    private void Awake()
    {
        dead = false;
        currentHealt = startingHealth;
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(float _damage)
    {
        
        currentHealt = Mathf.Clamp(currentHealt - _damage, 0, startingHealth);
      
        if (currentHealt > 0)
        {
            anim.SetTrigger("hurt");
        }
        else
        {
            if (!dead)
            {
                anim.SetTrigger("die");
                GetComponent<PlayerMovement>().enabled = false;
                dead = true;
            }
        }
    }

    public void addHealth(float _value)
    {
        currentHealt = Mathf.Clamp(currentHealt + _value, 0, startingHealth);
    }
}
