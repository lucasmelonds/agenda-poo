using System;
using System.Collections.Generic;
using System.Linq;
public class Contato : IComparable<Contato> {
  
  public string nome {get; set;}
  public long numero {get; set;}
  public string email {get; set;}
  public DateTime nasc {get; set;}
  public Categoria categoria {get; set;}
  private int categoriaId;
  public int CategoriaId { get => categoriaId; set => categoriaId = value; }
  public Contato() { }
  public Contato (string nome, long numero, string email, DateTime nasc, Categoria categoria) {
    this.nome = nome;
    this.numero = numero;
    this.email = email;
    this.nasc = nasc;
    this.categoria = categoria;
  }
  public void SetCategoria(Categoria categoria) {
    this.categoria = categoria;
    this.categoriaId = categoria.Id;
  }
  public Categoria GetCategoria() {
    return categoria;
  }
  public int CompareTo(Contato obj) {
    return this.nome.CompareTo(obj.nome);
  }
  public override string ToString() {
    return "* " + categoria.Descricao + " *" + "\nNome - " + nome + "\nNumero do celular - " + numero + "\nEmail - " + email + "\nData de Nascimento: " + nasc.ToShortDateString();
  }
}
