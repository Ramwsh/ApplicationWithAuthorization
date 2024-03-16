using ApplicationWithAuthorization.Model.Database.Entities;
using System.Globalization;

namespace ApplicationWithAuthorization.Model.Converters
{
    static public class DataConverter
    {
        public static DateTime ConvertFromUser(User user)
        {
            return DateTime.ParseExact(user.PasswordDate, "dd.MM.yyyy H:mm:ss", CultureInfo.InvariantCulture);
        }
    }
}
