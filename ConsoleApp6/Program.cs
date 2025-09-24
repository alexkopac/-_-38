using System;
using System.Net.Http;
using System.Threading.Tasks;

public class Program
{
    
    private static readonly HttpClient client = new HttpClient();
    
    public static async Task Main(string[] args)
    {
        string url;

        do
        {
            Console.WriteLine("Будь ласка, введіть повну URL-адресу сайту (наприклад, https://google.com/).");
            Console.WriteLine("Або введіть 'exit' для виходу.");

            url = Console.ReadLine();


            if (url?.ToLower() == "exit")
            {
                Console.WriteLine("Застосунок завершує роботу...");
                break;
            }


            if (string.IsNullOrWhiteSpace(url))
            {
                Console.WriteLine("Введена адреса не може бути порожньою. Спробуйте ще раз.");
                continue;
            }


            try
            {
                Console.WriteLine($"\nЗавантаження контенту з {url}...");                
                string responseBody = await client.GetStringAsync(url);
                Console.WriteLine("Завантаження успішне. Розмір HTML-коду: " + responseBody.Length + " символів.\n");
                Console.WriteLine("--- Початок HTML-коду ---\n");
                Console.WriteLine(responseBody.Substring(0, Math.Min(responseBody.Length, 500)));
                Console.WriteLine("\n--- Кінець HTML-коду ---\n");
            }            
            catch (HttpRequestException e)
            {
                Console.WriteLine($"\nВиняток: Помилка HTTP-запиту!");
                Console.WriteLine($"Повідомлення: {e.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nВиняток: Невідома помилка!");
                Console.WriteLine($"Повідомлення: {ex.Message}");
            }

            Console.WriteLine("----------------------------------------\n");

        } while (true);
    }
}