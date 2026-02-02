public class Lampada
{
    private Boolean isligada;


    public Lampada()
    {
        isligada = false;



    }
    
  
    public void Ligar()
    {
        isligada = true;
        Console.WriteLine("esta ligada!");


    }

 public void Desligada()
    {
        
        isligada = false;
        Console.WriteLine("Esta Desligada!" );


    }


}



