using System;
using System.Collections.Concurrent;
using System.Management.Automation;
using ObservableCmdlet.Lib;

namespace ObservableCmdletSample
{
    [Cmdlet("Invoke", "HelloRx")]
    public class HelloRxCmdlet : Cmdlet
    {
        protected override void ProcessRecord()
        {
            var lib = new Library();
            var q = new BlockingCollection<Action>();
            var observer =
                    lib.InvokeHello()
                    .Subscribe(
                    status => q.Add(() =>
                    {
                        WriteObject(status);
                        WriteProgress(new ProgressRecord(1, "Progress", status));
                    }),
                    e => ThrowTerminatingError(new ErrorRecord(e, "", ErrorCategory.OperationStopped, null)),
                    q.CompleteAdding);
            foreach (var item in q.GetConsumingEnumerable())
            {
                item();
            }

            WriteObject(true);
        }
    }
}
