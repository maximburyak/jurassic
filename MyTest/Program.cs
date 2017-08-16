using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Jurassic;

namespace MyTest
{
    public class Program
    {
        
        private int steps = 0;
        private string b = "a";
        public void Branch()
        {
            Console.WriteLine("trala");
            var l = steps;
            var i = steps++;
        }

        public static int steps2;

        public static void BranchStatic()
        {
            Console.WriteLine("trala");
            var l = steps2;
            var i = steps2++;
        }
        static void Main(string[] args)
        {
            var s = @"
function Test2(a){
if (a==0)
return 0;
return Test(a-1)+1;

}
function Test(a){ 
if (a==0)
return 0;
return Test2(a-1)+1;    
}";
            long l;
            var sp = Stopwatch.StartNew();
            var engine = new ScriptEngine();
            
            engine.EnableDebugging = true;
            var program = new Program();
            //engine.OnLoopOrFuncCall = program.Branch;
            engine.OnLoopOrFuncCall = Program.BranchStatic;
            engine.Evaluate(s);

            var res = engine.CallGlobalFunction<object>("Test",20);
            Console.WriteLine(Program.steps2);
            Console.WriteLine($"{sp.ElapsedMilliseconds}");
            /*
               var engine = new Jurassic.ScriptEngine();
        engine.Evaluate("function test(a, b) { return a + b }");
        Assert.AreEqual(11, engine.CallGlobalFunction<int>("test", 5, 6));
        */
        }
    }
}
