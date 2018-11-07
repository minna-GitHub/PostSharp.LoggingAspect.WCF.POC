using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace PostSharp.LoggingAspect.WCF.POC
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class SampleService : ISampleService
    {
        public string GetData(int value)
        {
            CallSamePrivateMethod("hi", "wcf");
            return string.Format("You entered: {0}", value);
        }

        private void CallSamePrivateMethod(string a, string b)
        {
            string c = string.Concat(a, b);
        }

        public int Add(int a, int b)
        {
            return a + b;
        }
        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
