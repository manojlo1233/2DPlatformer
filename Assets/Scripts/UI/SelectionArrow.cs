using UnityEngine;
using UnityEngine.UI;

public class SelectionArrow : MonoBehaviour
{
    public RectTransform[] options;
    public AudioClip changeSound;
    public AudioClip interactSound;
    public int currentPosition;
    private RectTransform rect;
    
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        // Change positions of arrow
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            ChangePosition(-1);
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChangePosition(1);
        }

        // Interact with options
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    private void Interact()
    {
        SoundManager.instance.PlaySound(interactSound);
        // Access button component
        options[currentPosition].GetComponent<Button>().onClick.Invoke();
    }

    private void ChangePosition(int _change)
    {
        currentPosition += _change;
        if (currentPosition < 0)
        {
            currentPosition = options.Length - 1;
        }
        currentPosition %= options.Length;
        SoundManager.instance.PlaySound(changeSound);
        rect.position = new Vector3(rect.position.x, options[currentPosition].position.y, 0);
    }


}
