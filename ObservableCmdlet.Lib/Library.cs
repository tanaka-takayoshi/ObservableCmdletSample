using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ObservableCmdlet.Lib
{
public class Library
{
    public IObservable<string> InvokeHello()
    {
        var subject = new Subject<string>();
        ThreadPool.QueueUserWorkItem(async _ =>
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
            subject.OnNext("start");
            await Task.Delay(TimeSpan.FromSeconds(1));
            subject.OnNext("1");
            await Task.Delay(TimeSpan.FromSeconds(1));
            subject.OnNext("2");
            await Task.Delay(TimeSpan.FromSeconds(1));
            subject.OnNext("3");
            await Task.Delay(TimeSpan.FromSeconds(1));
            subject.OnNext("complete");
            subject.OnCompleted();
        });
        return subject;
    }
}
}
