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
	[MonoTouch.Foundation.Register("CoreTextController_Pad")]
	public partial class CoreTextController_Pad {
		
		private MonoTouch.UIKit.UINavigationBar __mt_navigationBar;
		
		private MonoTouch.UIKit.UIView __mt_view;
		
		private MonoTouch.UIKit.UITableView __mt_tableView;
		
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
		
		[MonoTouch.Foundation.Connect("view")]
		private MonoTouch.UIKit.UIView view {
			get {
				this.__mt_view = ((MonoTouch.UIKit.UIView)(this.GetNativeField("view")));
				return this.__mt_view;
			}
			set {
				this.__mt_view = value;
				this.SetNativeField("view", value);
			}
		}
		
		[MonoTouch.Foundation.Connect("tableView")]
		private MonoTouch.UIKit.UITableView tableView {
			get {
				this.__mt_tableView = ((MonoTouch.UIKit.UITableView)(this.GetNativeField("tableView")));
				return this.__mt_tableView;
			}
			set {
				this.__mt_tableView = value;
				this.SetNativeField("tableView", value);
			}
		}
	}
}
