using System;

namespace WebApplication4.Security
{
    public static class TCISOTP
    {
        public static int OTP(int id, OTPMinuteTypes type)
        {
            var dt = DateTime.Now;
            return (id * 1001) ^ ((dt.Day * 100 + dt.Month) * 100 + dt.Hour) * 10 + FindNumberKey(dt.Minute, (int)type);
        }
        public static int RetrievalOTP(int otp, OTPMinuteTypes type)
        {
            var dt = DateTime.Now;
            return (otp ^ (((dt.Day * 100 + dt.Month) * 100 + dt.Hour) * 10 + FindNumberKey(dt.Minute, (int)type))) / 1001;
        }
        private static int FindNumberKey(int currentMinutes, int percent)
        {
            const int TOTAL_SENCOND_ON_MINUTE = 60;
            int ratio = TOTAL_SENCOND_ON_MINUTE / percent;
            int i = 1;
            while (i * ratio <= TOTAL_SENCOND_ON_MINUTE)
            {
                if (currentMinutes <= (i * ratio))
                    return i;
                i++;
            }
            return 0;
        }
    }
    public enum OTPMinuteTypes
    {
        EXTRA_SMALL = 4,
        SMALL = 6,
        ELONGATE_SMALL = 10,
        MEDIUM = 12,
        LARGE = 20,
        MEDIUM_PLUS = 30
    }
}
