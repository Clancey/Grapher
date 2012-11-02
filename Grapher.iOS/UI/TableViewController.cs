using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Grapher
{
	public class TableViewController : UITableViewController
	{
		public TableViewController ()
		{

		}
		public List<CO2> Data = new List<CO2>();
		static bool isIos6 = new Version(UIDevice.CurrentDevice.SystemVersion).Major >= 6;
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			this.TableView.Source = new MyDataSource(this);
			if(!isIos6)
				return;
			this.RefreshControl = new UIRefreshControl();
			RefreshControl.AttributedTitle = new NSAttributedString("Pull down to refresh...");
			RefreshControl.ValueChanged += delegate {
				reloadData();
				RefreshControl.AttributedTitle = new NSAttributedString(String.Format ("Last Updated: {0:g}",DateTime.Now));
				RefreshControl.EndRefreshing();
			};

		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			reloadData();
		}
		bool isLoading;
		void reloadData()
		{
			var since = DateTime.UtcNow.AddMinutes(-1);
			Database.Main.GetCo2Async(1,since).ContinueWith(t=> {
				BeginInvokeOnMainThread(() => {
					Data = t.Result;
					TableView.ReloadData();
				});
			});
		}

		public class MyDataSource : UITableViewSource
		{
			TableViewController Parent;
			public MyDataSource(TableViewController parent)
			{
				Parent = parent;
			}

			#region implemented abstract members of UITableViewSource
			public override int RowsInSection (UITableView tableview, int section)
			{
				return Parent.Data.Count;
			}
			public override UITableViewCell GetCell (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
			{
				var cell = tableView.DequeueReusableCell("test");
				if(cell == null)
					cell = new UITableViewCell(UITableViewCellStyle.Default,"test");
				var tel = Parent.Data[indexPath.Row];
				cell.TextLabel.Text = string.Format("Name:{0} Value:{1}",tel.Name,tel.Value);
				return cell;
			}
			#endregion
		}
	}
}

