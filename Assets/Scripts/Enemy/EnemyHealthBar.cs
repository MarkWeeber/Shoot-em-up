
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Slider Slider;
    [SerializeField] private Color LowHealthColor = Color.red;
    [SerializeField] private Color MaxHealthColor = Color.green;
    [SerializeField] private Vector3 Offset = Vector3.up;
    private Image fillImage;
    
    private void Start()
    {
        fillImage = Slider.fillRect.GetComponentInChildren<Image>();
        SetHealth(1, 1);
    }

    private void Update()
    {
        Slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + Offset);
    }

    public void SetHealth(float currentHealth, float maxHealth)
    {
        if(currentHealth < 0)
        {
            Slider.gameObject.SetActive(false);
        }
        Slider.value = currentHealth;
        Slider.maxValue = maxHealth;
        fillImage.color = Color.Lerp(LowHealthColor, MaxHealthColor, Slider.normalizedValue);
    }
}
