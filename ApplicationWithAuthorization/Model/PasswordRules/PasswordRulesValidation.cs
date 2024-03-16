namespace ApplicationWithAuthorization.Model.PasswordRules
{    
    public class PasswordRulesValidation
    {
        // контейнер объектов PasswordConditions (список).
        // в него добавляются объекты-требования, состояние которых ( свойство conditionResult ) будет менятся взависимости от результата проверки функциями PasswordRules.
        private List<PasswordConditions> passwordConditions;

        // Контейнер для функций PassworRules. В него добавляются функции из PasswordRules.
        List<Func<bool>> validationOperations;

        // Инициализация списков для противодействия исключению Null Reference.
        public PasswordRulesValidation()
        {
            passwordConditions = new List<PasswordConditions>();
            validationOperations = new List<Func<bool>>();
        }

        // Метод, позволяющий объекту PasswordRulesValidation добавить объект PasswordCondition в  его контейнер passwordConditions.
        public PasswordRulesValidation SetCondition(PasswordConditions conditions)
        {
            passwordConditions.Add(conditions);
            return this;
        }

        // Метод, позволяющий объекту PasswordRulesValidation добавить функцию из PasswordRules в  его контейнер validationOperations.
        public PasswordRulesValidation SetRules(Func<bool> operation)
        {            
            validationOperations.Add(operation);
            return this;
        }

        // Процедура, где мы для каждого PasswordConditions (парольное требование) проверяем, выполняется ли оно.
        public List<PasswordConditions> ProcessValidations()
        {            
            List<PasswordConditions> conditionResults = new List<PasswordConditions>();
            var validationResults = validationOperations.Zip(passwordConditions, (operation, condition) => new { Operation = operation, Condition = condition });
            foreach (var result in validationResults)
            {
                result.Condition.conditionResult = result.Operation.Invoke();
            }            
            foreach (var result in validationResults)
            {
                conditionResults.Add(result.Condition);
            }
            return conditionResults;
        }
    }
}
