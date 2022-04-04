using System;
using System.Xml.Serialization;
using System.Text;
using System.IO;

public class Categoria {
  private int id;
  private string descricao;
  private Contato[] contatos = new Contato[10];
  private int np;
  public int Id { get => id; set => id = value; }
  public string Descricao { get => descricao; set => descricao = value; }
  public Categoria() { }

  public Categoria(int id, string descricao) {
    this.id = id;
    this.descricao = descricao;
  }

  public void SetId(int id) {
    this.id = id;
  }
  public void SetDescricao(string descricao) {
    this.descricao = descricao;
  }
  public int GetId() {
    return id;
  }
  public string GetDescricao() {
    return descricao;
  }

  public Contato[] Listar() {
    Contato[] c = new Contato[np];
    Array.Copy(contatos, c, np);
    return c;
  }
  public void Inserir(Contato p) {
    if (np == contatos.Length) {
      Array.Resize(ref contatos, 2 * contatos.Length);
    }
    contatos[np] = p;
    np++;
  }
  private int Indice(Contato p) {
    for (int i = 0; i < np; i++)
      if (contatos[i] == p) return i;
    return -1;  
  }
  public void Excluir(Contato p) {
    int n = Indice(p);
    if (n == -1) return;
    for (int i = n; i < np - 1; i++)
      contatos[i] = contatos[i + 1];
    np--;  
  }
  public override string ToString() {
    return id + " - " + descricao;
  }
}
