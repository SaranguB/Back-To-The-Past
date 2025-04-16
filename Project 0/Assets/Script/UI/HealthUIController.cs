using System;
using UnityEngine;

namespace UI
{
    public class HealthUIController
    {
        private HealthUIView healthUIView;
        private int numberOfHearts;
        private int totalLives;
        public HealthUIController(HealthUIView healthUIView)
        {
            this.healthUIView = healthUIView;
        }

        public void SetNumberOfLives(int totalLives)
        {
            this.totalLives = totalLives;
            numberOfHearts = totalLives;
        }

        public void AddLives()
        {
            if (numberOfHearts < totalLives)
            {
                healthUIView.hearts[numberOfHearts].SetActive(true);
                numberOfHearts++;
            }
        }

        public void RemoveLives(int damage)
        {
            Debug.Log("entered");
            for (int i = 1; i <= damage; i++)
            {
                Debug.Log("entered loop");

                if (numberOfHearts > 0)
                {
                    Debug.Log("removed");

                    numberOfHearts--;
                    healthUIView.hearts[numberOfHearts].SetActive(false);
                }
            }

        }

    }
}
