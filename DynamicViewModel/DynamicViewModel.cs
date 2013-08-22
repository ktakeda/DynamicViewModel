using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Dynamic;
using System.Reflection;
using System.ComponentModel;

//モデル - ビュー - ビューモデル (MVVM: Model-View-ViewModel) の問題点とその解決策
//http://msdn.microsoft.com/ja-jp/magazine/ff798279.aspx

namespace DynamicViewModel
{
    public class DynamicViewModel<T> : DynamicObject, INotifyPropertyChanged
    {
        /// <summary>バインドさせたいクラス</summary>
        private T TargetData { get; set; }

        /// <summary> INotifyPropertyChangedの実装 </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary> コンストラクタ </summary>
        public DynamicViewModel(T targetData)
        {
            TargetData = targetData;
        }

        /// <summary> Getter </summary>
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            //TargetDataの同名のプロパティ
            var prop = typeof(T).GetProperty(binder.Name);

            //TargetDataに同じ名前のプロパティがある場合には
            if (prop != null)
            {
                //TargetDataのプロパティの値を返す
                result = prop.GetValue(TargetData, null);
                return true;
            }
            else
            {
                result = null;
                return false;
            }
        }

        /// <summary> Setter </summary>
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            //TargetDataの同名のプロパティ
            var prop = typeof(T).GetProperty(binder.Name);

            //TargetDataに同じ名前のプロパティがある場合には
            if (prop != null)
            {
                //TargetDataのプロパティに値を設定して、値が設定されたことを通知
                prop.SetValue(TargetData, value, null);
                PropertyChanged(this, new PropertyChangedEventArgs(binder.Name));
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
