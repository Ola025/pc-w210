using System; using System.Collections.Generic;

class Address { private string street; private string city; private string state; private string country;

public Address(string street, string city, string state, string country)
{
    this.street = street;
    this.city = city;
    this.state = state;
    this.country = country;
}

public bool IsInUSA()
{
    return country.ToLower() == "usa";
}

public string GetFullAddress()
{
    return $"{street}\n{city}, {state}\n{country}";
}

}

class Customer { private string name; private Address address;

public Customer(string name, Address address)
{
    this.name = name;
    this.address = address;
}

public bool LivesInUSA()
{
    return address.IsInUSA();
}

public string GetName()
{
    return name;
}

public string GetAddress()
{
    return address.GetFullAddress();
}

}

class Product { private string name; private string productId; private decimal price; private int quantity;

public Product(string name, string productId, decimal price, int quantity)
{
    this.name = name;
    this.productId = productId;
    this.price = price;
    this.quantity = quantity;
}

public decimal GetTotalCost()
{
    return price * quantity;
}

public string GetProductDetails()
{
    return $"{name} (ID: {productId}) - ${price} x {quantity}";
}

}

class Order { private List<Product> products; private Customer customer;

public Order(Customer customer)
{
    this.customer = customer;
    this.products = new List<Product>();
}

public void AddProduct(Product product)
{
    products.Add(product);
}

public decimal GetTotalPrice()
{
    decimal total = 0;
    foreach (var product in products)
    {
        total += product.GetTotalCost();
    }
    total += customer.LivesInUSA() ? 5 : 35;
    return total;
}

public string GetPackingLabel()
{
    string label = "Packing Label:\n";
    foreach (var product in products)
    {
        label += product.GetProductDetails() + "\n";
    }
    return label;
}

public string GetShippingLabel()
{
    return $"Shipping Label:\n{customer.GetName()}\n{customer.GetAddress()}";
}

}

class Program { static void Main() { Address address1 = new Address("123 Main St", "New York", "NY", "USA"); Customer customer1 = new Customer("Alice Johnson", address1);

Address address2 = new Address("456 Maple Ave", "Toronto", "ON", "Canada");
    Customer customer2 = new Customer("Bob Smith", address2);

    Order order1 = new Order(customer1);
    order1.AddProduct(new Product("Laptop", "A123", 1200m, 1));
    order1.AddProduct(new Product("Mouse", "B456", 25m, 2));

    Order order2 = new Order(customer2);
    order2.AddProduct(new Product("Phone", "C789", 800m, 1));
    order2.AddProduct(new Product("Charger", "D101", 20m, 1));

    Console.WriteLine(order1.GetPackingLabel());
    Console.WriteLine(order1.GetShippingLabel());
    Console.WriteLine($"Total Price: ${order1.GetTotalPrice()}\n");

    Console.WriteLine(order2.GetPackingLabel());
    Console.WriteLine(order2.GetShippingLabel());
    Console.WriteLine($"Total Price: ${order2.GetTotalPrice()}\n");
}

}
