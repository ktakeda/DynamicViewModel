using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using DynamicViewModel;

using System.Dynamic;
using System.ComponentModel;
using System.Diagnostics;

namespace WpfApplication1
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        //実際に編集したいデータ
        public Person Target;

        //画面にバインドする用のデータ（画面上でのデータ編集をPropertyChangedイベントで補足できる）
        public DynamicViewModel<Person> BindingTarget;

        public MainWindow()
        {
            //編集したいオブジェクト
            Target = new Person { Name = "のっち" };

            //画面に見せる用のオブジェクト
            BindingTarget = new DynamicViewModel<Person>(Target);

            //画面でデータに変更があったときに呼ばれるイベントハンドラ
            BindingTarget.PropertyChanged += new PropertyChangedEventHandler(BindingTarget_PropertyChanged);

            //お約束
            InitializeComponent();
            DataContext = BindingTarget;
        }

        void BindingTarget_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //画面でデータが変更されるとデバッグ出力に編集後のNameが表示される。
            Debug.WriteLine("Name = " + Target.Name);
        }
    }
}
