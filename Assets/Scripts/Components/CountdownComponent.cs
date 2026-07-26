using Janito.EditorExtras;
using System;
using TMPro;
using UnityEngine;

namespace GMTK
{
    [RequireComponent(typeof(TMP_Text))]
    public class CountdownComponent : MonoBehaviour
    {
        public event Action OnCountdownEnd;
        public float RemainingTime { 
            get
            {
                return _remainingTime;
            }
            set
            {
                _remainingTime = Mathf.Clamp(value, 0, float.MaxValue);
            }
        }

        [SerializeField]
        private float _initialTime = 60;
        private TMP_Text _displayText;
        private float _remainingTime;

        [SerializeField]
        private Animator _animator;

        [Button(ButtonExecutionModes.PlayMode)]
        protected void TestIncrease()
        {
            RemainingTime += 10;
        }

        [Button(ButtonExecutionModes.PlayMode)]
        protected void TestDecrease()
        {
            RemainingTime -= 50;
        }

        public void ReduceTime(float time)
        {
            RemainingTime -= time;
            _animator?.Play("Remove", -1, 0.0f);
        }

        public void IncreaseTime(float time)
        {
            RemainingTime += time;
            _animator?.Play("Add", -1, 0.0f);
        }

        private void Awake()
        {
            _displayText = GetComponent<TMP_Text>();
            RemainingTime = _initialTime;
        }

        private void Update()
        {
            RemainingTime -= Time.deltaTime;
            _displayText.SetText(TimeSpan.FromSeconds(RemainingTime).ToString(@"mm\:ss"));
            if (RemainingTime <= 0)
            {
                OnCountdownEnd?.Invoke();
            }
        }
    }
}
