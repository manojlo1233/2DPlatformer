using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    public float attackCooldown;
    public Transform firePoint;
    public GameObject[] arrows;
    [Header("SFX")]
    public AudioClip arrowSound;
    private float cooldownTimer;
    

    private void Attack()
    {
        cooldownTimer = 0;
        int index = FindArrow();
        SoundManager.instance.PlaySound(arrowSound);
        arrows[index].transform.position = firePoint.position;
        arrows[index].GetComponent<EnemyProjectile>().ActivateProjectile();
        
    }

    private int FindArrow()
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            if (!arrows[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        if (cooldownTimer >= attackCooldown)
        {
            Attack();
        }
    }
}
