



public class Mina
{
Minerio minerio = new Minerio();
public string Codigo;
public string nome;
public decimal Capacidade;

string extrairMinerio()
    {
        return"minerio";
        

    }

public Minerio extrairMinerio()
    {
        minerio.codigo = "1";
        minerio.tipo = "ouro";


        return minerio;



    }


public string getCodigo()

    {
        return this.Codigo;
        
    }


public void getNome()

    {
        return this.nome;


    }


public void setCapacidade()


    {
        


    }


public Minerio acessarExtrairMinerio(bool isGestorMina)
    {
        if (isGestorMina)
        {
            return this.extrairMinerio();
         }
        else
        {
            
            Minerio minerio = new Minerio();
            minerio.codigo = "0";
            return minerio;
        }





    }

}



