//
// Alerts sample in C#
//

using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Drawing;

using MonoCatalog;

namespace MonoCatalog {
	
	public partial class AlertsViewController : UITableViewController {
	
		// Load our definition from the NIB file
		public AlertsViewController () : base ("AlertsViewController", null)
		{
		}
	
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			Title = "Alerts";

			var demo = new AlertsViewDemo (View);
	
			TableView.DataSource    = demo.CreateDataSource ();
			TableView.Delegate      = demo.CreateDelegate ();
		}
	}
}
