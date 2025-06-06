using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Health playerHealth;
    public Image totalHealthBar;
    public Image currentHealthBar;

    private void Start()
    {
        totalHealthBar.fillAmount = playerHealth.startingHealth / 10;
    }

    private void Update()
    {
        currentHealthBar.fillAmount = playerHealth.currentHealt / 10;
    }

}
