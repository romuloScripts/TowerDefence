using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class HealthUI : MonoBehaviour
{
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void UpdateHealth(float health)
    {
        image.fillAmount = health;
    }
}
