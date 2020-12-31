using Freed.Comman.Common;
using Freed.Controls.Foundation;
using Freedom.Controls.Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freed.Api.Monitor.ViewModel
{
    public class MenuMainPageViewModel: ObservableObject
    {
        //public RelayCommand ClickNewCommand
        //{
        //    get { return new RelayCommand(ClickNewVoid); }
        //}

        /// <summary>
        ///界面初始化
        /// </summary>
        /// <param name="obj"></param>
        public override void DoInitFunction(object obj)
        {
        }


        public override void DoNextFunction(object obj)
        {
            if (obj.IsEmpty())
                return;
            var nflag = obj.ToInt();
            var pagename = "";
            switch (nflag)
            {
                case 0:
                    pagename = "ApiMonitorPage";
                    break;
                case 1:
                    pagename = "SocketDataPage";
                    break;
                case 2:
                    pagename = "DataStatisticsPage";
                    break;
                    //case 3:
                    //    pagename = "IDCardApplyView/IDCardBankPayPage";
                    //    break;
            }
            MainWindowViewModels.Instance.SetFrameTraget(pagename);
        }

    }
}
