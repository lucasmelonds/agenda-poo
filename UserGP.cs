using System;
using System.Collections.Generic;

public class UserGrupo {
  private Contato contato;
  private long contatoNum;
  public Contato Contato {get => contato; set => contato = value;}
  public long ContatoNum {get => contatoNum; set => contatoNum = value;}
  public UserGrupo () { }
  public UserGrupo (Contato contato) {
    this.contato = contato;
    this.contatoNum = contato.numero;
  }
  public void SetContato(Contato contato) {
    this.contato = contato;
    this.contatoNum = contato.numero;
  }
  public Contato GetContato() {
    return contato;
  }
  public override string ToString() {
    return contato.nome;
  }
}
