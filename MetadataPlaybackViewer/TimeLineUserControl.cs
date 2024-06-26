using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using VideoOS.Platform;
using VideoOS.Platform.Data;

namespace MetadataPlaybackViewer
{
	public partial class TimeLineUserControl : UserControl
	{
		/// <summary>
		/// Precision is 5 pixels wide and 1000 for the timeinterval
		/// </summary>
		private Bitmap _timeLine = new Bitmap(24, 1000);

		private Graphics _graphics;

		private DateTime _startTime;
		private DateTime _endTime;

		private DateTime _startCache = DateTime.MaxValue;
		private DateTime _endCache = DateTime.MinValue;

		private DateTime _minRequested = DateTime.MaxValue;
		private DateTime _maxRequested = DateTime.MaxValue;

		private Collection<SequenceData> _cache = new Collection<SequenceData>();
		private Collection<SequenceData> _cacheRecordings = new Collection<SequenceData>();
		private bool _isMotionSequencesSupported;
		private bool _isRecordingSequencesSupported;

		private long _ticksPerPixel;
		private long _ticksPerMouseY;
		private DateTime _currentTime;
		private bool _isRequestInProgress;

		private Item _item;
		private SequenceDataSource _dataSource;

		private object _syncState = new object();		// not really used right now

		public TimeLineUserControl()
		{
			InitializeComponent();
			Init();
		}


		private void Init()
		{
			_graphics = Graphics.FromImage(_timeLine);
			_graphics.FillRectangle(Brushes.Black, 0, 0, _timeLine.Width, _timeLine.Height);

			pictureBox.Image = new Bitmap(_timeLine, pictureBox.Size);
			
			_startCache = DateTime.MaxValue;
			_endCache = DateTime.MinValue;

			_minRequested = DateTime.MaxValue;
			_maxRequested = DateTime.MaxValue;

			_cache = new Collection<SequenceData>();
			_cacheRecordings = new Collection<SequenceData>();
		}

		public Item Item
		{
			get
			{
				return _item;
			}
			set { 
				_item = value;
				Init();

				if (_dataSource != null)
				{
					_dataSource.Close();
					_dataSource = null;
				}

				if (value != null)
				{
					_dataSource = new SequenceDataSource(value);

					List<DataType> availableSequenceTypes = _dataSource.GetTypes();
					_isMotionSequencesSupported = false;
					_isRecordingSequencesSupported = false;
					foreach (DataType dt in availableSequenceTypes)
					{
						if (dt.Id == DataType.SequenceTypeGuids.RecordingSequence)
							_isRecordingSequencesSupported = true;
						if (dt.Id == DataType.SequenceTypeGuids.MotionSequence)
							_isMotionSequencesSupported = true;
					}
				}
			}
		}

		public event EventHandler MouseSetTimeEvent;


		private DateTime _timeBeforeMouseMove;
		public void SetShowTime(DateTime currentTime)
		{
			if (!_isMouseDown)
			{
			    if (currentTime.Kind != DateTimeKind.Local)
			        currentTime = currentTime.ToLocalTime();
				_timeBeforeMouseMove = currentTime;
				if ((_item != null && _dataSource != null))
					SetShowTime(0);
			}
		}

