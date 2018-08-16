using System;

namespace TestSamples.Before
{
    public class TemperatureCensorArgs : EventArgs
    {
        public TemperatureCensorArgs(int percentageAboveThreshold, int temperature)
        {
            PercentageAboveThreshold = percentageAboveThreshold;
            Temperature = temperature;
        }

        public int Temperature { get; }
        public int PercentageAboveThreshold { get; }
    }

    public class TemperatureCensor
    {
        //TODO: To be configurable in the future
        private const int TriggerPercentage = 5;
        private readonly int _temperatureThreshold;

        public TemperatureCensor(int temperatureThreshold, int initialTemperature)
        {
            CurrentTemperature = initialTemperature;
            _temperatureThreshold = temperatureThreshold;
        }

        public int CurrentTemperature { get; private set; }

        public int PercentageAboveThreshold => (int) Math.Round((double)(CurrentTemperature - _temperatureThreshold) * 100 / _temperatureThreshold);

        public event EventHandler<TemperatureCensorArgs> OverHeatTrigger;

        public void IncreaseTemperature(int value = 1)
        {
            CurrentTemperature += value;
            if (CurrentTemperature < _temperatureThreshold)
                return;

            if (PercentageAboveThreshold > TriggerPercentage)
                OnOverHeatTrigger();
        }

        protected virtual void OnOverHeatTrigger()
        {
            OverHeatTrigger?.Invoke(this, new TemperatureCensorArgs(PercentageAboveThreshold, CurrentTemperature));
        }
    }
}