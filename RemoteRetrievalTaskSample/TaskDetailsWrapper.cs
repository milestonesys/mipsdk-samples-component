using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Selectors;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoOS.Platform;
using VideoOS.Platform.ConfigurationItems;

namespace RemoteRetrievalTaskSample
{
    class TaskDetailsWrapper
    {
        private TaskDetail _taskDetail;
        private ServerTask _task;
        private ServerId _serverId;
        public TaskDetailsWrapper(IConfigurationItem task, ServerId serverId) 
        {
            _serverId = serverId;
            _taskDetail = task as TaskDetail;
        }

        public TaskDetailsWrapper(ServerTask task, ServerId serverId)
        {
            _serverId = serverId;
            _task = task;
            task.GetTask(task.Path);
        }

        public int Progress
        {
            get
            {
                string progress =  _taskDetail?.GetProperty("Progress") ?? _task?.GetProperty("Progress");
                return string.IsNullOrEmpty(progress) ? 0 : int.Parse(progress);
            }
        }
        public StateEnum State
        {
            get
            {
                StateEnum? stateEnum = _taskDetail?.State;
                return stateEnum ?? _task.State ;
            }
        }

        const string TaskStartTimeKey = "TaskStartTime";
        public string TaskStartTime
        {
            get
            {
                string taskStartTime = _taskDetail?.GetProperty(TaskStartTimeKey) ?? _task?.GetProperty(TaskStartTimeKey);
                return string.IsNullOrEmpty(taskStartTime) ? "Unknown Task Start Time" : taskStartTime;
            }
        }
        const string TaskEndTimeKey = "TaskEndTime";
        public string TaskEndTime
        {
            get
            {
                string taskEndTime = _taskDetail?.GetProperty(TaskEndTimeKey) ?? _task?.GetProperty(TaskEndTimeKey);
                return string.IsNullOrEmpty(taskEndTime) ? "Unknown End Time" : taskEndTime;
            }
        }

        const string StartTimeKey = "StartTime";
        public string StartTime
        {
            get
            {
                string startTime = _taskDetail?.GetProperty(StartTimeKey) ?? _task?.GetProperty(StartTimeKey);
                return string.IsNullOrEmpty(startTime) ? "Unknown Start Time" : startTime;
            }
        }

        const string EndTimeKey = "EndTime";
        public string EndTime
        {
            get
            {
                string endTime = _taskDetail?.GetProperty(EndTimeKey) ?? _task?.GetProperty(EndTimeKey);
                return string.IsNullOrEmpty(endTime) ? "Unknown End Time" : endTime;
            }
        }

        const string DevicePathKey = "Device";
        public string DevicePath
        {
            get
            {
                string devicePath = _taskDetail?.GetProperty(DevicePathKey) ?? _task?.GetProperty(DevicePathKey);
                return string.IsNullOrEmpty(devicePath) ? "Unknown Device Path" : devicePath;
            }
        }

        const string UserNameKey = "UserName";
        public string UserName
        {
            get
            {
                string userName = _taskDetail?.GetProperty(UserNameKey) ?? _task?.GetProperty(UserNameKey);
                return string.IsNullOrEmpty(userName) ? "Unknown User" : userName;
            }
        }

        public string DeviceName
        {
            get
            {
                return (new Camera(_serverId, DevicePath)).DisplayName;
            }
        }

        public void Stop()
        {
            if (_taskDetail != null)
            {
                _taskDetail.Stop();
            }            
        }
        public void Cleanup()
        {
            if (_taskDetail != null)
            {
                _taskDetail.Cleanup();
            }
        }
        public void UpdateState()
        {
            if (_taskDetail != null)
            {
                _taskDetail.UpdateState();
            }
            else if(_task != null)
            {
                _task.UpdateState();
            }
        }
    }
}
