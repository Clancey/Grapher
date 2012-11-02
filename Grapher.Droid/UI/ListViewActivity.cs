
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Grapher
{
	[Activity (Label = "ListViewActivity")]			
	public class ListViewActivity : ListActivity
	{
		public List<Telemetry> Data = new List<Telemetry>();
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
			
			//TelemetryService.Instance.StartListening();
			this.ListAdapter = new ListViewAdapter(this);
		}
		protected override void OnResume ()
		{
			base.OnResume ();
			reloadData();

		}

		void reloadData()
		{
			var since = DateTime.UtcNow.AddMinutes(-1);
			Database.Main.GetCo2Async(1,since).ContinueWith(t=> {
				this.RunOnUiThread(() => {
					Data = t.Result.Cast<Telemetry>().ToList();
					ListAdapter = new ListViewAdapter(this);
				});
			});
		}
		public class ListViewAdapter : BaseAdapter<Telemetry>
		{
			ListViewActivity Parent;

			public ListViewAdapter (ListViewActivity parent)
			{
				Parent = parent;
			}

			#region implemented abstract members of BaseAdapter

			public override long GetItemId (int position)
			{
				return 0;
			}

			public override View GetView (int position, View convertView, ViewGroup parent)
			{
				View view = convertView; // re-use an existing view, if one is available
				if (view == null) // otherwise create a new one
					view = Parent.LayoutInflater.Inflate (Android.Resource.Layout.SimpleListItem1, null);
				var tel = Parent.Data[position];
				view.FindViewById<TextView> (Android.Resource.Id.Text1).Text = string.Format("Name:{0} Value:{1}",tel.Name,tel.Value);
				return view;
			}

			public override int Count {
				get {
					return Parent.Data.Count;
				}
			}

			#endregion

			#region implemented abstract members of BaseAdapter

			public override Telemetry this [int position] {
				get {
					return Parent.Data[position];
				}
			}

			#endregion
		}
	}
}

