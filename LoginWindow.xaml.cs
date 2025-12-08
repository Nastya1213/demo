using demo.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace demo;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class LoginWindow : Window

    
{
    private readonly AppDbContext _context = new AppDbContext(); 
    public LoginWindow()
    {
        InitializeComponent();
    }

    private void Login_Click(object sender, RoutedEventArgs e)
    {
        var user = _context.Пользовательs.FirstOrDefault(u => u.Логин == LoginTextBox.Text && u.Пароль == PasswordTextBox.Text);
        if (user != null)
        {
            MessageBox.Show("Успешный вход");
            new MainWindow().Show();
            Close();
            // другое окно открываем и это закрваем
        }
        else
        {
            MessageBox.Show("Неверный логин или пароль");
        }

    }
}


