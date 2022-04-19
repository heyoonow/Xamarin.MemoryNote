using HeyNow.Std.Biz.MemoryNote;
using HeyNow.Std.Biz.MemoryNote.Data;
using HeyNow.Std.Model.MemoryNote.App;
using MemoryNote.Views.Setting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MemoryNote.ViewModels
{
    public class SettingViewModel: BaseViewModel
    {
        public Command SettingPageCommand { get; }
        public Command HelpCommand { get; }
        private SettingBiz biz;
        private string settingMemory;
        private bool isSettingMemoryNone;
        private bool isSettingMemory;
        public bool IsSettingMemoryNone
        {
            get
            {
                return isSettingMemoryNone;
            }
            set
            {
                SetProperty(ref isSettingMemoryNone, value);
                if (value)
                    pref.Set("SETTING_MEMORY", "0");
            }
        }

        public bool IsSettingMemory
        {
            get
            {
                return isSettingMemory;
            }
            set
            {
                SetProperty(ref isSettingMemory, value);
                if (value)
                    pref.Set("SETTING_MEMORY", "1");
            }
        }
        public SettingViewModel()
        {
            SettingPageCommand = new Command(OnSettingPage);
            HelpCommand = new Command(async () => { await OnHelp(); });
            biz = new SettingBiz();
            string settingMemory =pref.Get("SETTING_MEMORY");
            isSettingMemory = false;
            isSettingMemoryNone = false;
            if (settingMemory == "0")
                isSettingMemoryNone = true;
            else
                isSettingMemory = true;
        }

        private async Task OnHelp()
        {
            await SendEmail("help.heynow@gmail.com", "메모리노트 버그 또는 아이디어");
        }

        private async void OnSettingPage(object obj)
        {
            string page = (string)obj;
            if (page == "SETTING_COLOR")
                await Shell.Current.GoToAsync(nameof(SettingColorPage));
        }
    }
}
