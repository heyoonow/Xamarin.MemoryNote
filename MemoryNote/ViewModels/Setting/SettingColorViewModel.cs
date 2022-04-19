using HeyNow.Std.Biz.MemoryNote;
using HeyNow.Std.Biz.MemoryNote.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MemoryNote.ViewModels.Setting
{
    public class SettingColorViewModel : BaseViewModel
    {
        public Command SelectColorCommand { get; }
        public Command SelectListColorCommand { get; }
        public Command SelectDetailColorCommand { get; }
        private SettingBiz biz;
        public SettingColorViewModel()
        {
            SelectColorCommand = new Command(OnSelectColor);
            SelectListColorCommand = new Command(OnSelectListColor);
            SelectDetailColorCommand = new Command(OnSelectDetailColor);
            biz = new SettingBiz();
        }

        private void OnSelectColor(object obj)
        {
            var model = SettingData.Instance.Setting;
            model.BackgroundColor = obj.ToString();
            biz.Update(model);
            BackgroundColor = obj.ToString();

        }
        private void OnSelectListColor(object obj)
        {
            var model = SettingData.Instance.Setting;
            model.MemoListColor = obj.ToString();
            biz.Update(model);
            MemoListColor = obj.ToString();

        }
        private void OnSelectDetailColor(object obj)
        {
            var model = SettingData.Instance.Setting;
            model.MemoDetailColor = obj.ToString();
            biz.Update(model);
            MemoDetailColor = obj.ToString();

        }
    }
}
