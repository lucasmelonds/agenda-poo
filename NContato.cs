using System.Collections.Generic;
using System.Linq;


class NContato {
  private NContato() { }
  static NContato obj = new NContato();
  public static NContato Singleton { get => obj; }
  private List<Contato> contatos = new List<Contato>();
  public void Abrir() {
    Arquivo<List<Contato>> f = new Arquivo<List<Contato>>();
    contatos = f.Abrir("./contatos.xml");
    AtualizarCategoria();
  }

  private void AtualizarCategoria() {
    for(int i = 0; i < contatos.Count; i++) {
      Contato p = contatos[i];
      Categoria c = NCategoria.Singleton.Listar(p.CategoriaId);
      if (c != null) {
        p.SetCategoria(c);
        c.Inserir(p);
      }
    }
  }

  public void Salvar() {
    Arquivo<List<Contato>> f = new Arquivo<List<Contato>>();
    f.Salvar("./contatos.xml", Listar());
  }
  public List<Contato> Listar() {
      contatos.Sort();
      return contatos;
  }
  public Contato Listar(long num) {
      for (int i = 0 ; i < contatos.Count ; ++i) if (contatos[i].numero == num) return contatos[i];
  return null;
  }
  public void Inserir (Contato num) {
    long max = 0;
    foreach (Contato obj in contatos)
        if(obj.numero > max) max = obj.numero;
    num.numero = num.numero;
    contatos.Add(num);
  }
  public void Atualizar(Contato c) {
    Contato c_atual = Listar(c.numero);
    c_atual.nome = c.nome;
    c_atual.email = c.email;
    c_atual.nasc = c.nasc;
    c_atual.categoria = c.categoria;
  }
  public void Excluir(Contato c) {
    if (c != null) contatos.Remove(c);
  }
  public Contato Buscar(long numero) {
    Contato c_atual = Listar(numero);
    for (int i = 0 ; i < contatos.Count ; i++)
      if (contatos[i].numero == numero) return c_atual;
    return null;
  }
  
}
