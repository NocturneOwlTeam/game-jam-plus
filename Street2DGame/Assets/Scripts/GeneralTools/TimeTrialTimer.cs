using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;

namespace Nocturne.GeneralTools
{
    public class TimeTrialTimer : MonoBehaviour
    {
        // This is a standard time trial timer for any purpose.

        [SerializeField]
        private float startingTime = 0f;

        [SerializeField]
        private float startingDelay = 0.4f;

        [SerializeField]
        private TextMeshProUGUI timerText;

        public float counterTime { get; private set; }

        private StringBuilder timer = new();

        [SerializeField]
        private bool canContinue = true;

        // Start is called before the first frame update
        private void Start()
        {
            counterTime = startingTime;
            timerText.text = "00:00";
            //StartCoroutine(StartCountdown());
        }

        private void Update()
        {
            if (canContinue)
            {
                UpdateTime();
            }
        }

        private IEnumerator StartCountdown()
        {
            yield return Helpers.GetWait(startingDelay);
            StartTime();
        }

        public void StopTimer() => canContinue = false;

        public void StartTime() => canContinue = true;

        public void ResetTime()
        {
            counterTime = startingTime;
            timerText.text = "00:00";
        }

        //For the MM:SS format.
        public void UpdateTime()
        {
            //Quick question: is this optimal?;
            counterTime += Time.deltaTime;
            timer.Length = 0;
            timer.AppendFormat("{0:00}", Mathf.FloorToInt(counterTime / 60));
            timer.Append(":");
            timer.AppendFormat("{0:00}", Mathf.FloorToInt(counterTime % 60));
            timerText.text = timer.ToString();
        }

        public void IncreaseTimer(float extraTime) => counterTime += extraTime;

        public void DecreaseTimer(float removedTime) => counterTime = Mathf.Max(counterTime - removedTime, 0);
    }
}