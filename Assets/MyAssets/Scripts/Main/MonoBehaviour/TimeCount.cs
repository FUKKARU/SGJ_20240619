using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace main
{
    public class TimeCount : MonoBehaviour
    {
        public int countdownMinutes = 3;
        private float countdownSeconds;
        private TextMeshProUGUI timeText;

        private void Start()
        {
            timeText = GetComponent<TextMeshProUGUI>();
            countdownSeconds = countdownMinutes * 60;
        }

        void Update()
        {
            countdownSeconds -= Time.deltaTime;
            var span = new TimeSpan(0, 0, (int)countdownSeconds);
            timeText.text = span.ToString(@"mm\:ss");

            if (countdownSeconds <= 0)
            {
                
            }
        }
    }
}