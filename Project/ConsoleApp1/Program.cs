using System;
using System.Linq;
using System.Reflection;

namespace ConsoleApp1
{
    class Program
    {
        static void Main()
        {
            // Create MOtorcycle istance
            Type motoType = typeof(Motorcycle);
            Type ctorParametr = typeof(string);

            ConstructorInfo ctor = motoType
                .GetTypeInfo()
                .DeclaredConstructors
                .First(ct => ct.GetParameters()[0].ParameterType == ctorParametr);
            //FirstOrDefault

            object[] args = new object[] { "Honda" };

            object obj = ctor.Invoke(args);
            Console.WriteLine($"Current type - {obj.GetType()}");
            Console.WriteLine(obj.ToString());

            // Read/Write field operations

            FieldInfo fieldInfo = obj.GetType().GetTypeInfo().GetDeclaredField("_vinNumber");
            Console.WriteLine($"Field - {fieldInfo.Name}");
            Console.WriteLine($"Is Private - {fieldInfo.IsPrivate}");

            fieldInfo.SetValue(obj, 999);
            Console.WriteLine(obj.ToString());
        }
    }
}
