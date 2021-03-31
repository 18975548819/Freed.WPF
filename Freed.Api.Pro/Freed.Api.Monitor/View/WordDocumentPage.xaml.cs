using Freed.Comman.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Freed.Api.Monitor.View
{
    /// <summary>
    /// WordDocumentPage.xaml 的交互逻辑
    /// </summary>
    public partial class WordDocumentPage : Page
    {
        public WordDocumentPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                //for (int i = 1; i <= fileDialog.FileName.Length; i++)
                //{
                //    if (fileDialog.FileName.Substring(fileDialog.FileName.Length - i, 1).Equals(@"\"))
                //    {
                //        //更改默认路径为最近打开路径
                //        Path = fileDialog.FileName.Substring(0, fileDialog.FileName.Length - i + 1);
                //        return true;
                //    }
                //}
                string FilePath = fileDialog.FileName;
                string DirPath = System.IO.Path.GetDirectoryName(FilePath);//更改默认路径为最近打开路径

                docViewer.Document = WordUnityHepler.ConvertWordToXPS(FilePath).GetFixedDocumentSequence();
                docViewer.FitToWidth();

            }
        }
    }
}
