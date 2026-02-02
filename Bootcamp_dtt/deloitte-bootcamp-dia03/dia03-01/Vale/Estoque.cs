


public class Eestoque01
{
private int Id;
private  int ProducaoId;
private decimal Quantidade;
private string Local;




public int getId()
    {
        
        return this.Id;
    }


public int getProducaoId()
    {
        

        return this.ProducaoId;  
    }



public void setQuantidade()
    {
        

        return this.Quantidade;
    }



public string getLocal()
    {
        

        return this.Local;
    }


public Armazenar(int Id,decimal Quantidade)
   
    {
        
    if(Quantidade > 0)
        {
            return Console.Write("Quantidade :  ".Quantidade);
        
        }
        else
        {
            return Console.Write("Quantidade Vazia");
        }
    
        
    }





}