		private void SetShowTime(long offset)
		{
			if (_timeBeforeMouseMove.Ticks == 0 || pictureBox.Height == 0)
				return;

			_currentTime = _timeBeforeMouseMove + TimeSpan.FromTicks(offset);

			_startTime = _currentTime - TimeSpan.FromMinutes(30);
			_endTime = _currentTime + TimeSpan.FromMinutes(30);

			_graphics.Dispose();
			_timeLine = new Bitmap(pictureBox.Width, pictureBox.Height);
			_graphics = Graphics.FromImage(_timeLine);

			_ticksPerPixel = (_endTime.Ticks - _startTime.Ticks) / _timeLine.Height;
			_ticksPerMouseY = (_endTime.Ticks - _startTime.Ticks) / pictureBox.Height;

			_graphics.FillRectangle(Brushes.Black, 0, 0, _timeLine.Width, _timeLine.Height);
			_graphics.DrawLine(Pens.White, new Point(0, _timeLine.Height / 2), new Point(_timeLine.Width, _timeLine.Height / 2));

			pictureBox.Image = new Bitmap(_timeLine, pictureBox.Size);

			labelStartTime.Text = _startTime.ToShortTimeString();
			labelEndTime.Text = _endTime.ToShortTimeString();
			labelCurrent.Text = _currentTime.ToShortTimeString();
			labelCurrent.Location = new Point(labelCurrent.Location.X, pictureBox.Location.Y + pictureBox.Height /2 -8);

			if (_isRequestInProgress == false && _isMouseDown == false)
			{
				if (_startTime < _minRequested)
				{
					TimeSpan ts = TimeSpan.FromHours(1);
					if (_endTime > _minRequested)
						ts = (_startCache - _startTime) - TimeSpan.FromSeconds(5);

					_isRequestInProgress = true;
					if (_isMotionSequencesSupported)
						_dataSource.GetDataAsync(_syncState, _startTime.ToUniversalTime(), TimeSpan.FromSeconds(0), 0, ts, 300,
					                         VideoOS.Platform.Data.DataType.SequenceTypeGuids.MotionSequence,
					                         new AsyncCallbackHandler(SequenceHandler));
					
					if (_isRecordingSequencesSupported)
						_dataSource.GetDataAsync(_syncState, _startTime.ToUniversalTime(), TimeSpan.FromSeconds(0), 0, ts, 300,
					                         VideoOS.Platform.Data.DataType.SequenceTypeGuids.RecordingSequence,
					                         new AsyncCallbackHandler(SequenceRecordingsHandler));
					

					Debug.WriteLine("Seq Request:" + _startTime.ToUniversalTime().ToShortTimeString() + " ... " + ts.ToString());

					if (_startTime < _minRequested)
						_minRequested = _startTime;
					if (_endTime > _maxRequested)
						_maxRequested = _endTime;

				}
				else if (_maxRequested < _endTime)
				{
					DateTime startRequest = _startTime;
					if (_startTime < _maxRequested)
						startRequest = _maxRequested;
					TimeSpan ts = _endTime - startRequest;
					_isRequestInProgress = true;
					if (_isMotionSequencesSupported)
						_dataSource.GetDataAsync(_syncState, startRequest.ToUniversalTime(), TimeSpan.FromSeconds(0), 0, ts, 300,
					                         VideoOS.Platform.Data.DataType.SequenceTypeGuids.MotionSequence,
					                         new AsyncCallbackHandler(SequenceHandler));
					
					if (_isRecordingSequencesSupported)
						_dataSource.GetDataAsync(_syncState, startRequest.ToUniversalTime(), TimeSpan.FromSeconds(0), 0, ts, 300,
											 VideoOS.Platform.Data.DataType.SequenceTypeGuids.RecordingSequence,
					                         new AsyncCallbackHandler(SequenceRecordingsHandler));
					
					Debug.WriteLine("Seq Request:" + startRequest.ToUniversalTime().ToShortTimeString() + " ... " + ts.ToString());

					if (_startTime < _minRequested)
						_minRequested = _startTime;
					if (_endTime > _maxRequested)
						_maxRequested = _endTime;
				}
			}
			DisplayLine();
		}

		private void SequenceHandler(object objects, object syncState)
		{
			if (this.InvokeRequired)
			{
				BeginInvoke(new AsyncCallbackHandler(SequenceHandler), new[] { objects, syncState });
			}
			else
			{
				SequenceData[] result = objects as SequenceData[];

				foreach (SequenceData sd in result)
				{
					_cache.Add(sd);

					DateTime start = sd.EventSequence.StartDateTime.ToLocalTime();
					DateTime end = sd.EventSequence.EndDateTime.ToLocalTime();

					if (end > DateTime.Now)
						end = DateTime.Now;

					if (start < _startCache)
						_startCache = start;
					if (end > _endCache)
						_endCache = end;

					Debug.WriteLine("Seq:" + start.ToLongTimeString() + "..." + end.ToLongTimeString());
				}
				_isRequestInProgress = false;

				if (result.Length>0)
					DisplayLine();
			}
		}

		private void SequenceRecordingsHandler(object objects, object syncState)
		{
			if (this.InvokeRequired)
			{
				BeginInvoke(new AsyncCallbackHandler(SequenceRecordingsHandler), new[] { objects, syncState });
			}
			else
			{
				SequenceData[] result = objects as SequenceData[];

				foreach (SequenceData sd in result)
				{
					_cacheRecordings.Add(sd);

					DateTime start = sd.EventSequence.StartDateTime.ToLocalTime();
					DateTime end = sd.EventSequence.EndDateTime.ToLocalTime();

					if (end > DateTime.Now)
						end = DateTime.Now;

					if (start < _startCache)
						_startCache = start;
					if (end > _endCache)
						_endCache = end;

					Debug.WriteLine("REC:" + start.ToLongTimeString() + "..." + end.ToLongTimeString());
				}
				_isRequestInProgress = false;

				if (result.Length > 0)
					DisplayLine();
			}
		}

