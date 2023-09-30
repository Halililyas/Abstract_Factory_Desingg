using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstract_Factory_Desing
{
    public class Program
    {
        static void Main(string[] args)
        {
            ProductManager productManager = new ProductManager(new Factory1()); // Burda Çalışacağımız Fabrikayı Vermeyi Unutmaylaım 
            productManager.GetAll();
            Console.ReadLine();
        }
    }
    public abstract class Logging // Loglma İşlemi İçin Bir Soyut sınıf Nedeni Birden Fazla loglama Yapabiliriz Çeşidi Fazla olabilir 
    {
        public abstract void Log(string Message);
    }
    public class Log4netLogger : Logging // Loglama Çeşitleri
    {
        public override void Log(string Message)// Loglama Çeşitleri
        {
            Console.WriteLine("Loggend with Log4net");
        }
    }
    public class NLoger : Logging
    {
        public override void Log(string Message)// Loglama Çeşitleri 
        {
            Console.WriteLine("Loggend with NLogger");
        }
    }
    public abstract class Cahing // Cache İçin Soyut bir Sınıf 
    {
        public abstract void Cache(string data);
    }
    public class MemCahce : Cahing // cache çeşidi
    {
        public override void Cache(string data)
        {
            Console.WriteLine("Cahcing with MemCache");
        }
    }
    public class RedisCahce : Cahing
    {
        public override void Cache(string data)
        {
            Console.WriteLine("Cahcing with Redis");
        }
    }

    public abstract class CCC // Fabrika AnaFabrika Soyut Şekilinde  Bu fabrika Çağrıldığı yerde bize loglama işlemi üreticek 
    {
        public abstract Logging CreatLoggıng();
        public abstract Cahing CreatCahcenig();
    }

    public class Factory1 : CCC // FAbrika1
    {
        public override Cahing CreatCahcenig()
        {
            return new MemCahce();
        }

        public override Logging CreatLoggıng()
        {
            return new NLoger();
        }
    }
    public class Factory2 : CCC // Fabrika2
    {
        public override Cahing CreatCahcenig()
        {
            return new RedisCahce();
        }

        public override Logging CreatLoggıng()
        {
            return new Log4netLogger();
        }
    }
    public class ProductManager
    {
        private CCC _ccc; // Burada Bu sınıf Enjekte Ettik 
        private Logging _logging;
        private Cahing _cahing;

        public ProductManager(CCC ccc)
        {
            _ccc = ccc;
            _logging = _ccc.CreatLoggıng();
            _cahing = _ccc.CreatCahcenig();
        }

        public void GetAll()
        {
            _logging.Log("Logged");
            _cahing.Cache("DATA");
            Console.WriteLine("Product Listed!!");
        }
    }
}
