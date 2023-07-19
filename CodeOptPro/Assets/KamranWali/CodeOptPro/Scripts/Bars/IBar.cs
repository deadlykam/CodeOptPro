using KamranWali.CodeOptPro.CommonInterfaces;

namespace KamranWali.CodeOptPro.Bars
{
    public interface IBar : IObjectNormalValue
    {
        /// <summary>
        /// This method should be called in start method for setting up the bar at start up.
        /// </summary>
        /// <param name="maxValue">The max value of the bar, of type int</param>
        public void StartSetup(int maxValue);

        /// <summary>
        /// The amount of value to add to the bar.
        /// </summary>
        /// <param name="value">The amount of value to add, of type int</param>
        public void AddValue(int value);

        /// <summary>
        /// The amount of value to remove from the bar.
        /// </summary>
        /// <param name="value">The amount of value to remove, of type int</param>
        public void RemoveValue(int value);

        /// <summary>
        /// This method gets the max value of the bar.
        /// </summary>
        /// <returns>The max value of the bar, of type int</returns>
        public int GetMaxValue();

        /// <summary>
        /// This method gets current value of the bar.
        /// </summary>
        /// <returns>The current value of the bar, of type int</returns>
        public int GetCurrentValue();

        /// <summary>
        /// This method sets the current value, if current value more than max then max value used.
        /// </summary>
        /// <param name="value">To set the current value, cannot exceed max value, of type int</param>
        public void SetCurrent(int value);

        /// <summary>
        /// This method checks if the bar is finished.
        /// </summary>
        /// <returns>True means the bar is finished, false otherwise, of type bool</returns>
        public bool IsDepleted();

        /// <summary>
        /// This method resets the bar back to default state with the given max value.
        /// </summary>
        public void Restore();
    }
}