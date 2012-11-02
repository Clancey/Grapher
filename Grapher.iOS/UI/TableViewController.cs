using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace Grapher
{
	public class TableViewController : UITableViewController
	{
		public TableViewController ()
		{

		}
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			this.TableView.Source = new MyDataSource(this);
			this.RefreshControl = new UIRefreshControl();
			RefreshControl.AttributedTitle = new NSAttributedString("Pull down to refresh...");
			RefreshControl.ValueChanged += delegate {
				//TODO: Get data
				TableView.ReloadData();
				RefreshControl.AttributedTitle = new NSAttributedString(String.Format ("Last Updated: {0:g}",DateTime.Now));
				RefreshControl.EndRefreshing();
			};

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
				return 0;
			}
			public override UITableViewCell GetCell (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
			{
				throw new NotImplementedException ();
			}
			#endregion
		}
	}
}

