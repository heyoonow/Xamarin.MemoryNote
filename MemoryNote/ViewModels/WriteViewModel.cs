using HeyNow.Std.Biz.MemoryNote;
using HeyNow.Std.Model.MemoryNote;
using HeyNow.Std.Model.MemoryNote.Type;
using MemoryNote.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MemoryNote.ViewModels
{
    [QueryProperty(nameof(Idx), nameof(Idx))]
    public class WriteViewModel:BaseViewModel
    {
        private MemoModel model;
        private int idx;
        private MemoBiz biz;
        private string contents;
        private bool isModify;
        public int Idx
        {
            get
            {
                return idx;
            }
            set
            {
                idx = value;
                Get(value);
            }
        }
        public MemoModel Model
        {
            get { return model; }
            set { SetProperty(ref model, value); }
        }
        
        public string Contents
        {
            get => contents;
            set => SetProperty(ref contents, value);
        }
        public bool IsModify
        {
            get => isModify;
            set => SetProperty(ref isModify, value);
        }
        public Command SaveCommand { get; }

        public WriteViewModel()
        {
            biz = new MemoBiz();
            SaveCommand = new Command(OnSave, ValidateSave);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
            Title = "메모작성";
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(contents);
        }

        private void OnSave()
        {
            if (IsModify)
                Modify();
            else
                Create();
        }

        private void Create()
        {
            biz.Create(Contents);
            Contents = "";
            Shell.Current.CurrentItem = (Shell.Current as AppShell).GetTab(MainTabType.LIST);
        }
        private async void Modify()
        {
            biz.Modify(Model, Contents);
            biz.Update(Model);
            Contents = "";
            await Shell.Current.GoToAsync("..");
        }

        private void Get(int value)
        {
            IsModify = true;
            Title = "메모수정";
            Model = biz.Get(value);
            Contents = model.Contents;
        }
    }
}
