namespace ReadCSV.Services
{
    public static class StringExtension
    {
        /// <summary>
        /// Returns double value
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static double TryGetDouble(this string item)
        {
            double i;
            double.TryParse(item, out i);
            return i;
        }

        /// <summary>
        /// Returns int value
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static int TryGetInt(this string item)
        {
            int i;
            int.TryParse(item, out i);
            return i;
        }
    }
}
