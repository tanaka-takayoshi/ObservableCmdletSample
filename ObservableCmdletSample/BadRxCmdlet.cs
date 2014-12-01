using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ObservableCmdlet.Lib;

namespace ObservableCmdletSample
{
    [Cmdlet("Invoke", "BadHelloRx")]
    public class BadRxCmdlet : Cmdlet
    {
        protected override void ProcessRecord()
        {
            var context = SynchronizationContext.Current;
            WriteObject(context == null ? "ぬるぽ" : context.ToString());
            var lib = new Library();
            lib.InvokeHello()
                .ObserveOn(context)
                .Select(s =>
                {
                    WriteObject(s);
                    return s;
                })
                .FirstAsync()
                .Wait();
            WriteObject(true);
        }
    }
}
