namespace ApplicationWithAuthorization.Model.PasswordRules
{
    // Объекты PasswordConditions служат сущностями, которые описывают результаты валидации
    // conditionName - название условия валидации пароля. Фактически принимает в себя название того или иного парольного требования
    // conditionResult - результат валидации, True / False, взависимости от того, что вернет функция валидации, присутствующая в PasswordRulesValidation классе.
    public class PasswordConditions
    {
        public string? conditionName { get; set; } = null;
        public bool conditionResult { get; set; } = false;        
    }
}
