using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ManaUI : MonoBehaviour
{
    [SerializeField]
    private GameObject manaTarget;

    private IManaSystem manaSystem;

    [SerializeField]
    private TextMeshProUGUI textMana;

    [SerializeField]
    private Image manaBar;

    private void Start()
    {
        if (!manaTarget.TryGetComponent(out manaSystem))
        {
            Debug.LogError("Error: El objeto que esta apuntando no tiene un sistema de mana establecido.");
        }

        StartUI();
    }

    private void StartUI()
    {
        if (manaSystem != null)
        {
            manaSystem.OnManaChanged += UpdateMana;
            UpdateManaBar();
            UpdateManaText();
        }
    }

    public void UpdateMana()
    {
        UpdateManaBar();
        UpdateManaText();
    }

    private void UpdateManaBar()
    {
        manaBar.DOFillAmount(manaSystem.GetMana() / manaSystem.GetMaxMana(),0.5f);
    }

    private void UpdateManaText()
    {
        textMana.text = $"{manaSystem.GetMana()}/{manaSystem.GetMaxMana()}";
    }

    private void OnEnable()
    {
        StartUI();
    }

    private void OnDisable()
    {
        manaSystem.OnManaChanged -= UpdateMana;
    }
}