using UnityEngine;

public class HealthCollectable : MonoBehaviour
{
    public float healthValue;
    public AudioClip pickupSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SoundManager.instance.PlaySound(pickupSound);
            collision.GetComponent<Health>().addHealth(healthValue);
            gameObject.SetActive(false);
        }
    }
}
