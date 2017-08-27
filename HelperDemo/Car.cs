using System;
using Helper.Threading;

namespace Helper.Demo
{
    internal class Car
    {
        public string LicenseNumber { get; }

        internal event EventHandler<CarStartedEventArgs> Started;

        public Car(string licenseNumber)
        {
            this.LicenseNumber = licenseNumber;
        }

        internal void Start()
        {
            // Do something to start the car.
            Console.WriteLine($"Starting car {LicenseNumber}...");

            // Once the car is started, raise OnStarted event here.
            var e = new CarStartedEventArgs(LicenseNumber);
            OnStarted(e);
        }

        protected virtual void OnStarted(CarStartedEventArgs e)
        {
            Started.Raise(this, e);
        }
    }
}