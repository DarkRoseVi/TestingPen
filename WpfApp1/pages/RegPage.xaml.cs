using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.componens;
using WpfApp1.pages;

namespace WpfApp1.pages
{
    /// <summary>
    /// Логика взаимодействия для RegPage.xaml
    /// </summary>
    public partial class RegPage : Page
    {
        public RegPage()
        {
            InitializeComponent();
        }

        private void DeletBt_Click(object sender, RoutedEventArgs e)
        {
            LoginTb.Text = " ";
            PasswordTb.Password = "";
            NameTb.Text = " ";
            PhoneTb.Text = " ";
        }

        private void RegBt_Click(object sender, RoutedEventArgs e)
        {

            string login = LoginTb.Text.Trim();
            string password = PasswordTb.Password.Trim();
            string name = NameTb.Text.Trim();
            string phone = PhoneTb.Text.Trim();
            var check = BdConect.db.Use.Where(x => x.Login == login).FirstOrDefault();
            if (check == null)
            {
                if (login.Length > 0 && password.Length > 0 )
                {

                    if (BdConect.db.Use.Local.Any(x => x.Login == login))
                    {
                        MessageBox.Show("Такой пользователль уже существует ");
                        return;
                    }
                    else
                    {
                        BdConect.db.Use.Add(new Use
                        {
                            Login = login,
                            Password = password,
                          Name = name,
                          Phone = phone,
                          RoleId = 3
                        });
                        
                       
                        BdConect.db.SaveChanges();

                         
                    }
                    MessageBox.Show("Сохранено");
                }
                else
                    MessageBox.Show("Заполните поля");
            }
            else
            {
                MessageBox.Show("Такой логмин уже существует");
            }
        }
    }
}
