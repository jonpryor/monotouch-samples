using System;
using MonoCatalog;

using MonoTouch.UIKit;

namespace MonoCatalog.iPad {

	partial class AlertsViewController_Pad : UITableViewController {

		// Load our definition from the NIB file
		public AlertsViewController_Pad ()
			: base ("AlertsViewController_Pad", null)
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