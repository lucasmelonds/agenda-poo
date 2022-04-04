using System;
using System.Collections.Generic;

public class Grupo {
  public int id {get; set;}
  public string grupos {get; set;}
  public Contato contato {get; set;}
  private List<UserGrupo> usergp = new List<UserGrupo>();
  
  public int Id {get => id; set => id = value;}
  public string Grupos {get => grupos; set => grupos = value;}
  public List<UserGrupo> Usergp { get => usergp; set => usergp = value; }
  public Grupo () { }
  public Grupo (int id, string grupos)
   {
    this.id = id;
    this.grupos = grupos;
  }
  public void SetContato(Contato contato) {
    this.contato = contato;
  }
  public override string ToString() {
    return $"({id}) GRUPO: {grupos}\n*Membros*";
  }
  private UserGrupo UserListar(Contato c) {
    foreach(UserGrupo ug in usergp) 
      if (ug.GetContato() == c) return ug;
    return null;
  }
  public List<UserGrupo> UserListar() {
    return usergp;
  }
  public void UserInserir(Contato c) {
    UserGrupo user = UserListar(c);
    if (user == null) {
      user = new UserGrupo(c);
      usergp.Add(user);
    }
  }
  public void UserExcluir() {
    usergp.Clear();
  }
}
