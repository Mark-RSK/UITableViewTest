using System;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;

namespace UITableViewTest.Core.ViewModels
{
	public class FirstViewModel
		: MvxViewModel
	{
		public MvxObservableCollection<string> Items { get; }
		public EventHandler<bool> ModifyList = delegate { };

		public FirstViewModel()
		{
			Items = new MvxObservableCollection<string>();

		}

		public override void Start()
		{
			base.Start();
			Add(this);
		}

		private async void Add(FirstViewModel instance)
		{
			while (true)
			{
				ModifyList(this, true);
				for (int i = 0; i < 100; i++)
				{
					Items.Add($"Item {i}");
				}
				ModifyList(this, false);

				await Task.Delay(10000);

				ModifyList(this, true);
				for (int i = 0; i < 100; i++)
				{
					Items.Remove($"Item {i}");
				}
				ModifyList(this, false);

				await Task.Delay(10000);
			}
		}
	}
}
