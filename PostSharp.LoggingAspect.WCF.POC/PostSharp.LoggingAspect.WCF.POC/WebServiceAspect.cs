using System;
using PostSharp.Aspects;
using System.Diagnostics;

namespace PostSharp.LoggingAspect.WCF.POC
{
    [Serializable]
    [WebServiceAspect(AttributeExclude = true)]
    public class WebServiceAspect: OnMethodBoundaryAspect
    {
        public override void OnEntry(MethodExecutionArgs args)
        {
            args.MethodExecutionTag = Stopwatch.StartNew();
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            Stopwatch sw = (Stopwatch)args.MethodExecutionTag;
            sw.Stop();
            string methodName = (args.Method != null ? args.Method.Name : "");
            string className = string.Empty, arguments = string.Empty;
            bool logFile = true, isPrivateMethod = false;
            if (string.IsNullOrEmpty(methodName).Equals(false))
            {
                if (args.Method.DeclaringType != null)
                {
                    if (args.Method.DeclaringType.Name.Equals("CompositeType"))
                    {
                        logFile = false;
                    }
                    else
                    {
                        className = $"{args.Method.DeclaringType.Namespace}.{args.Method.DeclaringType.Name}";
                        arguments = string.Join(", ", args.Arguments);
                        isPrivateMethod = (args.Method.IsPrivate ? true : false);
                    }
                }



            }

            if (logFile)
            {
                string output = string.Format("{0} method {1}.{2}() with arguments {3} is Executed in {4} seconds",
                    (isPrivateMethod ? "Private" : "Public"), className, methodName, arguments,
                    sw.ElapsedMilliseconds / 1000);

                AppendToFile(output);
                System.Diagnostics.Debug.WriteLine(output);
            }
        }
        private void AppendToFile(string line)
        {
            System.IO.File.AppendAllLines(@"\\192.168.0.50\Share\R&D\bin\log.txt", new string[] {DateTime.Now.ToString()+ Environment.NewLine+ line });
            Console.WriteLine(">> " + line);
        }
    }
}
