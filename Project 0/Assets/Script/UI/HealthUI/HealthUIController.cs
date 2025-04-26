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
            for (int i = 1; i <= damage; i++)
            {
                if (numberOfHearts > 0)
                {
                    numberOfHearts--;
                    healthUIView.hearts[numberOfHearts].SetActive(false);
                }
            }
        }
    }
}
