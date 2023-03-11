﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfApp1.componens
{
    internal class Navigation
    {
        public static List<Nav> navs = new List<Nav>();
        public static MainWindow main;
        public static Use AutoUser;
        public static bool IsUser;
        public static void NextPage(Nav nav)
        {
            navs.Add(nav);
            Update(nav);
        }
        public static void Update(Nav nav)
        {
            main.MyTitle.Text = nav.Title;
            main.MyFarame.Navigate(nav.Page);
            main.BackBtn.Visibility = navs.Count > 1 ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
            main.ExitBtn.Visibility = IsUser == true ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
        }
        public static void BackPage()
        {
            if (navs.Count > 1)
            {
                navs.Remove(navs[navs.Count - 1]);
                Update(navs[navs.Count - 1]);
            }
        }
    }
    class Nav
    {
        public string Title { get; set; }
        public Page Page { get; set; }
        public Nav(string Title, Page Page)
        {
            this.Title = Title;
            this.Page = Page;
        }
    }
}

