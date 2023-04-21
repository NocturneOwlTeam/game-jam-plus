using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Nocturne.GeneralTools
{
    public class StandardSequence : MonoBehaviour
    {
        // This is a standard delay for any purpose.

        [SerializeField]
        private float sequenceTime;

        [SerializeField]
        private UnityEvent OnStartSequence;

        [SerializeField]
        private UnityEvent OnEndSequence;

        private IEnumerator loopSequence;

        private void Start()
        {
            loopSequence = Loop();
        }

        public void StartSequence() => StartCoroutine(Sequence());

        public void StartSequence(float customTime) => StartCoroutine(Sequence(customTime));

        public void StartLoop() => StartCoroutine(loopSequence);

        public void StopLoop() => StopCoroutine(loopSequence);

        private IEnumerator Sequence()
        {
            OnStartSequence.Invoke();
            yield return Helpers.GetWait(sequenceTime);
            OnEndSequence.Invoke();
        }

        private IEnumerator Sequence(float customTime)
        {
            OnStartSequence.Invoke();
            yield return Helpers.GetWait(customTime);
            OnEndSequence.Invoke();
        }

        //Uses what is on the OnEndSequence event.
        private IEnumerator Loop()
        {
            yield return Helpers.GetWait(sequenceTime);
            OnEndSequence.Invoke();
            StartCoroutine(Loop());
        }
    }
}