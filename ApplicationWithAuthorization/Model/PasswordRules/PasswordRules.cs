using ApplicationWithAuthorization.Model.Converters;
using ApplicationWithAuthorization.Model.Database.Entities;
using System.Text.RegularExpressions;

namespace ApplicationWithAuthorization.Model.PasswordRules
{
    public class PasswordRules
    {
        // Функция обрабатывающая требование - минимальная длина пароля. Системой задано 10 - минимальная длина.
        public bool LengthRule(string password)
        {
            if (!string.IsNullOrEmpty(password))
            {
                if (password.Length > 9)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }            
            return false;
        }

        // Функция обрабатывающая требование - использование спец. символов ( или символов разного регистра ).
        public bool MultiRegRule(string password)
        {
            if (!string.IsNullOrEmpty(password))
            {
                bool hasDigit = Regex.IsMatch(password, @"\d");
                bool hasLowercase = Regex.IsMatch(password, @"[a-z]");
                bool hasUppercase = Regex.IsMatch(password, @"[A-Z]");
                bool hasSpecialChar = Regex.IsMatch(password, @"[^a-zA-Z0-9]");
                return hasDigit && hasLowercase && hasUppercase && hasSpecialChar;
            }
            return false;
        }

        // Функция обрабатывающая требование - отбраковка паролей по словарю, проверка на предыдущие пароли.
        // Реализуется созданием массива паролей, который формируется из истории паролей в БД.
        public bool CheckPasswordHistory(string password, User user)
        {
            if (!string.IsNullOrEmpty(password) && user != null)
            {
                string[] passwordHistoryArray = user.PasswordHistory.Split("\n");

                if (!passwordHistoryArray.Contains(password))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }            
            return false;
        }
        
        // Функция обрабатывающая требование - проверку пароля на срок. Срок действительности каждого пароля программой задан как 30 дней.
        public bool CheckOutdatedRule(User user)
        {
            if (user != null)
            {
                DateTime userPasswordDate = DataConverter.ConvertFromUser(user);
                DateTime currentDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                var daysDifference = currentDate - userPasswordDate;
                if (daysDifference.Days >= 30)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }             
            return false;
        }
    }
}
