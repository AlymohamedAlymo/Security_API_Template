namespace Security_API_Template.Extensions
{
    public static class DateTimeExtention
    {
        public static int CalculateAge(this DateOnly dob)
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            var age = today.Year - today.Month;
            if (dob > today.AddYears(-age)) age--;
            return age;
        }
    }
}
