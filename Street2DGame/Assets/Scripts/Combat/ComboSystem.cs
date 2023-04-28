using DG.Tweening;
using TMPro;
using UnityEngine;

public class ComboSystem : MonoBehaviour
{
    public int currentComboCount { get; private set; }

    public int highestComboCount { get; private set; }

    private float currentCountdownCombo;

    [SerializeField]
    private TextMeshProUGUI comboCounter;

    private RectTransform counterTransform;

    [SerializeField]
    private float countdownCombo;

    [SerializeField]
    private Vector2 showDestination;
    [SerializeField]
    private Vector2 hideDestination;

    private bool shownCounter;

    private void Start()
    {
        if (!comboCounter && !TryGetComponent(out comboCounter))
        {
            Debug.LogError("El contador no fue asignado");
        }
        counterTransform = comboCounter.rectTransform;
        comboCounter.text = $"Combo: {currentComboCount}";
        shownCounter = false;
    }

    private void Update()
    {
        if (currentCountdownCombo > 0)
        {
            currentCountdownCombo -= Time.deltaTime;
        }
        else
        {
            if (shownCounter)
            {
                ResetCombo();
            }
        }
    }

    private void ShowUI()
    {
        DOTween.To(() => comboCounter.alpha, x => comboCounter.alpha = x, 1f, 0.3f);
        counterTransform.DOAnchorPos(showDestination, 0.3f);
    }

    private void HideUI()
    {
        DOTween.To(() => comboCounter.alpha, x => comboCounter.alpha = x, 0f, 0.3f);
        counterTransform.DOAnchorPos(hideDestination, 0.3f);
    }

    public void IncreaseCombo()
    {
        currentComboCount++;
        //comboCounter.text = $"{currentComboCount}";
        currentCountdownCombo = countdownCombo;
        counterTransform.DOShakeScale(0.3f, Vector2.one * 0.1f, 2, 45);
        comboCounter.text = $"Combo: {currentComboCount}";
        if (!shownCounter)
        {
            shownCounter = true;
            //Mostrar el contador si no se ha mostrado;
            ShowUI();
        }
    }

    public void RecoverCountdown()
    {
        if (shownCounter)
        {
            currentCountdownCombo = countdownCombo;
        }
    }

    public void ResetCombo()
    {
        if (shownCounter)
        {
            shownCounter = false;
            //Ocultar el contador si no se ha mostrado;
            HideUI();
        }
        SetHighestCombo();
        currentCountdownCombo = 0;
    }

    public void SetHighestCombo() => highestComboCount = Mathf.Max(highestComboCount, currentComboCount);

    private void OnEnable()
    {
        MeleeBaseState.OnAttackLanded += IncreaseCombo;
    }

    private void OnDisable()
    {
        MeleeBaseState.OnAttackLanded -= IncreaseCombo;
    }
}