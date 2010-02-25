
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using MonoTouch.CoreGraphics;
using MonoTouch.CoreText;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace MonoCatalog.iPad {

	public partial class CoreTextController_Pad : UITableViewController
	{
		// The IntPtr and NSCoder constructors are required for controllers that need
		// to be able to be created from a xib rather than from managed code

		public CoreTextController_Pad (IntPtr handle) : base(handle)
		{
			Initialize ();
		}

		[Export("initWithCoder:")]
		public CoreTextController_Pad (NSCoder coder) : base(coder)
		{
			Initialize ();
		}

		public CoreTextController_Pad () : base ("CoreTextController_Pad", null)
		{
			Initialize ();
		}

		void Initialize ()
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			Title = "CoreText Demos";
			TableView.DataSource    = new CoreTextDemo.ItemsDataSource ();
			TableView.Delegate      = new CoreTextDemo.ItemsTableDelegate ();
		}
	}
}
