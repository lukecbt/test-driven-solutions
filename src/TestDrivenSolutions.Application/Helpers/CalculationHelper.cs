namespace TestDrivenSolutions.Application.Helpers
{
    public static class CalculationHelper
    {
        /// <summary>
        /// Calculates the parking charge for the given charge per minute and additional charge based on the vehicle type tarrif
        /// </summary>
        public static decimal CalculateParkingCharge(DateTime timeIn, DateTime timeOut, decimal chargePerMinute, int additionalChargeMinutes = 5, decimal additionalChargeAmount = 1)
        {
            TimeSpan lengthOfStay = timeOut - timeIn;
            int minutes = (int)lengthOfStay.TotalMinutes;

            decimal charge = minutes * chargePerMinute;

            // Additional charge per 5 minutes
            int additionalChargesCount = minutes / additionalChargeMinutes;
            decimal additionalCharge = additionalChargesCount * additionalChargeAmount;

            return charge + additionalCharge;
        }
    }
}
