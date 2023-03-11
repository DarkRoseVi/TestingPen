using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
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
    /// Логика взаимодействия для EditPenPage.xaml
    /// </summary>
    public partial class EditPenPage : Page
    {
        public Pen Pens { get; set; }
        public List<Color> Colors { get; set; }
        public List<componens.Type> Types { get; set; }
        public EditPenPage(Pen _pen)
        {
            BdConect.db.Color.Load();
            Colors = BdConect.db.Color.Local.ToList();
            BdConect.db.Type.Load();
            Types = BdConect.db.Type.Local.ToList();
            Pens = _pen;
            InitializeComponent();
        }

        private void AddBt_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFil = new OpenFileDialog()
            {
                Filter = "**.png|*.png|*.jpeg|*.jpeg|*.jpg|*.jpg",
            };
            if (openFil.ShowDialog().GetValueOrDefault())
            {
                Pens.Image = File.ReadAllBytes(openFil.FileName);
                ProductPhoto.Source = new BitmapImage(new Uri(openFil.FileName));
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            var PenId = BdConect.db.Pen.Where(x => x.Name == NameTb.Text.Trim()).FirstOrDefault();
            if (PenId == null)
            {
                BdConect.db.Pen.Add(Pens);
                MessageBox.Show("yes");
            }
            BdConect.db.SaveChanges();
            Navigation.NextPage(new Nav("Ручки", new PensPage()));
        }

        private void UnitMeasurementCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UnitMeasurementCb.SelectedItem == null)
                return;
            Pens.Color = UnitMeasurementCb.SelectedItem as Color;
        }

        private void TypeCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TypeCb.SelectedItem == null)
                return;
            Pens.Type = TypeCb.SelectedItem as componens.Type;
        }
    }
}
