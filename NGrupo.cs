using System;
using System.Collections.Generic;

class NGrupo {
  private NGrupo() { }
  static NGrupo obj = new NGrupo();
  public static NGrupo Singleton { get => obj; }
  private List<Grupo> grupos = new List<Grupo>();
  public void Abrir() {
    Arquivo<List<Grupo>> f = new Arquivo<List<Grupo>>();
    grupos = f.Abrir("./grupos.xml");
  }
  public void Salvar() {
    Arquivo<List<Grupo>> f = new Arquivo<List<Grupo>>();
    f.Salvar("./grupos.xml", Listar());
  }
  public List<Grupo> Listar() {
    return grupos;
  }
  public Grupo ListarGP(int id) {
    for (int i = 0 ; i < grupos.Count ; ++i) if (grupos[i].id == id) return grupos[i];
  return null;
  }

  public void Inserir (Grupo g) {
    int max = 0;
    foreach (Grupo obj in grupos)
        if(obj.id > max) max = obj.id;
    g.id = g.id;
    grupos.Add(g);
  }
  
  public void Atualizar(Grupo g) {
    Grupo g_atual = ListarGP(g.id);
    g_atual.grupos = g.grupos;
    g_atual.contato = g.contato;
  }
  public void Excluir(Grupo g) {
    if (g != null) grupos.Remove(g);
  }
  public List<UserGrupo> UserListar(Grupo g) {
    return g.UserListar();
  }

  public void UserInserir(Grupo g, Contato c) {
    g.UserInserir(c);
  }  

  public void UserExcluir(Grupo g) {
    g.UserExcluir();
  }
}
