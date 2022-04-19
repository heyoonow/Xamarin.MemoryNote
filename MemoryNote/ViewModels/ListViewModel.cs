using HeyNow.Std.Biz.MemoryNote;
using HeyNow.Std.Biz.MemoryNote.Data;
using HeyNow.Std.Extend;
using HeyNow.Std.Model.MemoryNote;
using HeyNow.Std.Model.MemoryNote.App;
using HeyNow.Std.Model.MemoryNote.Type;
using MemoryNote.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MemoryNote.ViewModels
{
    public class ListViewModel : BaseViewModel
    {
        public MemoSearchModel searchModel ;
        private MemoModel _selectedItem;
        private MemoOrderModel selectMemoOrder;
        private string searchText;
        private bool isFilter;
        private string imageFilter;
        public ObservableCollection<MemoModel> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command SearchCommand { get; }
        public Command FilterCommand { get; }
        public Command PrevCommand { get; }
        public Command<MemoModel> ItemTapped { get; }
        private MemoBiz biz;
        public List<MemoOrderModel> ListMemoOrder { get; set; }
        
        public MemoOrderModel SelectMemoOrder
        {
            get => selectMemoOrder;
            set
            {
                selectMemoOrder = value;
                searchModel.MemoOrderType = value.Code;
                if (IsSettingMemory == true)
                    pref.Set("SETTING_MEMO_ORDER", (int)value.Code);
                GetMemoList();
            }
        } 
        public string SearchText
        {
            get { return searchText; }
            set 
            { 
                SetProperty(ref searchText, value);
                searchModel.SearchText = SearchText;
                if (value.IsEmpty())
                    GetMemoList();
            }
        }
        public bool IsFilter
        {
            get { return isFilter; }
            set
            {
                SetProperty(ref isFilter, value);
            }
        }
        public string ImageFilter
        {
            get { return imageFilter; }
            set
            {
                SetProperty(ref imageFilter, value);
            }
        }
        public ListViewModel()
        {
            int code = 0;
            if (IsSettingMemory == true)
                code = pref.Get("SETTING_MEMO_ORDER", 0);
            MemoOrderType order = (MemoOrderType)code;
            searchModel = new MemoSearchModel() { MemoOrderType= order };
            Title = "Browse";
            Items = new ObservableCollection<MemoModel>();
            //LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            LoadItemsCommand = new Command(GetMemoList);

            ItemTapped = new Command<MemoModel>(OnItemSelected);
            SearchCommand = new Command(OnSearch);
            AddItemCommand = new Command(OnAddItem);
            FilterCommand = new Command(OnFilter);
            
            biz = new MemoBiz();
            ListMemoOrder = biz.GetOrderData();
            SelectMemoOrder = ListMemoOrder.Where(x=>x.Code == order).First();
            ImageFilter = "filter.png";

            FirstSetup(biz);
        }

        

        private async void OnPrev(object obj)
        {
            await Shell.Current.GoToAsync("..");
        }

        private void OnFilter(object obj)
        {
            IsFilter = !IsFilter;
            if (IsFilter)
                ImageFilter = "filter_fill.png";
            else
                ImageFilter = "filter.png";
        }

        public void OnSearch(object obj)
        {
            
            GetMemoList();
        }

        async void GetMemoList()
        {
            IsBusy = true;

            try
            {
                Items.Clear();


                var items = biz.GetList(searchModel);
                foreach (var item in items)
                {
                    if (item.ModifyDT != null)
                        item.IsModify = true;
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
            ReloadSetting();
        }

        public MemoModel SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(WritePage));
        }

        async void OnItemSelected(MemoModel item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(DetailPage)}?{nameof(DetailViewModel.Idx)}={item.Idx}");
        }
        

    }
}
