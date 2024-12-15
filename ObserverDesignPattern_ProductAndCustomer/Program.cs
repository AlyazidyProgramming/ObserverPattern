internal class Program
{
   private static void Main(string[] args)
   {
      Console.WriteLine();
      IProduct product1 = new Product("ASUS Laptop", 1000);
      ICustomer customer1 = new Customer("Saleh");
      ICustomer customer2 = new Customer("Saman");
      ICustomer customer3 = new Customer("Abu-Samer");
      ICustomer customer4 = new Customer("Sharif");
      product1.AddWatcher(customer1);
      product1.AddWatcher(customer2);
      product1.AddWatcher(customer3);
      product1.AddWatcher(customer4);
      ((Product)product1).Price = 999m;

      product1.RemoveWatcher(customer1);
      product1.RemoveWatcher(customer4);
      ((Product)product1).Price = 899m;

      Console.ReadKey();
   }
}

internal interface IProduct
{
   string Name { get; }
   decimal Price { get; }

   void AddWatcher(ICustomer customer);

   void RemoveWatcher(ICustomer customer);

   void Notify();
}

internal class Product : IProduct
{
   private readonly string _name;
   private decimal _price;
   private List<ICustomer> _customers = new();

   public Product(string name, decimal price)
   {
      _name = name;
      _price = price;
   }

   public string Name => _name;

   public decimal Price
   {
      get
      {
         return _price;
      }
      set
      {
         if (_price != value)
         {
            decimal oldPrice = _price;
            _price = value;
            _Notify(oldPrice);
         }
      }
   }

   public void AddWatcher(ICustomer customer)
   {
      _customers.Add(customer);
   }

   private void _Notify(decimal oldPrice)
   {
      Console.WriteLine("-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_");
      Console.WriteLine($"Price of {Name} changed From {oldPrice} to {Price}");
      Console.WriteLine("-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_");
      Console.WriteLine();
      Notify();
   }

   public void Notify()
   {
      _customers.ForEach(customer => customer.Notify(this));
      Console.WriteLine();
   }

   public void RemoveWatcher(ICustomer customer)
   {
      _customers.Remove(customer);
   }
}

internal interface ICustomer
{
   void Notify(IProduct product);
}

internal class Customer : ICustomer
{
   private readonly string _name;

   public Customer(string name)
   {
      _name = name;
   }

   public void Notify(IProduct product)
   {
      Console.WriteLine($"{_name}, the price of product {product.Name} has changed to {product.Price}");
   }
}