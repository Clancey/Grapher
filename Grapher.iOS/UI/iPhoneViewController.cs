using System;
using MonoTouch.Dialog;
using MonoTouch.UIKit;

namespace Grapher
{
	public class iPhoneViewController : DialogViewController
	{
		public iPhoneViewController () : base(UITableViewStyle.Grouped,null)
		{
			Root = CreateRoot();
		}

		RootElement CreateRoot()
		{
			return new RootElement("Data Collector")
			{
				new Section(){
					new StringElement("Graphs",delegate{
						Console.WriteLine("graphs tapped");
						this.NavigationController.PushViewController(new GraphViewController(),true);
					}),
				},
				new Section(){
					new StringElement("Table",delegate{
						Console.WriteLine("Tables tapped");
						this.NavigationController.PushViewController(new TableViewController(),true);
					}),
				},
				new Section(){
					new StringElement("Play Movie",delegate{
						Console.WriteLine("Play Movie");
						this.NavigationController.PushViewController(new MovieViewController(),true);
					}),
				}
			};
		}
	}
}

