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
					}),
				},
				new Section(){
					new StringElement("Table",delegate{
						Console.WriteLine("Tables tapped");
					}),
				},
				new Section(){
					new StringElement("Play Movie",delegate{
						Console.WriteLine("Play Movie");
					}),
				}
			};
		}
	}
}

