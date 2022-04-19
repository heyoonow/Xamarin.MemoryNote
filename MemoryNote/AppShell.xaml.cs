using HeyNow.Std.Model.MemoryNote.Type;
using MemoryNote.ViewModels;
using MemoryNote.Views;
using MemoryNote.Views.Setting;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MemoryNote
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(WritePage), typeof(WritePage));
            Routing.RegisterRoute(nameof(DetailPage), typeof(DetailPage));
            Routing.RegisterRoute(nameof(SettingColorPage), typeof(SettingColorPage));
            
            //Routing.RegisterRoute(nameof(ListPage), typeof(ListPage));
            //Routing.RegisterRoute(nameof(SettingPage), typeof(SettingPage));
            if (Device.RuntimePlatform == Device.UWP)
            {
                list.Icon = "Assets/Image/list.png";
                write.Icon = "Assets/Image/write.png";
                setting.Icon = "Assets/Image/setting.png";
            }
        }


        public ShellContent GetTab(MainTabType type)
        {
            ShellContent value;
            switch (type)
            {
                case MainTabType.LIST:
                    value = list;
                    break;
                case MainTabType.WRITE:
                    value = write;
                    break;
                case MainTabType.SETTING:
                    value = setting;
                    break;
                default:
                    value = list;
                    break;
            }
            return value;
            
        }
    }
}
