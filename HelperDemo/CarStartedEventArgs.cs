using System;

namespace Helper.Demo
{
    internal class CarStartedEventArgs : EventArgs
    {
        public string LicenseNumber {get;}

        public CarStartedEventArgs(string licenseNumber)
        {
            this.LicenseNumber = licenseNumber;
        }
    }
}