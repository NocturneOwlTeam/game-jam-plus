using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Nocturne.GeneralTools
{
    public class Timer : MonoBehaviour
    {
        // This is a standard timer for any purpose.
        [SerializeField]
        private float time = 100f;

        [SerializeField]
        private TextMeshProUGUI timerText;

        public float counterTime { get; private set; }

        [SerializeField]
        private UnityEvent OnTimerEnded;

        private bool isDone;

        [SerializeField]
        private bool isRunning = true;

        private StringBuilder timeString = new();

        public float currentTime => counterTime;

        private void Start()
        {
            BeginAgain();
        }

        private void Update()
        {
            TimerController();
        }

        public void TimerController()
        {
            if (!isRunning || isDone) return;
            if (counterTime > 0.05f)
            {
                counterTime -= Time.deltaTime;
                ShowTimeFormatted();
            }
            else
            {
                if (!isDone)
                {
                    isDone = true;
                    OnTimerEnded.Invoke();
                }
            }
        }

        public void ResetTimer() => counterTime = time;

        private void ShowTimeFormatted()
        {
            timeString.Length = 0;
            timeString.AppendFormat("{0:00}", Mathf.FloorToInt(counterTime / 60));
            timeString.Append(":");
            timeString.AppendFormat("{0:00}", Mathf.FloorToInt(counterTime % 60));
            timerText.text = timeString.ToString();
        }

        public void ResumeTimer() => isRunning = true;

        public void StopTimer() => isRunning = false;

        public void BeginAgain()
        {
            ResetTimer();
            ShowTimeFormatted();
        }

        public void IncreaseTimer(float extraTime) => counterTime += extraTime;

        public void DecreaseTimer(float removedTime) => counterTime = Mathf.Max(counterTime - removedTime, 0);
    }
}