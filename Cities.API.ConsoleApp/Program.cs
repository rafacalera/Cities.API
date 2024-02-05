using System.Threading.Tasks;

namespace Cities.API.ConsoleApp
{
    internal class Program
    {
        private static HttpClient _sharedClient = new()
        {
            BaseAddress = new Uri("http://localhost:5222"),
        };
        private static Tasks _tasks = new Tasks(_sharedClient);



        static async Task Main(string[] args)
        {
            while (true)
            {


                Console.WriteLine("\nSelecione a operacão que você gostaria de fazer:\n");
                Console.WriteLine("Visualizar todos os registros (1)");
                Console.WriteLine("Visualizar registro específico (2)");
                Console.WriteLine("Adicionar registro (3)");
                Console.WriteLine("Atualizar registro (4)");
                Console.WriteLine("Deletar registro (5)\n");



                string option = Console.ReadLine();
                Console.Clear();

                switch (option.Trim())
                {
                    case "1":
                        await _tasks.GetAll();
                        break;
                    case "2":
                        await _tasks.GetById();
                        break;
                    case "3":
                        await _tasks.Add();
                        break;
                    case "4":
                        await _tasks.Update();
                        break;
                    case "5":
                        await _tasks.Delete();
                        break;
                    default:
                        Console.WriteLine("Opcão inválida.");
                        break;
                }

                Console.WriteLine("\n\n\nPressione qualquer tecla para continuar");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
