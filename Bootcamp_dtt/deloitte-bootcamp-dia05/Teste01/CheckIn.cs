public class Visitantes
{
    public int Id{get;set;}
    public string Nome{get;set;}

    public DateTime HorarioChegada{get;set;}

    public DateTime? HorarioSaida{get;set;}
     
    public Boolean EprimeiraVez{get;set;}





    public Visitantes (string Nome, int Id, string Documento, bool EprimeiraVez)
    {

      Id = id;
      Nome = nome;
      Documento = documento;
      HoraChegada = DateTime.Now;
      EprimeiraVez = eprimeiraVez;

    }







}