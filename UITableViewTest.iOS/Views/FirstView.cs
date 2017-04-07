using System;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.iOS.Views;
using UIKit;
using UITableViewTest.Core.ViewModels;

namespace UITableViewTest.iOS.Views
{
	[MvxFromStoryboard]
	public partial class FirstView : MvxViewController<FirstViewModel>
	{
		public FirstView(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			ViewModel.ModifyList += (sender, modifyList) =>
			{
				if (modifyList)
					table.BeginUpdates();
				else
					table.EndUpdates();
			};
			var tableSource = new MyTableViewSource(table);
			table.Source = tableSource; 

			var set = this.CreateBindingSet<FirstView, FirstViewModel>();
			set.Bind(tableSource).To(vm => vm.Items);
			set.Apply();
		}

	}

	public class MyTableViewSource : MvxTableViewSource
	{
		public MyTableViewSource(UITableView tableView)
			: base(tableView)
		{
			tableView.RegisterClassForCellReuse(typeof(UITableViewCell), "MyTableViewSource");
			UseAnimations = true;
			AddAnimation = UITableViewRowAnimation.Top;
			RemoveAnimation = UITableViewRowAnimation.Top;
		}

		protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
		{
			var cell = tableView.DequeueReusableCell("MyTableViewSource", indexPath);

			cell.TextLabel.Text = item as string;

			return cell;
		}
	}
}
