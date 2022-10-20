namespace TechCon.Tests.Utils.Helpers
{
    public static class AsyncHelper
    {
        private static readonly TaskFactory _taskFactory = new TaskFactory(CancellationToken.None,
                                                                           TaskCreationOptions.None,
                                                                           TaskContinuationOptions.None,
                                                                           TaskScheduler.Default);
        
        /// <summary>
        /// Executes an async Task method which has a void return value synchronously
        /// USAGE: AsyncUtil.RunSync(() => AsyncMethod());
        /// </summary>
        /// <param name="task">Task method to execute</param>
        public static TResult RunSync<TResult>(Func<Task<TResult>> func)
            => _taskFactory
                .StartNew(func)
                .Unwrap()
                .GetAwaiter()
                .GetResult();

        /// <summary>
        /// Executes an async Task<T> method which has a T return type synchronously
        /// USAGE: T result = AsyncUtil.RunSync(() => AsyncMethod<T>());
        /// </summary>
        /// <typeparam name="TResult">Return Type</typeparam>
        /// <param name="task">Task<T> method to execute</param>
        /// <returns></returns>
        public static void RunSync(Func<Task> func)
            => _taskFactory
                .StartNew(func)
                .Unwrap()
                .GetAwaiter()
                .GetResult();  // var t = AsyncUtil.RunSync<T>(() => AsyncMethod<T>());
    }
}
