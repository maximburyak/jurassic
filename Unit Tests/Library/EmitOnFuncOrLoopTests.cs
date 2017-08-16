using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jurassic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.Library
{

    public class ClassWithCallCounters
    {
        public int InstanceCounter = 0;
        public static int StaticCounter = 0;
        public void InstanceMethod()
        {
            InstanceCounter++;
        }

        public static void StaticMetod()
        {
            StaticCounter++;
        }
    }

    [TestClass]
    public class EmitOnFuncOrLoopTests
    {
    

        [TestMethod]
        public void OnLoopCallWithInstanceMethod()
        {
            var engine = new ScriptEngine();
            var classWithCallCounters = new ClassWithCallCounters();
            engine.OnLoopOrFuncCall = classWithCallCounters.InstanceMethod;
            var loopScript = @"
function Test(){ 
var d=1;
for (var i=0; i< 20; i++){

if (i==d){
d+=2;
}
d++;
}
}";
            engine.Evaluate(loopScript);
            engine.CallGlobalFunction("Test");
            Assert.AreEqual(19, classWithCallCounters.InstanceCounter);
        }

        [TestMethod]
        public void OnFuncCallWithInstanceMethod()
        {
            var engine = new ScriptEngine();
            var classWithCallCounters = new ClassWithCallCounters();
            engine.OnLoopOrFuncCall = classWithCallCounters.InstanceMethod;
            engine.EnableDebugging = true;
            var loopScript = @"
function InnerTestFunc(i){
return i+1;
}
function Test(){ 
var d=1;
for ( var i=0; i< 10; i++){
d+=InnerTestFunc(d);
d+=InnerTestFunc(d);
}
}";
            engine.Evaluate(loopScript);
            engine.CallGlobalFunction("Test");
            Assert.AreEqual(39, classWithCallCounters.InstanceCounter);
        }
    }
}
