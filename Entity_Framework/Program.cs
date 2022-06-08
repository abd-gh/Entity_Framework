using Entity_Framework;

namespace Asset_Tracking
{




    class Program
    {
        static List<Product> products = new List<Product>();
        static void print()
        {

            Console.WriteLine("Id".PadRight(5) + "Type".PadRight(12) + "Brand".PadRight(12) + "Model".PadRight(14) + "Office".PadRight(12) + "Purchase Date".PadRight(15) + "Price in USD".PadRight(15) + "Currency".PadRight(12) + "Local Price".PadRight(15));
            Console.WriteLine("----".PadRight(5) + "----".PadRight(12) + "-----".PadRight(12) + "-----".PadRight(14) + "------".PadRight(12) + "-------------".PadRight(15) + "------------".PadRight(15) + "--------".PadRight(12) + "-----------".PadRight(15) + "\n");
            MyDbContext context = new MyDbContext();

            //Getting data from DataBase
            var x = context.Products.ToList();

            /* Getting the Data from local List
             List<Product> sorted = products.OrderBy(s => s.office)
                                    .ThenBy(s => s.purchase_date)
                                    .ToList();         
             }*/
            foreach (var product in x)
            {
                //Get the different between the current date and the product date
                var sub = (DateTime.Now).Subtract(product.purchase_date);

                //Compare teh date by Days
                if (sub.Days > (365 * 3))
                {
                    if (sub.Days > ((365 * 3) + (31 * 6)))
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    else
                        Console.ForegroundColor = ConsoleColor.Red;
                }
                Console.WriteLine(product.Id.ToString().PadRight(5) + product.name.PadRight(12) + product.brand.PadRight(12) + product.model.PadRight(14) + product.office.PadRight(12) + product.purchase_date.ToString("yyyy-MM-dd").PadRight(15) + product.price.ToString().PadRight(15) + product.currency.PadRight(12) + product.local_Price.ToString().PadRight(15));
                Console.ResetColor();
            }
        }

        static void insert()
        {


            Console.Write("Choose The Type Of Product Computer or Mobile:  ");
        RewriteName: string name = Console.ReadLine().ToUpper();

            /* if (name.ToLower().Trim() == "print") //check if user insert 'print' to print table
             {
                 print();
                 break;
             }
             else*/
            if (name.ToLower().Trim() != "computer" && name.ToLower().Trim() != "mobile") //we allow just to insert computer or mobile 
            {
                Console.Write("This Name doesn't exist, Please Computer or Mobile: ");
                goto RewriteName; //if not go to the 'RewriteName' and execute the code again
            }

            Console.Write("Inter Brand : ");
            string brand = Console.ReadLine();

            Console.Write("Inter Mobel Name: ");
            string model = Console.ReadLine();

            Console.Write("Choose Office 'FR, SE or USA': ");

        Rewriteoffice: string office = Console.ReadLine().ToUpper();
            //we allow just to insert 'FR, SE or USA'
            if (office.ToLower().Trim() != "fr" && office.ToLower().Trim() != "se" && office.ToLower().Trim() != "usa")
            {
                Console.Write("This office doesn't exist, Please choose 'FR, SE or USA' : ");
                goto Rewriteoffice; //if not go to the 'Rewriteoffice' and execute the code again
            }

            Console.Write("Inter Price In USD :");
            int price = Convert.ToInt32(Console.ReadLine());

            Console.Write("Inter Purchase Date (yyyy-MM-dd):  ");
            DateTime date = Convert.ToDateTime(Console.ReadLine());


            //determine the local currency by check which office the user choose
            //then calculate the local price using dollar price
            string currency;
            double local_Price;
            if (office.ToLower().Trim() == "usa")
            {
                local_Price = price;
                currency = "USD";
            }
            else if (office.ToLower().Trim() == "fr")
            {
                local_Price = price * 0.94;
                currency = "EUR";
            }
            else
            {
                local_Price = price * 9.8;
                currency = "SEK";
            }

            Console.WriteLine();

            //products.Add(new Product(office, name, brand, date, price, model, currency, local_Price));

            //save in Data Base
            Product p = new Product(office, name, brand, date, price, model, currency, local_Price);
            Console.Write("Press 'save' if you want to save it in DataBase !!");
            string s = Console.ReadLine();
            if (s.ToLower().Trim() == "save")
            {
                MyDbContext context = new MyDbContext();
                context.Products.Add(p);
                context.SaveChanges();
                print();
            }

        }
        static void Update()
        {
            Console.Write("insert the Id of product to update it !!");
            int IdToUpdate = Convert.ToInt32(Console.ReadLine());
            Console.Write("please insert the name of value which you want to update it 'price or model'");
            string nameOfValue = Console.ReadLine();
            string newVallue;
            string currentCurency;
            MyDbContext context = new MyDbContext();
            var record = context.Products.SingleOrDefault(p => p.Id == IdToUpdate);
            switch (nameOfValue.ToLower())
            {
                case "price":
                    Console.Write("insert the new price:");
                    newVallue = Console.ReadLine();
                    record.price = Convert.ToInt32(newVallue);
                    currentCurency = record.currency;
                    if (currentCurency == "SEK")
                        record.local_Price = Convert.ToInt32(newVallue) * 9.8;
                    else if (currentCurency == "EUR")
                        record.local_Price = Convert.ToInt32(newVallue) * 0.94;
                    else
                        record.local_Price = Convert.ToInt32(newVallue);

                    context.SaveChanges();
                    print();
                    break;
                case "model":
                    Console.Write("insert the new model:");
                    newVallue = Console.ReadLine();
                    record.model = newVallue;
                    context.SaveChanges();
                    print();
                    break;

            }
        }
        static void Main(string[] args)
        {

            print();
            while (true)
            {

                Console.Write("Press 'C' create new item ,'D' to delet item or 'U' to update item : ");
                string input = Console.ReadLine();
                if (input.ToLower().Trim() == "c")
                {
                    insert();
                }
                else if (input.ToLower().Trim() == "d")
                {
                    Console.Write("insert 'Id'to delet item : ");
                    int id = Convert.ToInt32(Console.ReadLine());
                    MyDbContext context = new MyDbContext();
                    var pTodelete = context.Products.SingleOrDefault(x => x.Id == id);
                    context.Products.Remove(pTodelete);
                    context.SaveChanges();
                    print();
                }
                else if (input.ToLower().Trim() == "u")
                {
                    Update();
                }


            }

        }
    }
}