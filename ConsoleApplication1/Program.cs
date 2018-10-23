using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UMS.Models;
using Newtonsoft.Json;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            string js= "{\"body\":{\"operatetype\":2,\"id\":53,\"linecode\":\"3401000700034172100000210\",\"lineid\":\"LG119\","
+"\"linename\":\"合肥 - 东至(高速)\",\"linetype\":3,\"linelevel\":2,\"firststationname\":\"合肥旅游汽车站\" "
+ ",\"firststationid\":340100007,\"endstationname\":\"东至汽车站\",\"endstationid\":341721000,\"mileag"
+"e\":null,\"hghwaymile\":null,\"linesatus\":null,\"lineenabledate\":null,\"stopstation infos\":[{\"st"
+"opindex\":1,\"stopstation\":\"340800001\",\"stationmiles\":0.00},{\"stopindex\":2,\"stopstation\":\"34+"
+"1721001\",\"stationmiles\":0.00},{\"stopindex\":3,\"stopstation\":\"341721002\",\"stationmiles\":0.00"
+"},{\"stopindex\":4,\"stopstation\":\"341721007\",\"stationmiles\":0.00},{\"stopindex\":5,\"stopstatio"
+"n\":\"341721000\",\"stationmiles\":256.00}]},\"publicrequest\":{\"protover\":\"1.0\",\"pushid\":\"340102"
+"143237000089\",\"typecode\":\"line\",\"signdata\":\"2B513934EA45495D3964AC52674B3D7B\"} } ";
            var obj = JsonConvert.DeserializeObject(js);

            string modelFile = Path.Combine(@"D:\work\Sample\源码\UMS", @"ConsoleApplication1\bin\Debug\UMS.Models.dll");
            byte[] fileData = File.ReadAllBytes(modelFile);
            //Assembly assembly = Assembly.Load(fileData);
            Assembly assembly = Assembly.Load(modelFile);
            Type baseType = typeof(EntityBase);
            var assm = assembly.DefinedTypes;
            IEnumerable<Type> modelTypes = assembly.GetTypes().Where(m =>!m.Equals(baseType)&& baseType.IsAssignableFrom(m) && !m.IsAbstract);
            foreach(var t in assm)
            {
                Console.WriteLine(t.Equals(baseType));
                //Console.WriteLine(baseType.IsAssignableFrom(t));
            }
            //var f = baseType.IsAssignableFrom(typeof(UMS.Models.SysLoginInfo));
            //Console.WriteLine(f);
        }
    }
}
