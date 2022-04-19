using HeyNow.Std.Biz.MemoryNote;
using HeyNow.Std.Model.MemoryNote;
using MemoryNote.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MemoryNote.ViewModels
{
    [QueryProperty(nameof(Idx), nameof(Idx))]
    public class DetailViewModel : BaseViewModel
    {
        private MemoModel model;
        private int idx;
        private MemoBiz biz;
        private string contents;
        private bool favorit;
        private string imageFavorit;
        public Command FavoritCommand { get; }
        public Command ModifyCommand { get; }
        public Command DeleteCommand { get; }
        public Command PrevCommand { get; }
        public Command ShareCommand { get; }
        public MemoModel Model
        {
            get { return model; }
            set { SetProperty(ref model, value); }
        }

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

        public string Contents
        {
            get => contents;
            set => SetProperty(ref contents, value);
        }

        public bool Favorit
        {
            get => favorit;
            set => SetProperty(ref favorit, value);
        }
        public string ImageFavorit
        {
            get => imageFavorit;
            set => SetProperty(ref imageFavorit, value);
        }
        public DetailViewModel()
        {
            biz = new MemoBiz();
            FavoritCommand = new Command(OnFavorit);
            ModifyCommand = new Command(OnModify);
            DeleteCommand = new Command(OnDelete);
            PrevCommand = new Command(OnPrev);
            ShareCommand = new Command(async () => await SharedMemo());
            ImageFavorit = "heart.png";
        }

        private async Task SharedMemo()
        {
            await ShareText(Contents);
        }

        private async void OnPrev(object obj)
        {
            await Shell.Current.GoToAsync("..");
        }
        private async void OnDelete(object obj)
        {
            biz.Delete(Model);
            await Shell.Current.GoToAsync("..");
        }

        private async void OnModify(object obj)
        {
            await Shell.Current.GoToAsync($"{nameof(WritePage)}?{nameof(WriteViewModel.Idx)}={Idx}");
        }

        private void OnFavorit(object obj)
        {
            model.Favorit = !model.Favorit;
            Favorit = model.Favorit;
            biz.Update(Model);

            if (Favorit)
                ImageFavorit = "heart_fill.png";
            else
                ImageFavorit = "heart.png";
        }

        private async void Get(int idx)
        {
            
            Model = biz.Get(idx);
            if (model == null)
            {
                await Shell.Current.GoToAsync("..");
            }
            Contents = Model.Contents;
            Favorit = Model.Favorit;
            Title = Model.Contents;

            if (Favorit)
                ImageFavorit = "heart_fill.png";
            else
                ImageFavorit = "heart.png";

        }
    }
}
