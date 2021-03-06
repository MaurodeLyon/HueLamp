﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace HueLamp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static ApplicationDataContainer LOCAL_SETTINGS = ApplicationData.Current.LocalSettings;
        //LOCAL_SETTINGS.Values["ip"]= "localhost";
        Random rnd;
        public MainPage()
        {
            this.InitializeComponent();

            Debug.WriteLine(LOCAL_SETTINGS.Values["ip"]);
            Debug.WriteLine(LOCAL_SETTINGS.Values["port"]);
            Debug.WriteLine(LOCAL_SETTINGS.Values["user"]);
            Debug.WriteLine(LOCAL_SETTINGS.Values["username"]);
            rnd = new Random();
            //LOCAL_SETTINGS.Values["ip"] = null;
            //LOCAL_SETTINGS.Values["username"] = null;
            //LOCAL_SETTINGS.Values["user"] = null;
            //LOCAL_SETTINGS.Values["port"] = null;

            // this.ViewModel = new LampViewModel();

            DataContext = new MainViewModel();
            if (LOCAL_SETTINGS.Values["username"] != null)
            {
                MainViewModel m = DataContext as MainViewModel;
                m.NetworkHandler = new NetworkHandler(LOCAL_SETTINGS.Values["ip"].ToString(),
                LOCAL_SETTINGS.Values["port"].ToString(), LOCAL_SETTINGS.Values["user"].ToString(), m);
            }
        }

        //public LampViewModel ViewModel { get; set; }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Settings));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            
            String Fullcontents = (String)e.Parameter;
            if (Fullcontents != "")
            {
                String[] splitContents = Fullcontents.Split(' ');
                MainViewModel m = DataContext as MainViewModel;
                if (LOCAL_SETTINGS.Values["username"] == null)
                {
                    LOCAL_SETTINGS.Values["ip"] = splitContents[0];
                    LOCAL_SETTINGS.Values["port"] = splitContents[1];
                    LOCAL_SETTINGS.Values["user"] = splitContents[2];   
                    
                }
                m.NetworkHandler = new NetworkHandler(LOCAL_SETTINGS.Values["ip"].ToString(),
                    LOCAL_SETTINGS.Values["port"].ToString(), LOCAL_SETTINGS.Values["user"].ToString(), m);
                blah.Text = Fullcontents;
            }
        }

        private void RelativePanel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            RelativePanel block = (RelativePanel)sender;
            Lamp e2 = (Lamp)block.DataContext;
            Frame.Navigate(typeof(LampSettingsPage), new NavigateWrapper(e2.Id, (MainViewModel)DataContext));
        }

        private void Hue_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Rectangle block = (Rectangle)sender;
            Lamp e2 = (Lamp)block.DataContext;
            Frame.Navigate(typeof(LampSettingsPage), new NavigateWrapper(e2.Id, (MainViewModel)DataContext));
        }

        private void Lampname_Tapped(object sender, TappedRoutedEventArgs e)
        {
            TextBlock block = (TextBlock)sender;
            Lamp e2 = (Lamp)block.DataContext;
            Frame.Navigate(typeof(LampSettingsPage), new NavigateWrapper(e2.Id, (MainViewModel)DataContext));
        }

        private void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            MainViewModel m = (MainViewModel)DataContext;

            foreach (Lamp l in m.Lamps)
            {
                int bri, hue, sat;

                

                bri = rnd.Next(0, 254);
                hue = rnd.Next(0, 65535) ;
                
                sat = rnd.Next(0, 254);
                m.NetworkHandler.setLamp(l.Id.ToString(), bri.ToString(), hue.ToString(), sat.ToString());
            }


        }

        private void AppBarButton_Click_2(object sender, RoutedEventArgs e)
        {

        }
    }
}
