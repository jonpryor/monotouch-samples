// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 2.0.50727.1433
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace MonoCatalog.iPad {
	
	
	// Base type probably should be MonoTouch.UIKit.UIViewController or subclass
	[MonoTouch.Foundation.Register("DetailViewController")]
	public partial class DetailViewController {
		
		private MonoTouch.UIKit.UINavigationBar __mt_navigationBar;
		
		#pragma warning disable 0169
		[MonoTouch.Foundation.Connect("navigationBar")]
		private MonoTouch.UIKit.UINavigationBar navigationBar {
			get {
				this.__mt_navigationBar = ((MonoTouch.UIKit.UINavigationBar)(this.GetNativeField("navigationBar")));
				return this.__mt_navigationBar;
			}
			set {
				this.__mt_navigationBar = value;
				this.SetNativeField("navigationBar", value);
			}
		}
	}
	
	// Base type probably should be MonoTouch.UIKit.UITableViewController or subclass
	[MonoTouch.Foundation.Register("RootViewController")]
	public partial class RootViewController {
		
		private DetailViewController __mt_detailViewController;
		
		#pragma warning disable 0169
		[MonoTouch.Foundation.Connect("detailViewController")]
		private DetailViewController detailViewController {
			get {
				this.__mt_detailViewController = ((DetailViewController)(this.GetNativeField("detailViewController")));
				return this.__mt_detailViewController;
			}
			set {
				this.__mt_detailViewController = value;
				this.SetNativeField("detailViewController", value);
			}
		}
	}
	
	// Base type probably should be MonoTouch.Foundation.NSObject or subclass
	[MonoTouch.Foundation.Register("SplitViewAppDelegate")]
	public partial class SplitViewAppDelegate {
		
		private DetailViewController __mt_detailViewController;
		
		private RootViewController __mt_rootViewController;
		
		private MonoTouch.UIKit.UISplitViewController __mt_splitViewController;
		
		private MonoTouch.UIKit.UIWindow __mt_window;
		
		#pragma warning disable 0169
		[MonoTouch.Foundation.Connect("detailViewController")]
		private DetailViewController detailViewController {
			get {
				this.__mt_detailViewController = ((DetailViewController)(this.GetNativeField("detailViewController")));
				return this.__mt_detailViewController;
			}
			set {
				this.__mt_detailViewController = value;
				this.SetNativeField("detailViewController", value);
			}
		}
		
		[MonoTouch.Foundation.Connect("rootViewController")]
		private RootViewController rootViewController {
			get {
				this.__mt_rootViewController = ((RootViewController)(this.GetNativeField("rootViewController")));
				return this.__mt_rootViewController;
			}
			set {
				this.__mt_rootViewController = value;
				this.SetNativeField("rootViewController", value);
			}
		}
		
		[MonoTouch.Foundation.Connect("splitViewController")]
		private MonoTouch.UIKit.UISplitViewController splitViewController {
			get {
				this.__mt_splitViewController = ((MonoTouch.UIKit.UISplitViewController)(this.GetNativeField("splitViewController")));
				return this.__mt_splitViewController;
			}
			set {
				this.__mt_splitViewController = value;
				this.SetNativeField("splitViewController", value);
			}
		}
		
		[MonoTouch.Foundation.Connect("window")]
		private MonoTouch.UIKit.UIWindow window {
			get {
				this.__mt_window = ((MonoTouch.UIKit.UIWindow)(this.GetNativeField("window")));
				return this.__mt_window;
			}
			set {
				this.__mt_window = value;
				this.SetNativeField("window", value);
			}
		}
	}
}
