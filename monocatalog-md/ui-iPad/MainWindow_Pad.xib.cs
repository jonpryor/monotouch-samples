
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace MonoCatalog.iPad
{
	public partial class MainWindow_Pad : UIViewController
	{
		#region Constructors

		// The IntPtr and initWithCoder constructors are required for controllers that need 
		// to be able to be created from a xib rather than from managed code

		public MainWindow_Pad (IntPtr handle) : base(handle)
		{
			Initialize ();
		}

		[Export("initWithCoder:")]
		public MainWindow_Pad (NSCoder coder) : base(coder)
		{
			Initialize ();
		}

		public MainWindow_Pad () : base("MainWindow_Pad", null)
		{
			Initialize ();
		}

		void Initialize ()
		{
		}
		
		#endregion
	}

	partial class DetailViewController : UIViewController {

		UIView detailItem;
		UIPopoverController popoverController;

		public DetailViewController (IntPtr handle)
			: base(handle)
		{
		}


		void SetDetailItem (UIView newDetailItem)
		{
			if (detailItem != newDetailItem) {
				detailItem = newDetailItem;
				navigationBar.TopItem.Title = "Insert title here!";
			}
			if (popoverController != null)
				popoverController.Dismiss (true);
		}

		// TODO: UIPopoverControllerDelegate, UISplitViewControllerDelegate
	}

	partial class RootViewController : UITableViewController {

		public RootViewController (IntPtr handle)
			: base (handle)
		{
		}

		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			return true;
		}

		public override SizeF ContentSizeForViewInPopover {
			get {return new SizeF (320f, 600f);}
			set {/* ignore */}
		}

	}

	public partial class SplitViewAppDelegate : UIApplicationDelegate {

		public SplitViewAppDelegate (IntPtr handle)
			: base (handle)
		{
		}

		public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
		{
			// Add the split view controller's view to the window and display.
			window.AddSubview (splitViewController.View);
			window.MakeKeyAndVisible ();

			return true;
		}
	}
}
