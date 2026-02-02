class Programa
{
    static void Main()
    { 
        Console.Write("Escreva o nome: ");
        string nome = Console.ReadLine();


        Console.Write("digite a idade: ") ;
        int idade = int.Parse(Console.ReadLine());


        if(String.IsNullOrWhiteSpace(nome))
        {
            Console.WriteLine("Erro identificado nao pode ser vazio!");
            return;
        }

        if (idade<=0)
    
        
         {
            Console.WriteLine("nao pode ser menor que 0 ");
            return;
         }

         Console.WriteLine("usurio cadastrado com sucesso!");
         Console.WriteLine($"Nome : {nome}   |  Idade : {idade}");





    }


}