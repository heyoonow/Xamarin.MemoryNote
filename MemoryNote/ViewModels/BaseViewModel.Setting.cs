using HeyNow.Std.Biz.MemoryNote;
using HeyNow.Std.Biz.MemoryNote.Data;
using HeyNow.Std.Model.MemoryNote;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MemoryNote.ViewModels
{
    public partial class BaseViewModel : INotifyPropertyChanged
    {
        private string backgroundColor;
        private string memoListColor;
        private string memoDetailColor;
        public SettingModel Setting
        {
            get { return setting; }
            set { SetProperty(ref setting, value); }
        }
        public string BackgroundColor
        {
            get { return backgroundColor; }
            set { SetProperty(ref backgroundColor, value); }
        }
        public string MemoListColor
        {
            get { return memoListColor; }
            set { SetProperty(ref memoListColor, value); }
        }
        public string MemoDetailColor
        {
            get { return memoDetailColor; }
            set { SetProperty(ref memoDetailColor, value); }
        }
        public bool IsSettingMemory
        {
            get
            {
                string value = pref.Get("SETTING_MEMORY");
                if (value == ""||value == "0")
                    return false;
                return true;
            }
        }
        protected void ReloadSetting()
        {
            SettingData.Instance.LoadSetting();
            Setting = SettingData.Instance.Setting;
            BackgroundColor = setting.BackgroundColor;
            MemoListColor = setting.MemoListColor;
            MemoDetailColor = setting.MemoDetailColor;
        }

        protected void FirstSetup(MemoBiz biz)
        {
            if (IsFirst() == false)
                return;
            string contents = $@"꼭 읽어 주세요.
메모리노트를 사용해 주셔서 감사합니다.!
메모리노트는 어떻게 하면 메모를 잘 사용할 수 있을까 에대한 물음표로 시작했습니다. 
앞으로 업데이트를 꾸준히 하여 좀더 편한 메모 앱이 될 수 있도록 노력 하겠습니다.

탭별 기능
1.목록
 - 작성한 메모가 노출 됩니니다.
 - Floating 버튼(우측하단)을 눌러서 원하는 메모를 검색 및 정렬할 수 있습니다.
 - 메모정보
    조회수 : 작성한 메모를 본 횟수
    등록일 : 작성한 날짜/시간
    제목 : 메모상 제일 첫줄
    제목 우측 숫자 : 메모의 내용 길이
    수정일 : 메모를 수정하면 아래쪽 노출
    하트 : 메모에 하트를 표시하면 리스트에 노출
2.작성
    메모를 작성합니다.
3.설정
    메모의 다양한 설정을 할 수 있습니다.
";

            biz.Create(contents);

            pref.Set("SETTING_MEMORY", "0");
        }
    }
}
