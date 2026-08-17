using System;
using System.Collections.Generic;
using System.Linq;

namespace Gün13
{
    public class Product
    {
        private int Id;
        private string Name;
        private decimal Price;
        private int Stock;

        public int id
        {
            get { return Id; }
            set { Id = value; }
        }

        public string name
        {
            get { return Name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name cannot be empty.");
                }

                Name = value;
            }
        }

        public decimal price
        {
            get { return Price; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Price cannot be negative.");
                }

                Price = value;
            }
        }

        public int stock
        {
            get { return Stock; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Stock cannot be negative.");
                }

                Stock = value;
            }
        }

        public Product(int id, string name, decimal price, int stock)
        {
            this.id = id;
            this.name = name;
            this.price = price;
            this.stock = stock;
        }
    }

    public class Customer
    {
        private int Id;
        private string Name;
        private string Surname;

        public int id
        {
            get { return Id; }
            set { Id = value; }
        }

        public string name
        {
            get { return Name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name cannot be empty.");
                }

                Name = value;
            }
        }

        public string surname
        {
            get { return Surname; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Surname cannot be empty.");
                }

                Surname = value;
            }
        }

        public Customer(int id, string name, string surname)
        {
            this.id = id;
            this.name = name;
            this.surname = surname;
        }
    }

    public class Orders
    {
        private int OrderNumber;
        private Customer Customer;
        private Product Product;
        private int Quantity;
        private decimal Price;

        public int orderNumber
        {
            get { return OrderNumber; }
            set { OrderNumber = value; }
        }

        public Customer customer
        {
            get { return Customer; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Customer cannot be null.");
                }

                Customer = value;
            }
        }

        public Product product
        {
            get { return Product; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Product cannot be null.");
                }

                Product = value;
            }
        }

        public int quantity
        {
            get { return Quantity; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(
                        "Quantity must be greater than zero."
                    );
                }

                Quantity = value;
            }
        }

        public decimal price
        {
            get { return Price; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Price cannot be negative.");
                }

                Price = value;
            }
        }

        public Orders(
            int orderNumber,
            Customer customer,
            Product product,
            int quantity
        )
        {
            this.orderNumber = orderNumber;
            this.customer = customer;
            this.product = product;
            this.quantity = quantity;

            this.price = CalculateTotalPrice();

            DecreaseStock();
        }

        public decimal CalculateTotalPrice()
        {
            return product.price * quantity;
        }

        public void DecreaseStock()
        {
            if (quantity <= product.stock)
            {
                product.stock = product.stock - quantity;
            }
            else
            {
                throw new ArgumentException(
                    "Not enough stock available."
                );
            }
        }
    }

    internal class Program
    {
        // Bütün metotlar AYNI listeleri kullanacak.
        static List<Product> products = new List<Product>();
        static List<Customer> customers = new List<Customer>();
        static List<Orders> orders = new List<Orders>();

        static int productId = 1;
        static int customerId = 1;
        static int orderId = 1;

        public static void Menu()
        {
            Console.Clear();

            Console.WriteLine(
                "CUSTOMER AND PRODUCT MANAGEMENT APP"
            );

            Console.WriteLine(
                "---------------------------------------"
            );

            Console.WriteLine("1 - Product Add");
            Console.WriteLine("2 - Customer Add");
            Console.WriteLine("3 - Create Order");
            Console.WriteLine("4 - View Order");
            Console.WriteLine("5 - Exit");

            Console.Write("\nSelect: ");
        }

        public static void ProductAdd()
        {
            Console.Clear();

            Console.Write("Product Name: ");
            string name = Console.ReadLine();

            Console.Write("Product Price: ");
            decimal price =
                decimal.Parse(Console.ReadLine());

            Console.Write("Stock: ");
            int stock =
                int.Parse(Console.ReadLine());

            Product product =
                new Product(
                    productId,
                    name,
                    price,
                    stock
                );

            products.Add(product);

            productId++;

            Console.Clear();

            Console.WriteLine("Product Added!");
            Console.WriteLine();

            Console.WriteLine(
                $"Product Id: {product.id}\n" +
                $"Product Name: {product.name}\n" +
                $"Product Price: {product.price}\n" +
                $"Product Stock: {product.stock}"
            );

            Console.ReadLine();
        }

        public static void CustomerAdd()
        {
            Console.Clear();

            Console.Write("Customer Name: ");
            string name = Console.ReadLine();

            Console.Write("Customer Surname: ");
            string surname = Console.ReadLine();

            Customer customer =
                new Customer(
                    customerId,
                    name,
                    surname
                );

            customers.Add(customer);

            customerId++;

            Console.Clear();

            Console.WriteLine("Customer Added!");
            Console.WriteLine();

            Console.WriteLine(
                $"Customer Id: {customer.id}\n" +
                $"Customer Name: {customer.name}\n" +
                $"Customer Surname: {customer.surname}"
            );

            Console.ReadLine();
        }

        static void CreateOrder()
        {
            Console.Clear();

            if (products.Count == 0)
            {
                Console.WriteLine(
                    "Please add a product first."
                );

                Console.ReadLine();
                return;
            }

            if (customers.Count == 0)
            {
                Console.WriteLine(
                    "Please add a customer first."
                );

                Console.ReadLine();
                return;
            }

            Console.WriteLine("PRODUCTS");
            Console.WriteLine("------------------");

            foreach (Product product in products)
            {
                Console.WriteLine(
                    $"{product.id} - " +
                    $"{product.name} - " +
                    $"{product.price} TL - " +
                    $"Stock: {product.stock}"
                );
            }

            Console.WriteLine();

            Console.Write("Product Id: ");

            int selectedProductId =
                int.Parse(Console.ReadLine());

            Product selectedProduct =
                products.FirstOrDefault(
                    p => p.id == selectedProductId
                );

            if (selectedProduct == null)
            {
                Console.WriteLine(
                    "Product not found."
                );

                Console.ReadLine();
                return;
            }

            Console.Clear();

            Console.WriteLine("CUSTOMERS");
            Console.WriteLine("------------------");

            foreach (Customer customer in customers)
            {
                Console.WriteLine(
                    $"{customer.id} - " +
                    $"{customer.name} " +
                    $"{customer.surname}"
                );
            }

            Console.WriteLine();

            Console.Write("Customer Id: ");

            int selectedCustomerId =
                int.Parse(Console.ReadLine());

            Customer selectedCustomer =
                customers.FirstOrDefault(
                    c => c.id == selectedCustomerId
                );

            if (selectedCustomer == null)
            {
                Console.WriteLine(
                    "Customer not found."
                );

                Console.ReadLine();
                return;
            }

            Console.Write("Quantity: ");

            int quantity =
                int.Parse(Console.ReadLine());

            try
            {
                Orders order =
                    new Orders(
                        orderId,
                        selectedCustomer,
                        selectedProduct,
                        quantity
                    );

                orders.Add(order);

                orderId++;

                Console.Clear();

                Console.WriteLine(
                    "ORDER CREATED"
                );

                Console.WriteLine(
                    "-------------------"
                );

                Console.WriteLine(
                    $"Order Number: {order.orderNumber}\n" +
                    $"Customer: {order.customer.name} " +
                    $"{order.customer.surname}\n" +
                    $"Product: {order.product.name}\n" +
                    $"Quantity: {order.quantity}\n" +
                    $"Total Price: {order.price}\n" +
                    $"Remaining Stock: {order.product.stock}"
                );
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(
                    ex.Message
                );
            }

            Console.ReadLine();
        }

        static void ViewOrder()
        {
            Console.Clear();

            if (orders.Count == 0)
            {
                Console.WriteLine(
                    "There are no orders."
                );

                Console.ReadLine();
                return;
            }

            Console.Write(
                "Enter Order Number: "
            );

            int orderNumber =
                int.Parse(Console.ReadLine());

            Orders order =
                orders.FirstOrDefault(
                    o => o.orderNumber == orderNumber
                );

            Console.Clear();

            if (order == null)
            {
                Console.WriteLine(
                    "Order not found."
                );

                Console.ReadLine();
                return;
            }

            Console.WriteLine("ORDER INFORMATION");
            Console.WriteLine("------------------");

            Console.WriteLine(
                $"Order Number: {order.orderNumber}\n" +
                $"Customer: {order.customer.name} " +
                $"{order.customer.surname}\n" +
                $"Product: {order.product.name}\n" +
                $"Quantity: {order.quantity}\n" +
                $"Total Price: {order.price}"
            );

            Console.ReadLine();
        }

        static void Main(string[] args)
        {
            char choice;

            do
            {
                Menu();

                choice =
                    Console.ReadKey().KeyChar;

                if (choice == '1')
                {
                    ProductAdd();
                }
                else if (choice == '2')
                {
                    CustomerAdd();
                }
                else if (choice == '3')
                {
                    CreateOrder();
                }
                else if (choice == '4')
                {
                    ViewOrder();
                }

            } while (choice != '5');
        }
    }
}