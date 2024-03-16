using System.Windows;

namespace ApplicationWithAuthorization
{    
    public partial class App : Application
    {
        // Вызов OnStartup() метода при старте программы для тестирования подключения к БД.
        protected override void OnStartup(StartupEventArgs e)
        {
            //UserCRUD crud = new UserCRUD();
            //User admin = crud.ReadUsers().FirstOrDefault();
            //Console.WriteLine(admin.Name);
            //Console.WriteLine(admin.Password);
            //Console.WriteLine(admin.Email);
            //Console.WriteLine(admin.Role);
            //Console.WriteLine(admin.Id);            
        }
    }    
}