		private void DisplayLine()
		{
			_graphics.FillRectangle(Brushes.Black, 0, 0, _timeLine.Width, _timeLine.Height);

			DateTime startData = _minRequested;
			DateTime endData = _maxRequested;
			if (startData < _startTime || startData>endData)
				startData = _startTime;
			if (endData > _endTime || startData>endData)
				endData = _endTime;
			long startOffset = (startData.Ticks - _startTime.Ticks) / _ticksPerPixel;
			long endOffset = (endData.Ticks - _startTime.Ticks) / _ticksPerPixel;

			Brush brushRect = Brushes.Gray;
			_graphics.FillRectangle(brushRect, 4, startOffset - 1, 16, endOffset - startOffset);

			foreach (SequenceData sd in _cacheRecordings)
			{
				DateTime start = sd.EventSequence.StartDateTime.ToLocalTime();
				DateTime end = sd.EventSequence.EndDateTime.ToLocalTime();

				if (start < _endTime || end > _startTime)			// Is the time inside the timeline interval at all?
				{
					if (start < _startTime)							// Is the start just before UI start
						start = _startTime;
					if (end > _endTime)								// Is end just afer UI end
						end = _endTime;

					startOffset = (start.Ticks - _startTime.Ticks) / _ticksPerPixel;
					endOffset = (end.Ticks - _startTime.Ticks) / _ticksPerPixel;

					_graphics.FillRectangle(Brushes.LightGreen, 4, startOffset - 2, 16, endOffset - startOffset+2);
				}

			}
			foreach (SequenceData sd in _cache)
			{
				DateTime start = sd.EventSequence.StartDateTime.ToLocalTime();
				DateTime end = sd.EventSequence.EndDateTime.ToLocalTime();

				if (start < _endTime || end > _startTime)			// Is the time inside the timeline interval at all?
				{
					if (start < _startTime)							// Is the start just before UI start
						start = _startTime;
					if (end > _endTime)								// Is end just afer UI end
						end = _endTime;

					startOffset = (start.Ticks - _startTime.Ticks) / _ticksPerPixel;
					endOffset = (end.Ticks - _startTime.Ticks) / _ticksPerPixel;

					_graphics.FillRectangle(Brushes.Red, 4, startOffset-1, 16, endOffset - startOffset+1);
				}

			}
			_graphics.DrawLine(Pens.White, new Point(0, _timeLine.Height / 2), new Point(_timeLine.Width, _timeLine.Height / 2));
			pictureBox.Image = new Bitmap(_timeLine, pictureBox.Size);
		}

		private int _initialMouseY = 0;
		private bool _isMouseDown;
		private void OnMouseDown(object sender, MouseEventArgs e)
		{
			_initialMouseY = e.Y;
			_isMouseDown = true;
		}

		private void OnMouseMove(object sender, MouseEventArgs e)
		{
			if (_isMouseDown)
			{
				int newY = e.Y;
				int deltaPixels = newY - _initialMouseY;
				long deltaTicks = -deltaPixels*_ticksPerMouseY;
				SetShowTime(deltaTicks);
				if (MouseSetTimeEvent != null)
					MouseSetTimeEvent(this, new TimeEventArgs() {Time = _currentTime});
			}

		}

		private void OnMouseUp(object sender, MouseEventArgs e)
		{
			_isMouseDown = false;
			int newY = e.Y;
			int deltaPixels = newY - _initialMouseY;
			long deltaTicks = -deltaPixels * _ticksPerMouseY;
			_timeBeforeMouseMove = _timeBeforeMouseMove + TimeSpan.FromTicks(deltaTicks);
			if ((_item != null && _dataSource != null))
				SetShowTime(_timeBeforeMouseMove);

			_minRequested = DateTime.MaxValue;
			_maxRequested = DateTime.MinValue;
		}

		private void OnMouseLeave(object sender, EventArgs e)
		{
			_isMouseDown = false;
			_timeBeforeMouseMove = _currentTime;
		}

		private void OnDump(object sender, EventArgs e)
		{
			Debug.WriteLine("--------Dump Recording Sequences ------------");
			string dump = "";
			int lines = 0;
			foreach (SequenceData sd in _cacheRecordings)
			{
				DateTime start = sd.EventSequence.StartDateTime.ToLocalTime();
				DateTime end = sd.EventSequence.EndDateTime.ToLocalTime();

				Debug.WriteLine("REC:" + start.ToLongTimeString() + "..." + end.ToLongTimeString());
				dump += "REC:" + start.ToLongTimeString() + "..." + end.ToLongTimeString() + Environment.NewLine;
				if (++lines > 25)
				{
					dump += " ---- More -----";
					break;
				}
			}
			MessageBox.Show(dump);
		}

	}

	public class TimeEventArgs : EventArgs
	{
		public DateTime Time;
	}
}
