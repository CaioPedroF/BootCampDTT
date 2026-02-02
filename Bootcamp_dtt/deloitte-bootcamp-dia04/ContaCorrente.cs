public class ContaCorrente
{
    private decimal Saldo = 10000;
    private Boolean Vip ;


    public decimal ValorParaSacar()
    {
        Console.Write("Digite o valor que deseja sacar: ");
        return decimal.Parse(Console.ReadLine());
    }

    public void Sacar()
    {
        decimal valor = ValorParaSacar();

        if (valor <= Saldo)
        {
            Saldo -= valor;
            Console.WriteLine("Valor retirado com sucesso!");
            Console.WriteLine("Saldo atual: " + Saldo);
        }
        else
        {
            Console.WriteLine("Saldo insuficiente.");
        }
    }

    public decimal ValorParaDepositar()
    {
        Console.Write("Digite o valor que deseja depositar: ");
        return decimal.Parse(Console.ReadLine());
    }

    public void Depositar()
    {
        decimal valor = ValorParaDepositar();
        Saldo += valor;

        Console.WriteLine("Depósito realizado!");
        Console.WriteLine("Saldo atual: " + Saldo);
    }

public decimal ConsultarSaldo()

{
        Console.WriteLine("Seu Saldo Atual:  " + Saldo);
        return decimal.Parse(Console.ReadLine());



}




}
