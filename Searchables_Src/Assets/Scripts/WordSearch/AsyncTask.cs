using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

namespace WordSearch
{
	public class AsyncTask : MonoBehaviour
	{
		private class TaskInfo
		{
			private bool isDone;

			private object isDoneLock = new object();

			private object _OutData_k__BackingField;

			public bool IsDone
			{
				get
				{
					object obj = this.isDoneLock;
					bool result;
					lock (obj)
					{
						result = this.isDone;
					}
					return result;
				}
				set
				{
					object obj = this.isDoneLock;
					lock (obj)
					{
						this.isDone = value;
					}
				}
			}

			public object OutData
			{
				get;
				set;
			}
		}

		public delegate object Task(object inData);

		private sealed class _Run_c__AnonStorey0
		{
			internal AsyncTask.Task task;

			internal object data;

			internal AsyncTask _this;

			internal void __m__0()
			{
				this._this.taskInfo.OutData = this.task(this.data);
				this._this.taskInfo.IsDone = true;
			}
		}

		private AsyncTask.TaskInfo taskInfo;

		private Action<object> onFinishedCallback;

		private void Update()
		{
			if (this.taskInfo != null && this.taskInfo.IsDone)
			{
				this.onFinishedCallback(this.taskInfo.OutData);
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}

		public static void Start(object inData, AsyncTask.Task task, Action<object> onFinishedCallback)
		{
			new GameObject("AsyncTask", new Type[]
			{
				typeof(AsyncTask)
			}).GetComponent<AsyncTask>().Run(inData, task, onFinishedCallback);
		}

		public void Run(object data, AsyncTask.Task task, Action<object> onFinishedCallback)
		{
			this.onFinishedCallback = onFinishedCallback;
			this.taskInfo = new AsyncTask.TaskInfo();
			Thread thread = new Thread(delegate()
			{
				this.taskInfo.OutData = task(data);
				this.taskInfo.IsDone = true;
			});
			thread.Start();
		}
	}
}
