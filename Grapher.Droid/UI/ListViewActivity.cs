
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
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
			
			//TelemetryService.Instance.StartListening();
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
				throw new NotImplementedException ();
			}

			public override View GetView (int position, View convertView, ViewGroup parent)
			{
				View view = convertView; // re-use an existing view, if one is available
				if (view == null) // otherwise create a new one
					view = Parent.LayoutInflater.Inflate (Android.Resource.Layout.SimpleListItem1, null);
				//view.FindViewById<TextView> (Android.Resource.Id.Text1).Text = items [position];
				return view;
			}

			public override int Count {
				get {
					throw new NotImplementedException ();
				}
			}

			#endregion

			#region implemented abstract members of BaseAdapter

			public override Telemetry this [int position] {
				get {
					throw new NotImplementedException ();
				}
			}

			#endregion
		}
	}
}

