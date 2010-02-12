
using System;
using System.Collections.Generic;
using System.Drawing;

using MonoTouch.CoreGraphics;
using MonoTouch.CoreText;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace MonoCatalog {

	public partial class CoreTextController : UITableViewController
	{
		// The IntPtr and NSCoder constructors are required for controllers that need
		// to be able to be created from a xib rather than from managed code

		public CoreTextController (IntPtr handle) : base(handle)
		{
			Initialize ();
		}

		[Export("initWithCoder:")]
		public CoreTextController (NSCoder coder) : base(coder)
		{
			Initialize ();
		}

		public CoreTextController () : base ("CoreTextController", null)
		{
			Initialize ();
		}

		void Initialize ()
		{
		}

		class SectionInfo {
			public string Title;
			public Type ViewType;
			public float Height = 50f;
		}

		static SectionInfo [] Sections = new[]{
			new SectionInfo { Title = "2-1 Simple Paragraphs",  ViewType = typeof (SimpleParagraphsView) },
			new SectionInfo { Title = "2-2 Simple Text Labels", ViewType = typeof (SimpleTextLabelsView) },
			new SectionInfo { Title = "2-3 Columnar Layout",    ViewType = typeof (ColumnarLayoutView), Height = 100f },
			new SectionInfo { Title = "2-4 Manual Line Breaking",   ViewType = typeof (ManualLineBreakingView) },
		};

		class ItemsTableDelegate : UITableViewDelegate
		{
			//
			// Override to provide the sizing of the rows in our table
			//
			public override float GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
			{
				return Sections [indexPath.Section].Height;
			}
		}

		class ItemsDataSource : UITableViewDataSource {

			public override int NumberOfSections (UITableView tableView)
			{
				return Sections.Length;
			}

			public override string TitleForHeader (UITableView tableView, int section)
			{
				return Sections [section].Title;
			}

			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
			{
				var section = Sections [indexPath.Section];
				return CoreTextController.GetCell (section.ViewType, tableView, indexPath);
			}

			public override int RowsInSection (UITableView tableview, int section)
			{
				return 1;
			}
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			Title = "CoreText Demos";
			TableView.DataSource    = new ItemsDataSource ();
			TableView.Delegate      = new ItemsTableDelegate ();
		}

		static UITableViewCell GetCell (Type viewType, UITableView tableView, NSIndexPath indexPath)
		{
			const int TagMask = 0x0DEDBEEF;

			var id  = Sections [indexPath.Section].Title;
			var tag = indexPath.Section | TagMask;

			var cell = tableView.DequeueReusableCell (id);
			if (cell == null) {
				cell = new UITableViewCell (UITableViewCellStyle.Default, id);
			}
			else {
				RemoveViewWithTag (cell, tag);
			}

			var view = (UIView) Activator.CreateInstance (viewType);
			view.BackgroundColor    = UIColor.Clear;
			view.Frame              = new RectangleF (5, 5, tableView.Bounds.Width - 30, Sections [indexPath.Section].Height - 10);
			view.Tag                = tag;

			cell.ContentView.AddSubview (view);

			return cell;
		}

		static void RemoveViewWithTag (UITableViewCell cell, int tag)
		{
			var u = cell.ContentView.ViewWithTag (tag);
			Console.Error.WriteLine ("# Removing view: {0}", u);
			if (u != null)
				u.RemoveFromSuperview ();
		}

		class SimpleParagraphsView : UIView {

			public override void Draw (RectangleF rect)
			{
				Console.WriteLine ("# Draw: rect={{X={0}, Y={1}, Width={2}, Height={3}}}", rect.X, rect.Y, rect.Width, rect.Height);
				// Initialize a graphics context and set the text matrix to a known value
				var context = UIGraphics.GetCurrentContext ();
				// context.TextMatrix = CGAffineTransform.MakeIdentity ();
				context.TextMatrix = CGAffineTransform.MakeScale (1f, -1f);

	            // initialize a rectangular path
				var path = new CGPath ();
				// path.AddRect (new RectangleF (10.0f, 10.0f, 200.0f, 30.0f));
				path.AddRect (rect);
				RectangleF r;
				Console.WriteLine ("path.IsRect={0}; Width={1}, Height={2}", path.IsRect (out r), r.Width, r.Height);

				// initialize an attributed string
				var attrString = new NSMutableAttributedString ("We hold this truth to be self evident, that everyone is created equal.");
	            // use a red font for the first 50 chars
				attrString.AddAttributes(new CTStringAttributes () {
						ForegroundColor = new CGColor (
							CGColorSpace.CreateDeviceRGB(),
							new[]{1.0f, 0.0f, 0.0f, 0.8f}
						),
					}, new NSRange (0, 50));

				// Create the framesetter with the attributed string
				using (var framesetter = new CTFramesetter (attrString)) {
					NSRange fitRange;
					var size = framesetter.SuggestFrameSize (new NSRange (), null, new SizeF (200f, 200f), out fitRange);
					Console.WriteLine ("fitRange.Location={0}; fitRange.Length={1}", fitRange.Location, fitRange.Length);
					Console.WriteLine ("Size: Width={0}; Height={1}", size.Width, size.Height);

					// Create the frame and draw it into the graphics context
					using (var frame = framesetter.GetFrame (new NSRange (0, 70), path, null))
						frame.Draw (context);
					context.ShowText ("hello, world!");
				}
			}
		}

		class SimpleTextLabelsView : UIView {

			public override void Draw (RectangleF rect)
			{
				var context = UIGraphics.GetCurrentContext ();
				context.TextMatrix = CGAffineTransform.MakeScale (1f, -1f);
				context.TextPosition = new PointF (10f, base.Center.Y);
				var attrString = new NSAttributedString ("my Arial 20 label!", new CTStringAttributes () {
			        Font = new CTFont ("Arial", 20f),
				});
				using (var line = new CTLine (attrString))
					line.Draw (context);
			}
		}

		class ColumnarLayoutView : UIView {

			const int ColumnCount = 3;

			CGPath[] CreateColumns ()
			{
				var bounds = new RectangleF (0, 0, Bounds.Width, Bounds.Height);

				var columnRects = new RectangleF [ColumnCount];

				// Start by setting the first column to cover the entire view
				columnRects[0] = bounds;
				// Divide the columns equally across the frame's width.
				var columnWidth = Bounds.Width / ColumnCount;
				for (int i = 0; i < columnRects.Length - 1; ++i) {
					columnRects [i].Divide (columnWidth, CGRectEdge.MinXEdge, out columnRects [i], out columnRects[i+1]);
				}

				var paths = new CGPath [columnRects.Length];
				for (int i = 0; i < columnRects.Length; ++i) {
					paths [i] = new CGPath ();
					paths [i].AddRect (columnRects[i].Inset (10f, 10f));
				}

				return paths;
			}

			public override void Draw (RectangleF rect)
			{
				// Initialize a graphics context and set the text matrix to a known value
				var context = UIGraphics.GetCurrentContext ();
				context.TextMatrix = CGAffineTransform.MakeScale (1f, -1f);

				var value = "This is the string that will be split across " +
					ColumnCount + " columns.  This code brought to you by " +
					"ECMA 334, ECMA 335 and the Mono Team at Novell.";

				var framesetter = new CTFramesetter (new NSAttributedString (value));
				var paths = CreateColumns ();
				var startIndex = 0;
				for (int i = 0; i < paths.Length; ++i) {
					var path = paths [i];

					// Create a frame for this column and draw it
					using (var frame = framesetter.GetFrame (new NSRange (startIndex, 0), path, null)) {
						frame.Draw (context);

						var frameRange = frame.GetVisibleStringRange ();
						startIndex += frameRange.Length;
					}
				}
			}
		}

		class ManualLineBreakingView : UIView {

			public override void Draw (RectangleF rect)
			{
				// Initialize a graphics context and set the text matrix to a known value
				var context = UIGraphics.GetCurrentContext ();
				context.TextMatrix = CGAffineTransform.MakeScale (1f, -1f);

				var attrString = new NSAttributedString ("This is a long string.  You will only see half");
				var typesetter = new CTTypesetter (attrString);

				// Find a break for line from the beginning of the string to the given width
				int start = 0;
				var width = Frame.Width / 2;
				Console.WriteLine ("# ManualBreak: width={0}", width);
				width = 125;
				var count = typesetter.SuggestLineBreak (start, width);
				Console.WriteLine ("# ManualBreak: count={0}", count);

				// use the character count (to the break) to create the line:
				using (var line = typesetter.GetLine (new NSRange (start, count))) {
					// Get the offset needed to center the line:
					var flush = 0.5f;   // centered
					var penOffset = line.GetPenOffsetForFlush (flush, Frame.Width);
					Console.WriteLine ("# ManualBreak: pen offset={0}", penOffset);

					// Move the given text drawing position by the calculated offset and draw
					var curPosition = context.TextPosition;
					Console.WriteLine ("# ManualBreak: curPosition.X={0}", curPosition.X);
					context.TextPosition = new PointF (curPosition.X + (float) penOffset, base.Center.Y);
					line.Draw (context);

					// move the index beyond the line break; why?
					start += count;
				}
			}
		}
	}
}
