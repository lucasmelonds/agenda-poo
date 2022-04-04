using System;
using System.Collections.Generic;

public class SMS {
  public Contato Contato {get; set;}
  public Grupo Grupo {get; set;}
  public string Texto {get; set;}
  public DateTime Data {get; set;}

  public Contato Contato1 {get => Contato; set => Contato = value;}
  public Grupo Grupo1 {get => Grupo; set => Grupo = value;}
  public string Texto1 {get => Texto; set => Texto = value;}
  public DateTime Data1 {get => Data; set => Data = value;}
  public SMS () { }
  public SMS (DateTime data, string texto, Contato contato, Grupo grupo) {
    Data = data;
    Contato = contato;
    Texto = texto;
    Grupo = grupo;
  }
  
  public override string ToString() {
    if (Contato != null && Grupo == null) return $"------------\nMensagem enviada para {Contato.nome}. \n'{Texto}' - {Data.ToString("dd/MM/yyyy")}\n------------\n";
    else return $"------------\nMensagem enviada para o grupo {Grupo.grupos}. \n'{Texto}' - {Data.ToString("dd/MM/yyyy")}\n------------\n";
  }
}
