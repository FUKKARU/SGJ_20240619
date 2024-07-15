using System;
using System.Collections;
using System.Collections.Generic;
using SO;
using TMPro;
using UnityEngine;

namespace main
{
    public class Judge : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI timeText;
        [SerializeField] int countdownSeconds = 60;

        float currentSeconds;
        SO_Tags tags;

        void Awake()
        {
            tags = SO_Tags.Entity;
            currentSeconds = countdownSeconds;
        }

        void Update()
        {
            currentSeconds -= Time.deltaTime;
            var span = new TimeSpan(0, 0, (int)currentSeconds);
            timeText.text = span.ToString(@"mm\:ss");

            if (currentSeconds <= 0)
            {

            }
        }

        
    }
}