using ObservableCmdlet.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObservableCmdlet.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var lib = new Library();

            var disposable =
                lib.InvokeHello()
                    .Subscribe(s => Console.WriteLine(s), e => Console.WriteLine(e));
                    

            Console.ReadLine();
            disposable.Dispose();
        }
    }
}
