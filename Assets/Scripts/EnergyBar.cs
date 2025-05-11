using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    public Image fillImage;
    public float maxEnergy = 100f;
    public float currentEnergy;

    private void Update()
    {
        UpdateEnergyBar();
    }

    private void Start()
    {
        currentEnergy = maxEnergy;
        UpdateEnergyBar();
    }

    public void UseEnergy(float amount)
    {
        currentEnergy -= amount;
        currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);
        UpdateEnergyBar();
    }

    public void GainEnergy(float amount)
    {
        currentEnergy += amount;
        currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);
        UpdateEnergyBar();
    }

    void UpdateEnergyBar()
    {
        fillImage.fillAmount = currentEnergy / maxEnergy;
    }
}