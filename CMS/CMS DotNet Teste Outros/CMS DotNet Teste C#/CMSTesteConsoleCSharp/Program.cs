using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSTesteConsoleCSharp
{
    class Program
    {

       // public enum Colors { Red, Green, Blue, Yellow = 12 };
        
        static void Main(string[] args)
        {
            Console.WriteLine("Inicio");
            Console.WriteLine("");
            try
            {

                Console.WriteLine("Teste Delegate");
                var pedido = new Pedido();
                pedido.Fechar(10.0);

                //var str01 = "Texto";
                //var int01 = 10;
                //TesteParemtro(str: str01, i: int01);

                //var valueString = "1";
                //var valueInt    = 2;
                //Console.WriteLine($"valueString esquerda: {valueString.JDFuncZeros(3, true)} ");
                //Console.WriteLine($"valueInt    esquerda: {valueInt.JDFuncZeros(3, true)}    ");
                //Console.WriteLine("");
                //Console.WriteLine($"valueString direita: {valueString.JDFuncZeros(3, false,'*')} ");
                //Console.WriteLine($"valueInt    direita: {valueInt.JDFuncZeros(3, false, '*')}    ");

                //var p1 = new PessoaClass() { Id = 10, Nome = "Pessoa01" };
                //Console.WriteLine($"{p1.ToString()}");
                //var p2 = new PessoaStruct() { Id = 20, Nome = "Pessoa02" };
                //Console.WriteLine($"{p2.ToString()}");

                //Console.WriteLine((int)Colors.Red);
                //Console.WriteLine(Colors.Red);
                //Console.WriteLine("D: " + Role.Developer.ToString("D"));
                //Console.WriteLine("G: " + Role.Developer.ToString("G"));
                //Console.WriteLine(Role.Developer.StringBusinessUnits());

                //var c1 = new Cliente();
                //Console.WriteLine("");
                //var c2 = new Cliente(10);
                //Console.WriteLine("");
                //var v1 = new ClienteVIP();
                //Console.WriteLine("");
                //var v2 = new ClienteVIP(10, " Fulano");
                //Console.WriteLine("v2.Nome: " + v2.Nome);

                //TesteRefTypeInt();
                //TesteRefeValueTypeInt();
                //TesteEnum();

            }
            catch (Exception ex)
            {
                Console.WriteLine("");
                Console.WriteLine($"Erro: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("");
                Console.WriteLine("Fim");
                Console.ReadKey();

            }
        }

        public class Boleto 
        {
            public void Pagar()
            {
                Console.WriteLine("Boleto.Pagar");
            }
        }
        public class Pedido 
        {
            private Boleto _boleto = new Boleto();
            public void Fechar(double valor) 
            {
                Console.WriteLine("Pedido.Fechar");
                this._boleto.Pagar();
            }
        }

        public static void TesteParemtro(int i, string str, bool valorBoll = false)
        {
            Console.WriteLine($"str: {str} - i: {i.ToString()}");
        }

        public static void TesteEnum()
        {
            Console.WriteLine("Colors.Red    = {0}", Colors.Red.ToString("d")    );
            Console.WriteLine("Colors.Green  = {0}", Colors.Green.ToString("d")  );
            Console.WriteLine("Colors.Blue   = {0}", Colors.Blue.ToString("d")   );
            Console.WriteLine("Colors.Yellow = {0}", Colors.Yellow.ToString("d") );
            Colors myColor = Colors.Yellow;
            Console.WriteLine("myColor.ToString(\"d\") = {0}", myColor.ToString("d") );
            Console.WriteLine("myColor.ToString(\"x\") = {0}", myColor.ToString("x") );
            Console.WriteLine("myColor.ToString(\"g\") = {0}", myColor.ToString("g") );
            Console.WriteLine("myColor.ToString(\"f\") = {0}", myColor.ToString("f") );
        }

        public static void Dobrar(string name, int value) => value = value * 2;

        public static void Dobrar(string name, ref int value) => value = value * 2;


        public static void TesteRefeValueTypeInt()
        {
            var x = 10;
            Console.WriteLine($"x: {x.ToString()}");

            Dobrar(value: x, name: "");
            Console.WriteLine($"x: {x.ToString()} - Parametro por Valor");

            Dobrar(value: ref x, name: "");
            Console.WriteLine($"x: {x.ToString()} - Parametro por Referencia");
        }

        public static void TesteRefTypeInt()
        {
            var pos1 = new PosicaoClass();
            pos1.X = 10;
            pos1.Y = 20;
            //Console.WriteLine($"pos1: {pos1.ToString()}");

            var pos2 = new PosicaoClass();
            pos2.X = 30;
            pos2.Y = 40;
            //Console.WriteLine($"pos2: {pos2.ToString()}");

            pos2 = pos1;
            pos2.X = 50;
            pos2.Y = 60;

            Console.WriteLine($"pos1.X: {pos1.X.ToString()}");
            Console.WriteLine($"pos1.Y: {pos1.Y.ToString()}");

            Console.WriteLine("");

            Console.WriteLine($"pos2.X: {pos2.X.ToString()}");
            Console.WriteLine($"pos2.Y: {pos2.Y.ToString()}");

        }
    }

    public partial class Employee
    {
        public void DoWork()
        {
        }
    }

    public partial class Employee
    {
        public void GoToLunch()
        {
        }
    }

    partial interface ITest
    {
        void Interface_Test();
    }
    partial interface ITest
    {
        void Interface_Test2();
    }

    partial struct S1
    {
        void Struct_Test() { }
    }
    partial struct S1
    {
        void Struct_Test2() { }
    }

    public static class MyExtensions
    {
        public static string JDFuncZeros(this string value, int totalWidth = 0, bool esqueda = true, char paddingChar = '0')
        {
            value = value.Trim();
            if (value.Length >   totalWidth) value = value.Substring(0, totalWidth);
            if (esqueda      == true ) value = value.PadLeft(totalWidth, paddingChar);
            if (esqueda      == false) value = value.PadRight(totalWidth, paddingChar);
            return value;
        }
        public static string JDFuncZeros(this int value, int qtde = 0, bool esqueda = true, char paddingChar = '0')
        {
            return MyExtensions.JDFuncZeros(value.ToString(), qtde, esqueda, paddingChar);
        }
    }


    public class Cliente
    {
        public ClienteContato Contato { get; set; }
        public int Id { get; set; } = 0;
        // public string Nome { get; set; } = "";
        // private string _name;
        // public string Name => _name != null ? _name : "NA";
        // private static int _counter = 0;
        // public static int Counter => _counter;

        private string nome;
        public string Nome
        {
            // get { return this.nome ; }
            // get => _name;
            // set => _name = value;
            get => this.nome;
            set
            {
                this.nome = "Sem Nome";
                if (value != "")
                    this.nome = value;
            }
        }


        public Cliente()
        {
            Console.WriteLine("Cliente.Constructor 1");
            //this._counter = ++NumberOfEmployees;
            this.Contato = new ClienteContato();
            this.Id = 0;
            this.Nome = "";
        }
        public Cliente(int id) : this()
        {
            Console.WriteLine("Cliente.Constructor 2");
            this.Id = id;
        }
        public Cliente(string nome) : this()
        {
            Console.WriteLine("Cliente.Constructor 3");
            this.Nome = nome;
        }
        public Cliente(int id, string nome) : this()
        {
            Console.WriteLine("Cliente.Constructor 4");
            this.Id = id;
            this.Nome = nome;
        }
    }

    public class ClienteVIP : Cliente
    {
        public ClienteVIP() : base()
        {
            Console.WriteLine("ClienteVIP.Constructor 1");
        }
        public ClienteVIP(int id) : base(id)
        {
            Console.WriteLine("ClienteVIP.Constructor 2");
        }
        public ClienteVIP(string nome) : base(nome)
        {
            Console.WriteLine("ClienteVIP.Constructor 3");
        }
        public ClienteVIP(int id, string nome) : base(id, nome)
        {
            Console.WriteLine("ClienteVIP.Constructor 4");
        }
    }

    public class ClienteContato
    {
        public string Telefone { get; set; }
        public ClienteContato()
        {
            //Console.WriteLine("ClienteContato.Constructor 1");
            this.Telefone = "";
        }
        public ClienteContato(string telefone) : this()
        {
            //Console.WriteLine("ClienteContato.Constructor 2");
            this.Telefone = telefone;
        }
    }

    public class PessoaClass // Reference Type
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public override string ToString()
        {
            return "PessoaClass: " + Id + " " + Nome;
        }
    }

    public struct PessoaStruct // Value Type
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public override string ToString()
        {
            return
                "Class: PessoaStruct; " +
                "Id: " + Id + "; " +
                "Nome: " + Nome + "; ";
        }
    }

    public static class GroupTypes
    {
        public const string TheGroup = "OEM";
        public const string TheOtherGroup = "CMB";
    }

    public enum Role
    {

        [Description("Arquiteto1")]
        Arquiteto,

        [Description("Developer1")]
        Developer,

        [Description("Tester1")]
        Tester,
    }

    //public static class RoleExtensions
    //{
    //    public static string ToDescriptionString(this Role val)
    //    {
    //        DescriptionAttribute[] attributes = (DescriptionAttribute[])val.GetType().GetField(val.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
    //        return attributes.Length > 0 ? attributes[0].Description : string.Empty;
    //    }
    //}

    public static class RoleExtensions
    {
        public static string StringBusinessUnits(this Role BU)
        {
            switch (BU)
            {
                case Role.Arquiteto: return "NEW EQUIPMENT";
                case Role.Developer: return "SERVICE";
                case Role.Tester: return "OPERATOR TRAINING";
                default: return String.Empty;
            }
        }
    }

    public enum Colors
    {

        //[Description("Red")]
        Red = 0,

        //[Description("Green")]
        Green = 1,

        // [Description("Blue")]
        Blue = 2,

        // [Description("Yellow")]
        Yellow = 3,
        
    }

    //public static class ColorsExtensions
    //{
    //    public static string ToDescriptionString(this Colors val)
    //    {
    //        DescriptionAttribute[] attributes = (DescriptionAttribute[])val.GetType().GetField(val.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
    //        return attributes.Length > 0 ? attributes[0].Description : string.Empty;
    //    }
    //}


    class PosicaoClass
    {
        public int X { get; set; }
        public int Y { get; set; }
        public override string ToString() => $"PosicaoClass = X: {this.X.ToString()} - Y: {this.Y.ToString()}";
        //public override string ToString() => base.ToString();
    }

    public static class ColorExtensions
    {
        public static string GetName(Colors value)
        {
            return Enum.GetName(typeof(Colors), value);
        }

        public static string ToColorString(this Colors c)
        {
            switch (c)
            {
                case Colors.Red:
                    return "Everything is OK";
                case Colors.Green:
                    return "SNAFU, if you know what I mean.";
                case Colors.Blue:
                    return "Reaching TARFU levels";
                case Colors.Yellow:
                    return "Reaching TARFU levels";
                default:
                    return "Get your damn dirty hands off me you FILTHY APE!";
            }
        }
    }
}
