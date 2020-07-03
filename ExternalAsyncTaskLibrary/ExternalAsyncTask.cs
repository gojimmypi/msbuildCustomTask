using System;
using System.IO;
using System.Security;
using System.Collections;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System.Threading;
using System.Threading.Tasks;
using Task = Microsoft.Build.Utilities.Task;

namespace Microsoft.Build.Tasks
{
    /*
     * Class: ExternalAsyncTask
     *
     * An MSBuild task 
     *
     */
    public class ExternalAsyncTask : Task
    {
        // The Required attribute indicates the following to MSBuild:
        //	     - if the parameter is a scalar type, and it is not supplied, fail the build immediately
        //	     - if the parameter is an array type, and it is not supplied, pass in an empty array
        // In this case the parameter is an array type, so if a project fails to pass in a value for the
        // Directories parameter, the task will get invoked, but this implementation will do nothing,
        // because the array will be empty.

        int _WaitDuration = 0;
        [Required]
        public int WaitDuration // this is a project file parameter:  <ExternalAsyncTask WaitDuration="10000" /> 
        {
            get
            {
                return _WaitDuration;
            }

            set
            {
                _WaitDuration = value;
            }
        }
        
        // Directories to create.

        private ITaskItem[] directories;
        private ITaskItem[] directoriesCreated;

        public ITaskItem[] Directories
        {
            get
            {
                return directories;
            }

            set
            {
                directories = value;
            }
        }

        // The Output attribute indicates to MSBuild that the value of this property can be gathered after the
        // task has returned from Execute(), if the project has an <Output> tag under this task's element for
        // this property.
        [Output]
        // A project may need the subset of the inputs that were actually created, so make that available here.
        public ITaskItem[] DirectoriesCreated
        {
            get
            {
                return directoriesCreated;
            }
        }


        public class ThreadWithState
        {
            // State information used in the task.
            private TaskLoggingHelper Log;

            // The constructor obtains the state information.
            public ThreadWithState(TaskLoggingHelper l)
            {
                Log = l;
            }

            // The thread procedure performs the task, such as formatting
            // and printing a document.

            private static async Task<int> FryEggsAsync(TaskLoggingHelper la)
            {
                la.LogMessage(MessageImportance.High,"Warming the egg pan...");
                await System.Threading.Tasks.Task.Delay(3000);
                la.LogMessage(MessageImportance.High, "cracking eggs");
                la.LogMessage(MessageImportance.High, "cooking the eggs ...");
                await System.Threading.Tasks.Task.Delay(3000);
                la.LogMessage(MessageImportance.High, "Put eggs on plate");

                return 0;
            }

            static async System.Threading.Tasks.Task DoStuff(TaskLoggingHelper l)
            {
                int res = await FryEggsAsync(l);
            }

            public void ThreadProc()
            {
                Console.WriteLine("Hello from ThreadProc");
                _ = DoStuff(Log);
                Log.LogMessage(MessageImportance.High, "ThreadProc! ");
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine("Working thread...");
                    Thread.Sleep(1000);
                    Log.LogMessage(MessageImportance.High, "ThreadProc loop! " + i.ToString());
                }
            }
        }

        /// <summary>
        /// Execute is part of the Microsoft.Build.Framework.ITask interface.
        /// When it's called, any input parameters have already been set on the task's properties.
        /// It returns true or false to indicate success or failure.
        /// </summary>
        public override bool Execute()
        {
            Console.WriteLine("Execute! Begin ExternalAsyncTask... writeline");
            Log.LogMessage(MessageImportance.High, "Execute! Begin ExternalAsyncTask... LogMessage");

            // parameterized async thread, see: https://docs.microsoft.com/en-us/dotnet/standard/threading/creating-threads-and-passing-data-at-start-time#passing-data-to-threads
            ThreadWithState tws = new ThreadWithState(Log);
            Thread t = new Thread(new ThreadStart(tws.ThreadProc));
            t.Start();

            Log.LogMessage(MessageImportance.High, "Waiting " + _WaitDuration.ToString() + "ms");
            Thread.Sleep(_WaitDuration);
            Log.LogMessage(MessageImportance.High, "Done waiting! ");

            return !Log.HasLoggedErrors;
        }
    }
}