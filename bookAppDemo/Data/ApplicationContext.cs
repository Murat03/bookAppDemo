using bookAppDemo.Models;

namespace bookAppDemo.Data
{
    public class ApplicationContext
    {
        public static List<Book> Books { get; set; }
        
        static ApplicationContext()
        {
            Books = new List<Book>()
            {
                new Book()
                {
                    Id = 1,
                    Title = "Satranç",
                    Price = 50
                },
                new Book()
                {
                    Id= 2,
                    Title = "Utopia",
                    Price = 100
                },
                new Book()
                {
                    Id = 3,
                    Title = "Nasreddin",
                    Price = 75
                }
            };
        }
    }
}
