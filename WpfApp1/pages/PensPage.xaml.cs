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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.componens;
using WpfApp1.pages;

namespace WpfApp1.pages
{
    /// <summary>
    /// Логика взаимодействия для PensPage.xaml
    /// </summary>
    public partial class PensPage : Page
    {
        public PensPage()
        {
            InitializeComponent();
            PenListViu.ItemsSource = BdConect.db.Pen.ToList();
        }
        public void Rechres() 
        {
            IEnumerable<Pen> penList = BdConect.db.Pen;
            if (SortCb.SelectedItem != null)
            {
                switch ((SortCb.SelectedItem as ComboBoxItem).Tag)
                {
                    case "1":
                        penList = penList.OrderBy(x => x.Name);
                        break;
                    case "2":
                        penList = penList.OrderByDescending(x => x.Name);
                        break;
                    case "3":
                        penList = BdConect.db.Pen;
                        break;
                    case "4":
                        penList = penList.OrderBy(x => x.Price);
                        break;
                    case "5":
                        penList = penList.OrderByDescending(x => x.Price);
                        break;
                    default:
                        break;
                }
            }
            if (FiltrCb.SelectedItem != null)
            {
                switch ((FiltrCb.SelectedItem as ComboBoxItem).Tag)
                {
                    case "1":
                        penList = BdConect.db.Pen;
                        break;
                    case "2":
                        penList = penList.Where(x => x.ColorId == 1);
                        break;
                    case "3":
                        penList = penList.Where(x => x.ColorId == 2);
                        break; 
                    case "4":
                        penList = penList.Where(x => x.ColorId == 3);
                        break;   
                    case "5":
                        penList = penList.Where(x => x.ColorId == 4);
                        break;
                            case "6":
                        penList = penList.Where(x => x.TypeId == 1);
                        break;    
                    case "7":
                        penList = penList.Where(x => x.TypeId == 2);
                        break; 
                    case "8":
                        penList = penList.Where(x => x.TypeId == 3);
                        break;
                    default:
                        break;
                }
                if (PoiskTb == null)
                    return;
                if (PoiskTb.Text.Length > 0)
                {
                    penList = penList.Where(x => x.Name.StartsWith(PoiskTb.Text) );
                }
                PenListViu.ItemsSource = penList.ToList();

            }

        }
        

        private void EditingBt_Click(object sender, RoutedEventArgs e)
        {
            var pens = (sender as Button).DataContext as Pen;
            Navigation.NextPage(new Nav("Редактирование", new EditPenPage(pens)));
        }

        private void SortCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Rechres();
        }

        private void FiltrCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Rechres();
        }

        private void PoiskTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            Rechres();
        }
              
                private void DeletBtn_Click(object sender, RoutedEventArgs e)
        {
            var pens = (sender as Button).DataContext as Pen;
            if (MessageBox.Show("Вы точно хотите удалить эту запись", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                BdConect.db.Pen.Remove(pens);
        }

        private void AddProdBt_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new Nav("Добавление", new EditPenPage(new Pen())));
        }

      
    }
}
