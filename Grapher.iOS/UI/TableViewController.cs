using System;
using MonoTouch.UIKit;

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

