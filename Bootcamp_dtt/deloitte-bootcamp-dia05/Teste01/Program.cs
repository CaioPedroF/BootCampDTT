using System.Diagnostics;

class program
{

    static List<Visitantes> visitantes = new List<Visitantes>();
    static int ProximoId = 1;



    static void main()
    {
        int opcao = 0;

        do
        {
            Console.WriteLine("/n=== Controle de Check-IN ===");
            Console.WriteLine("1 - Cadastar ");
            Console.WriteLine("2 - Listar");
            Console.WriteLine("3 - Buscar Visitante pelo Nome");
            Console.WriteLine("4 - Registrar Saida ");
            Console.WriteLine("5 - Listar apenas Primeoira Saida");
            Console.WriteLine("6 - Ordenar por ID");
            Console.WriteLine("0 - Sair");

            try
            {
                opcao = int.Parse(Console.ReadLine());
                
            switch(opcao)
                {
                  case 1:
                  CadastrarVisitantes;
                  break;

                  case 2:
                  ListarVisitantes;
                  break;

                  case 3:
                  BuscarPeloNome;
                  break;

                  case 4:
                  RegistrarSaida;
                  break;

                  case 5:
                  ListarPriemiraSaida;
                  break;

                  case 6:
                  OrdenarID;
                  break;

                  case 0:
                  Console.WriteLine("Encerrando...");
                  break;

                  default:
                  Console.WriteLine("Opção invalida!");
                  break;
        
                }
            } catch(Exception ex)

            {
                  Console.WriteLine($"Error  {ex.Message}");
            }
            
            
        }   
            while(opcao!=0);
            
            
    }
  

 static void CadastarVisitante()
    {
        Console.Write("Digite seu nome:    ");
        string nome = Console.ReadLine();

        Console.Write("Informe seu Documento:           ");
        string documento = Console.ReadLine();

        Console.Write("É sua Primeira Vez? (s/n) ");
        Boolean primeiraVez = Console.ReadLine().ToLower() = 's';

        Visitante v = new Visitante(ProximoId,nome,documento,primeiraVez);
        Visitantes.Add(v);

        Console.WriteLine("Visitante Cadastrado com sucesso!");
    }

static void ListarVisitantes()
    {
        foreach (var v in Visitantes);
        {
            Console.WriteLine($"ID:  {v.ID}  |  {v.Nome}   |  Chegada: {v.HoraChegada}  |    Saida{v.HorarioSaida}");
        }
    }

static void BuscarPeloNome()
    {
        Console.Write("Digite Seu Nome:     ");
        string nome = Console.ReadLine();



        var encontrados = Visitantes.Where(V => v.nome.Contains(nome, StringComparsion.OrdinalIgnoreCase));

        foreach(var v in encontrados)
        {
            ConsoleTraceListener.WriteLine($"ID: {v.id}   |     Nome: {v.nome}");
        }
    }

static void RegistrarSaida()
    {
        Console.Write("Digite o Id do Visitante:      ");
        int id = int.Parse(Console.ReadLine());

        
        var Visitante = Visitantes.FirstOrDefault(v => Id == id);

        if(Visitante is null)
        {
            Console.WriteLine("Visitante nao encontrado");
            return;
        }
    
    Visitante.HorarioSaida = DateTime.Now;
        Console.WriteLine("Saída Registrada com sucesso!!!");

     }
    

static void ListarPrimeiraVisita()
    {
        var lista = Visitantes.Where(v => v.EprimeiraVez);

        foreach (var v in lista)
        { 
            Console.WriteLine($"ID:  {v.id}      Nome: {v.Nome}");
        }
    }


static void OrdenarPorId()
    {
        visitantes = Visitantes.OrderBy(v => v.Id).ToList();
        Console.WriteLine("Visitantes ordenados por ID ");
    } 






}