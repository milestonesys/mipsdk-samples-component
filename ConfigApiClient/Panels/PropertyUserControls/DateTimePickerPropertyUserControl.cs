using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ConfigAPIClient;
using VideoOS.ConfigurationAPI;

namespace ConfigAPIClient.Panels
{
	public partial class DateTimePickerPropertyUserControl : PropertyUserControl
	{
		private int _origY;
        private bool wasUnspecified = false;
        private bool _init;

        public DateTimePickerPropertyUserControl()
        { }

        public DateTimePickerPropertyUserControl(Property property)
			: base(property)
		{
			InitializeComponent();

			labelOfProperty.Text = property.DisplayName;
            _init = true;

            DateTime dateTime;
            if (DateTime.TryParse(property.Value, out dateTime))
            {
                wasUnspecified = dateTime.Kind == DateTimeKind.Unspecified;
                if (dateTime.Kind == DateTimeKind.Utc)
                    dateTime = dateTime.ToLocalTime();
                try
                {
                    dateTimePicker1.Value = dateTime;
                    dateTimePicker2.Value = dateTime;
                } catch 
                {

                }
            }
            else
            {
                wasUnspecified = true;
                dateTimePicker1.Value = DateTime.Today;
                dateTimePicker2.Value = DateTime.Now;
            }            

            dateTimePicker2.ShowUpDown = true;

            if (property.ValueType == "Time")
            {
                dateTimePicker1.Visible = false;
                dateTimePicker2.Format = DateTimePickerFormat.Time;
            }
            if (property.ValueType == "Date")
            {
                dateTimePicker2.Visible = false;
                dateTimePicker1.Format = DateTimePickerFormat.Short;
            }
            if (property.ValueType == "DateTime")
            {
                dateTimePicker1.Visible = true;
                dateTimePicker2.Visible = true;
            }
            
            HasChanged = false;
            _origY = dateTimePicker1.Left;

            _init = false;
		}

		internal override int LeftIndent
		{
            set { dateTimePicker1.Left = _origY - value; }
		}

        private void OnValueChanged(object sender, EventArgs e)
        {
            if (_init) return;

            if (base.Property.ValueType == "Time")
            {
                Property.Value = dateTimePicker2.Value.ToString("HH:mm:ss");
            }
            else
            if (base.Property.ValueType == "Date")
            {
                Property.Value = dateTimePicker1.Value.Date.ToString("yyyy-MM-dd");
            }
            else
            {
                DateTime dateTime = dateTimePicker1.Value.Date + dateTimePicker2.Value.TimeOfDay;
                if (wasUnspecified)
                    Property.Value = dateTime.ToString("yyyy-MM-ddTHH:mm:ss");
                else
                    Property.Value = dateTime.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss");
            }
            ValueChanged(this, e);
        }
	}
}
