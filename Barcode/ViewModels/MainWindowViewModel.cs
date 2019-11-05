using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.ComponentModel;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows.Input;

namespace Barcode.ViewModels
{
	public class MainWindowViewModel : BindableBase, INotifyPropertyChanged
	{

		public ReactivePropertySlim<string> Title { get; } = new ReactivePropertySlim<string>("Barcode Application");

		private CompositeDisposable Disposable { get; } = new CompositeDisposable();

		public ReactiveCommand<KeyEventArgs> BarcodeEvent { get; } = new ReactiveCommand<KeyEventArgs>();

		public ReactivePropertySlim<string> KeyData { get; } = new ReactivePropertySlim<string>();

		public MainWindowViewModel()
		{
			BarcodeEvent.Subscribe(BarcodeInput).AddTo(Disposable);
		}

		~MainWindowViewModel()
		{
			Disposable.Dispose();
		}

		/// <summary>
		/// バーコード入力
		/// </summary>
		/// <param name="e"></param>
		private void BarcodeInput(KeyEventArgs e)
		{
			// 数字の場合はD1やNumPad1で取得するので最後の1文字だけを抽出
			string str = e.Key.ToString();
			KeyData.Value += str.Substring(str.Length - 1, 1);
		}

	}
}
