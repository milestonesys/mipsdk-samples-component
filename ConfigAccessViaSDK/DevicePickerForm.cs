using System;
using System.Collections.Generic;
using System.Windows.Forms;
using VideoOS.Platform;

namespace ConfigAccessViaSDK
{
	public partial class DevicePickerForm : Form
	{
		private FQID[] _selectedFQID = null;

		public DevicePickerForm( )
		{
			InitializeComponent();

			// We could filter on some specific Kinds, e.g. :
			//  itemPickerUserControl.KindFilter = new List<Guid>() { Kind.Camera, Kind.InputEvent }; 
		}
		private void OnLoad(object sender, EventArgs e)
		{
			itemPickerUserControl.Init();
		    itemPickerUserControl.ShowDisabledItems = true;
			List<Item> list1 = Configuration.Instance.GetItems(ItemHierarchy.SystemDefined);			
			itemPickerUserControl.ItemsToSelectFromServer = list1;
			List<Item> list2 = Configuration.Instance.GetItems(ItemHierarchy.UserDefined);
			itemPickerUserControl.ItemsToSelectFromGroup = list2;
		}

		public FQID[] SelectedFQID
		{
			set
			{
				_selectedFQID = value;
				if (value != null)
				{
					List<Item> instances = new List<Item>();
					foreach (FQID fqid in value)
						instances.Add(Configuration.Instance.GetItem(fqid));
					itemPickerUserControl.ItemsSelected = instances;
				}
			}
			get
			{
				return _selectedFQID;
			}
		}

		private void OnOK(object sender, EventArgs e)
		{
			_selectedFQID = new FQID[itemPickerUserControl.ItemsSelected.Count];
			for (int ix = 0; ix < itemPickerUserControl.ItemsSelected.Count; ix++)
				_selectedFQID[ix] = itemPickerUserControl.ItemsSelected[ix].FQID;
			DialogResult = DialogResult.OK;
			this.Close();
		}

		private void OnCancel(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			this.Close();
		}
	}
}
