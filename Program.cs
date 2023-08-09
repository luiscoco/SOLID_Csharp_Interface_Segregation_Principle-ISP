using System;

namespace ISP
{
    public class Document
    {
        public string Content { get; set; }
    }

    public interface IMachine
    {
        void Print(Document d);
        void Fax(Document d);
        void Scan(Document d);
    }

    public class MultiFunctionPrinter : IMachine
    {
        public void Print(Document d)
        {
            Console.WriteLine("MultiFunctionPrinter: Printing document");
        }

        public void Fax(Document d)
        {
            Console.WriteLine("MultiFunctionPrinter: Faxing document");
        }

        public void Scan(Document d)
        {
            Console.WriteLine("MultiFunctionPrinter: Scanning document");
        }
    }

    public class OldFashionedPrinter : IMachine
    {
        public void Print(Document d)
        {
            Console.WriteLine("OldFashionedPrinter: Printing document");
        }

        public void Fax(Document d)
        {
            throw new NotImplementedException();
        }

        [Obsolete("Not supported", true)]
        public void Scan(Document d)
        {
            throw new NotImplementedException();
        }
    }

    public interface IPrinter
    {
        void Print(Document d);
    }

    public interface IScanner
    {
        void Scan(Document d);
    }

    public class Printer : IPrinter
    {
        public void Print(Document d)
        {
            Console.WriteLine("Printer: Printing document");
        }
    }

    public class Photocopier : IPrinter, IScanner
    {
        public void Print(Document d)
        {
            Console.WriteLine("Photocopier: Printing document");
        }

        public void Scan(Document d)
        {
            Console.WriteLine("Photocopier: Scanning document");
        }
    }

    public interface IMultiFunctionDevice : IPrinter, IScanner
    {

    }

    public class MultiFunctionMachine : IMultiFunctionDevice
    {
        // compose this out of several modules
        private readonly IPrinter printer;
        private readonly IScanner scanner;

        public MultiFunctionMachine(IPrinter printer, IScanner scanner)
        {
            this.printer = printer;
            this.scanner = scanner;
        }

        public void Print(Document d) => printer.Print(d);
        public void Scan(Document d) => scanner.Scan(d);
    }

    class MyClass
    {
        public static void Main(string[] args)
        {
            IPrinter printer = new Printer();
            IScanner scanner = new Photocopier();

            // Check if the device is a multi-function device before using its methods
            if (printer is IMultiFunctionDevice multiFunctionDevice)
            {
                Document document = new Document();
                document.Content = "Sample Content";

                multiFunctionDevice.Print(document);

                if (multiFunctionDevice is IScanner scannerDevice)
                {
                    scannerDevice.Scan(document);
                }
                else
                {
                    Console.WriteLine("Scanner functionality not available.");
                }
            }
            else
            {
                Console.WriteLine("Multi-function functionality not available.");
            }
        }
    }
}
