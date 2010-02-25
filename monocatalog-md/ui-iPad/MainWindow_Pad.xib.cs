
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

	partial class ContentViewController : UIViewController {
		public ContentViewController (IntPtr handle)
			: base (handle)
		{
		}

		internal SplitViewControllerDelegate SplitViewDelegate;

		UIViewController detailItem;
		public void SetDetailItem (UIViewController detailItem)
		{
			if (this.detailItem != detailItem) {
				this.detailItem = detailItem;
				Console.WriteLine ("# SetDetailItem: detailItem? {0}; NavigationController? {1}; ParentViewController? {2}",
					detailItem != null, NavigationController != null, ParentViewController != null);
				// NavigationController.PresentModalViewController (detailItem, true);
				NavigationController.PushViewController (detailItem, true);
				// navigationBar.TopItem.Title = detailItem.Title;
				// navigationBar?
			}
			if (SplitViewDelegate.popoverController != null)
				SplitViewDelegate.popoverController.Dismiss (true);
		}
	}

	partial class DetailViewController : UINavigationController {

		public DetailViewController (IntPtr handle)
			: base(handle)
		{
		}

		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			return true;
		}
	}

	partial class RootViewController : UITableViewController {

		public RootViewController (IntPtr handle)
			: base (handle)
		{
			Console.WriteLine ("RootViewController created!");
		}

		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			return true;
		}

		public override SizeF ContentSizeForViewInPopover {
			get {return new SizeF (320f, 600f);}
			set {/* ignore */}
		}

		static NSString kCellIdentifier = new NSString ("MyIdentifier");

		struct Sample {
			public string Title;
			public UIViewController Controller;

			public Sample (string title, UIViewController controller)
			{
				Title = title;
				Controller = controller;
			}
		}

		Sample [] samples;

		//
		// The data source for our TableView
		//
		class DataSource : UITableViewDataSource {
			RootViewController mvc;

			public DataSource (RootViewController mvc)
			{
				this.mvc = mvc;
			}

			public override int RowsInSection (UITableView tableView, int section)
			{
				return mvc.samples.Length;
			}

			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
			{
				var cell = tableView.DequeueReusableCell (kCellIdentifier);
				if (cell == null){
					cell = new UITableViewCell (UITableViewCellStyle.Default, kCellIdentifier);
					cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
				}
				cell.TextLabel.Text = mvc.samples [indexPath.Row].Title;
				return cell;
			}
		}
	
		//
		// This class receives notifications that happen on the UITableView
		//
		class TableDelegate : UITableViewDelegate {
			RootViewController mvc;
			
			public TableDelegate (RootViewController mvc)
			{
				this.mvc = mvc;
			}

			UIView current;
			
			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{
				Console.WriteLine ("MonoCatalog: Row selected {0}", indexPath.Row);
				
				var cont = mvc.samples [indexPath.Row].Controller;
				// mvc.detailViewController.PresentModalViewController (cont, true);
				if (current != null)
					current.RemoveFromSuperview ();
				mvc.detailViewController.Add (current = cont.View);
				// mvc.detailViewController.ParentViewController.PresentModalViewController (cont, true);
				// mvc.contentViewController.SetDetailItem (cont);
				// mvc.NavigationController.PushViewController (cont, true);
			}
		}
		
		public override void ViewDidLoad ()
		{
			var c = UIScreen.MainScreen;
			Console.WriteLine ("# screen modes:");
			foreach (var m in c.AvailableModes) {
				Console.WriteLine ("#  Width={0}; Height={1}", m.Size.Width, m.Size.Height);
			}
			Console.WriteLine ("# screen bounds: X={0}; Y={1}; Width={2}; Height={3}",
					c.Bounds.X, c.Bounds.Y, c.Bounds.Width, c.Bounds.Height);
			base.ViewDidLoad ();
			Title = "MonoTouch UICatalog";
			var s = new List<Sample> {
				// new Sample ("Alerts", new AlertsViewController ()),
				// new Sample ("Address Book", new AddressBookController ()),
				// new Sample ("Buttons", new ButtonsViewController ()),
				// new Sample ("Controls", new ControlsViewController ()),
			};
			// FIXME: what about iPhoneOS 3.3, etc.?
			if (UIDevice.CurrentDevice.SystemVersion.StartsWith ("3.2")) {
				s.Add (new Sample ("CoreText", new CoreTextController_Pad ()));
			}
			#if TODO
			s.AddRange (new[]{
				new Sample ("Images", new ImagesViewController ()),
				new Sample ("Mono.Data.Sqlite", new MonoDataSqliteController ()),
				new Sample ("Pickers", new PickerViewController ()),
				new Sample ("Segments", new SegmentViewController ()),
				new Sample ("Searchbar", new SearchBarController ()),
				new Sample ("TextField", new TextFieldController ()),
				new Sample ("TextView", new TextViewController ()),
				new Sample ("Toolbar", new ToolbarViewController ()),
				new Sample ("Transitions", new TransitionViewController ()),
				new Sample ("Web", new WebViewController ())
			});
			#endif
			samples = s.ToArray ();

			TableView.Delegate = new TableDelegate (this);
			TableView.DataSource = new DataSource (this);
	
			NavigationItem.BackBarButtonItem = new UIBarButtonItem () { Title = "Back" };
		}

	}

	public partial class SplitViewAppDelegate : UIApplicationDelegate {

		public SplitViewAppDelegate (IntPtr handle)
			: base (handle)
		{
		}

		public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
		{
			Console.WriteLine ("# iPad SplitViewAppDelegate: finished launching!");
			#if false
			detailViewController.SplitViewDelegate = new SplitViewControllerDelegate () {
				// NavigationBar = detailNavigationBar,
			};
			#endif
			splitViewController.Delegate = new SplitViewControllerDelegate ();
			// Add the split view controller's view to the window and display.
			window.AddSubview (splitViewController.View);
			window.MakeKeyAndVisible ();

			return true;
		}
	}

	class SplitViewControllerDelegate : UISplitViewControllerDelegate {

		internal UIPopoverController popoverController;

		public UINavigationBar NavigationBar {get; set;}

		public override void WillHideViewController (UISplitViewController svc, UIViewController aViewController, UIBarButtonItem barButtonItem, UIPopoverController pc)
		{
			Console.WriteLine ("# OnFoo...");
			barButtonItem.Title = "Root List";
			// NavigationBar.TopItem.SetLeftBarButtonItem (barButtonItem, true);
			popoverController = pc;
		}

		public override void WillShowViewController (UISplitViewController svc, UIViewController aViewController, UIBarButtonItem button)
		{
			Console.WriteLine ("# OnBar...");
			// NavigationBar.TopItem.SetLeftBarButtonItem (null, true);
			popoverController = null;
		}
	}
}
