using System;
using System.Collections.Generic;
using System.Linq;

class NSMS {
    private NSMS() { }
    static NSMS obj = new NSMS();
    public static NSMS Singleton { get => obj; }
    private List<SMS> SMS = new List<SMS> ();
    public void Abrir() {
    Arquivo<List<SMS>> f = new Arquivo<List<SMS>>();
    SMS = f.Abrir("./sms.xml");
  }
  public void Salvar() {
    Arquivo<List<SMS>> f = new Arquivo<List<SMS>>();
    f.Salvar("./sms.xml", Listar());
  }
    public List<SMS> Listar() {
        return SMS;
    }
    public List<SMS> Listar(Contato c) {
    return SMS.Where(v => v.Contato == c).ToList();
  }
    public void Inserir (SMS num) {
        string s = "";
        foreach (SMS obj in SMS)
            if(obj.Texto == s) s = obj.Texto;
        num.Texto = num.Texto;
        SMS.Add(num);
    }
    public void Excluir(SMS c) {
        if (c != null) SMS.Remove(c);
    }
}
