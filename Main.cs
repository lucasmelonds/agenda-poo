using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Linq;

class MainClass {
  private static NContato ncontato = NContato.Singleton;
  private static NCategoria ncategoria = NCategoria.Singleton;
  private static NGrupo ngrupo = NGrupo.Singleton;
  private static NSMS nsms = NSMS.Singleton;
  public static void Main (string[] args) {
    Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");
    try {
      ncategoria.Abrir();
      ncontato.Abrir();
      ngrupo.Abrir();
      nsms.Abrir();
    }
    catch (Exception erro) {
      Console.WriteLine(erro.Message);
    }
    int op = 0;
    Console.WriteLine("---AGENDA---");
    do {
      try {
          op = Menu();
          switch(op) {
            case 1 : InserirContato(); break;
            case 2 : ListarContato(); break;
            case 3 : AtualizarContato(); break;
            case 4 : ExcluirContato(); break;
            case 5 : BuscarContato(); break;
            case 6 : InserirCat(); break;
            case 7 : ListarCat(); break;
            case 8 : AtualizarCat(); break;
            case 9 : ExcluirCat(); break;
            case 10 : EnviarSMS(); break;
            case 11 : ListarSMS(); break;
            case 12 : CriarGrupo(); break;
            case 13 : ListarGrupo(); break;
            case 14 : AddUser(); break;
            case 15 : ExcluirGP(); break;
          }
          if (op == 9) switch(op) {case 1 : EnviarSMS(); break;}
        }
      catch (Exception erro) {
        Console.WriteLine(erro.Message);
        op = 100;
      }
    } while(op != 0);
    try {
      ncategoria.Salvar();
      ncontato.Salvar();
      ngrupo.Salvar();
      nsms.Salvar();
    }
    catch (Exception erro) {
      Console.WriteLine(erro.Message);
    }
  }
    public static int Menu() {
    Console.WriteLine("MENU\n------------------");
    Console.WriteLine("1 - Adicionar um contato");
    Console.WriteLine("2 - Listar todos os contatos");
    Console.WriteLine("3 - Atualizar contato");
    Console.WriteLine("4 - Excluir contato");
    Console.WriteLine("5 - Buscar contato");
    Console.WriteLine("------------------");
    Console.WriteLine("6 - Adicionar uma categoria");
    Console.WriteLine("7 - Listar todas categorias");
    Console.WriteLine("8 - Atualizar categoria");
    Console.WriteLine("9 - Excluir categoria");
    Console.WriteLine("------------------");
    Console.WriteLine("10 - SMS");
    Console.WriteLine("11 - Caixa de Entrada");
    Console.WriteLine("------------------");
    Console.WriteLine("12 - Criar grupo");
    Console.WriteLine("13 - Listar grupos");
    Console.WriteLine("14 - Adicionar membros");
    Console.WriteLine("15 - Excluir grupos");
    Console.WriteLine("------------------");
    Console.WriteLine("0 - Finalizar");
    Console.WriteLine("Informe a opcao que deseja executar: ");
    int op = int.Parse(Console.ReadLine());
    return op;
  }
  public static void ListarContato() {
    Console.WriteLine("--- LISTA DE CONTATOS ---");
    List<Contato> lc = ncontato.Listar();
    if (lc.Count == 0) {
      Console.WriteLine("Voce nao tem nenhum contato salvo.");
      return;
    }
    foreach (Contato c in lc) Console.WriteLine(c);
    Console.WriteLine();
    
    var lista = from m in lc
                group m by m.nasc.Month into Mes
                orderby Mes.Key ascending
                select Mes;
      foreach (var mes in lista) {
        Console.WriteLine(string.Format($"Mes: {mes.Key}"));
        foreach (var c in mes) Console.WriteLine($"\t{c.nome}");
      }
      Console.WriteLine();
  }
  public static void ListarCat() {
    Console.WriteLine("--- LISTA DE CATEGORIAS ---");
    Categoria[] lc = ncategoria.Listar();
    if (lc.Length == 0) {
      Console.WriteLine("Sem categorias");
      return;
    }
    foreach (Categoria c in lc) Console.WriteLine(c);
    Console.WriteLine();
  }
  public static void ListarGrupo() {
    Console.WriteLine("--- LISTA DE GRUPOS ---");
    List<Grupo> lc = ngrupo.Listar();
    if (lc.Count == 0) {
      Console.WriteLine("Sem grupos");
      return;
    }
    foreach (Grupo c in lc) {
       Console.WriteLine(c);
      foreach (UserGrupo ugp in ngrupo.UserListar(c))
        Console.WriteLine(ugp);
    }
    Console.WriteLine();
  }
  public static void InserirContato() {
    Console.WriteLine("--- ADICIONAR CONTATO ---");
    Console.WriteLine("Informe o nome do contato: ");
    string nome = Console.ReadLine();
    Console.WriteLine("Informe o numero do contato: ");
    long numero = long.Parse(Console.ReadLine());
    Console.WriteLine("Informe o email do contato: ");
    string email = Console.ReadLine();
    Console.WriteLine("Informe a data de nascimento (dd/mm/yyyy): ");
    DateTime nasc = DateTime.Parse(Console.ReadLine());
    ListarCat();
    Console.Write("Informe a categoria do contato: ");
    int id = int.Parse(Console.ReadLine());
    Categoria cat = ncategoria.Listar(id);
    Contato f = new Contato(nome, numero, email, nasc, cat);
    ncontato.Inserir(f);
  }
public static void InserirCat() {
    Console.WriteLine("--- ADICIONAR CATEGORIA ---");
    Console.WriteLine("Informe o id da categoria: ");
    int id = int.Parse(Console.ReadLine());
    Console.WriteLine("Informe a categoria do contato: ");
    string cat = Console.ReadLine();
    Categoria c = new Categoria(id, cat);
    ncategoria.Inserir(c);
  }
  public static void CriarGrupo() {
    Console.WriteLine("--- ADICIONAR GRUPO ---");
    Console.WriteLine("Digite o id do grupo: ");
    int id = int.Parse(Console.ReadLine());
    Console.WriteLine("Nome do grupo: ");
    string gp = Console.ReadLine();
    Grupo grupo = new Grupo(id, gp);
    ngrupo.Inserir(grupo);
    ListarContato();
    Console.WriteLine("Adicionar membros: ");
    long conta = 0;
    do {
    conta = long.Parse(Console.ReadLine());
    Contato cn = ncontato.Listar(conta);
    if (cn != null) ngrupo.UserInserir(grupo, cn);
    else conta = 0;
    } while (conta != 0);
  }
  public static void AddUser() {
    Console.WriteLine("--- ADICIONAR MEMBRO ---");
    Console.WriteLine("Qual grupo você deseja adicionar mais um membro?");
    ListarGrupo();
    int id = int.Parse(Console.ReadLine());
    Grupo grupo = ngrupo.ListarGP(id);
    ListarContato();
    Console.WriteLine("Adicionar membros: ");
    long conta = long.Parse(Console.ReadLine());
    Contato cn = ncontato.Listar(conta);
    ngrupo.UserInserir(grupo, cn);
  }
  public static void AtualizarContato() {
    Console.WriteLine("--- ATUALIZAR CONTATO ---");
    ListarContato();
    Console.WriteLine("Informe o nome do contato: ");
    string nome = Console.ReadLine();
    Console.WriteLine("Informe o numero do contato: ");
    long numero = long.Parse(Console.ReadLine());
    Console.WriteLine("Informe o email do Contato: ");
    string email = Console.ReadLine();
    Console.WriteLine("Informe a data de nascimento (dd/mm/yyyy): ");
    DateTime nasc = DateTime.Parse(Console.ReadLine());
    ListarCat();
    Console.Write("Informe a categoria do contato: ");
    int cat = int.Parse(Console.ReadLine());
    Categoria s = ncategoria.Listar(cat);
    Contato f = new Contato(nome, numero, email, nasc, s);
    ncontato.Atualizar(f);
  }
  public static void AtualizarCat() {
    Console.WriteLine("--- ATUALIZAR CATEGORIA ---");
    ListarCat();
    Console.WriteLine("Informe o id da categoria: ");
    int id = int.Parse(Console.ReadLine());
    Console.WriteLine("Informe o novo nome da categoria: ");
    string nome = Console.ReadLine();
    Categoria f = new Categoria(id, nome);
    ncategoria.Atualizar(f);
  }
public static void ExcluirContato() {
  Console.WriteLine("--- EXCLUIR CONTATO ---");
  ListarContato();
  Console.WriteLine("Informe o numero que voce deseja excluir: ");
  long num = long.Parse(Console.ReadLine());
  Contato c = ncontato.Listar(num);
  ncontato.Excluir(c);
  }
public static void ExcluirCat() {
  Console.WriteLine("--- EXCLUIR CATEGORIA ---");
  ListarCat();
  Console.WriteLine("Informe o id da categoria que voce deseja excluir: ");
  int ns = int.Parse(Console.ReadLine());
  Categoria c = ncategoria.Listar(ns);
  ncategoria.Excluir(c);
  }
public static void ExcluirGP() {
  Console.WriteLine("--- EXCLUIR GRUPO ---");
  ListarGrupo();
  Console.WriteLine("Informe o id do grupo que voce deseja excluir: ");
  int ns = int.Parse(Console.ReadLine());
  Grupo c = ngrupo.ListarGP(ns);
  ngrupo.Excluir(c);
  ngrupo.UserExcluir(c);
  }
public static void BuscarContato() {
    Console.WriteLine("--- BUSCAR CONTATO ---");
    ListarContato();
    Console.WriteLine("Informe o numero do contato: ");
    long numero = long.Parse(Console.ReadLine());
    if (ncontato.Buscar(numero) != null) Console.WriteLine(ncontato.Buscar(numero));
    else Console.WriteLine("Contato nao encontrado");
  }
public static void EnviarSMS() {
  Console.WriteLine("Envie mensagem para um \n1 - Contato\n2 - Grupo");
  int x = int.Parse(Console.ReadLine());
  if (x == 1) {
    ListarContato();
    Console.WriteLine("Digite o numero do destinatario: ");
    long conta = long.Parse(Console.ReadLine());
    Contato cn = ncontato.Listar(conta);
    Console.WriteLine("Digite uma mensagem: ");
    string s = Console.ReadLine();
    Grupo grupo = null;
    SMS MSGs = new SMS(DateTime.Now, s, cn, grupo);
    nsms.Inserir(MSGs);
  }
  if (x == 2) {
    ListarGrupo();
    Console.WriteLine("Digite o id do grupo: ");
    int conta = int.Parse(Console.ReadLine());
    Grupo gp = ngrupo.ListarGP(conta);
    Console.WriteLine("Digite uma mensagem: ");
    string s = Console.ReadLine();
    Contato cn = null;
    SMS MSGs = new SMS(DateTime.Now, s, cn, gp);
    nsms.Inserir(MSGs);
  }
  }
  public static void ListarSMS() {
  Console.WriteLine("--- CAIXA DE ENTRADA ---");
  List<SMS> lc = nsms.Listar();
  if (lc.Count == 0) {
    Console.WriteLine("Sem mensagens");
    return;
    }
  foreach (SMS c in lc) {
    Console.WriteLine(c); 
    }
  }
}using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Linq;

class MainClass {
  private static NContato ncontato = NContato.Singleton;
  private static NCategoria ncategoria = NCategoria.Singleton;
  private static NGrupo ngrupo = NGrupo.Singleton;
  private static NSMS nsms = NSMS.Singleton;
  public static void Main (string[] args) {
    Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");
    try {
      ncategoria.Abrir();
      ncontato.Abrir();
      ngrupo.Abrir();
      nsms.Abrir();
    }
    catch (Exception erro) {
      Console.WriteLine(erro.Message);
    }
    int op = 0;
    Console.WriteLine("---AGENDA---");
    do {
      try {
          op = Menu();
          switch(op) {
            case 1 : InserirContato(); break;
            case 2 : ListarContato(); break;
            case 3 : AtualizarContato(); break;
            case 4 : ExcluirContato(); break;
            case 5 : BuscarContato(); break;
            case 6 : InserirCat(); break;
            case 7 : ListarCat(); break;
            case 8 : AtualizarCat(); break;
            case 9 : ExcluirCat(); break;
            case 10 : EnviarSMS(); break;
            case 11 : ListarSMS(); break;
            case 12 : CriarGrupo(); break;
            case 13 : ListarGrupo(); break;
            case 14 : AddUser(); break;
            case 15 : ExcluirGP(); break;
          }
          if (op == 9) switch(op) {case 1 : EnviarSMS(); break;}
        }
      catch (Exception erro) {
        Console.WriteLine(erro.Message);
        op = 100;
      }
    } while(op != 0);
    try {
      ncategoria.Salvar();
      ncontato.Salvar();
      ngrupo.Salvar();
      nsms.Salvar();
    }
    catch (Exception erro) {
      Console.WriteLine(erro.Message);
    }
  }
    public static int Menu() {
    Console.WriteLine("MENU\n------------------");
    Console.WriteLine("1 - Adicionar um contato");
    Console.WriteLine("2 - Listar todos os contatos");
    Console.WriteLine("3 - Atualizar contato");
    Console.WriteLine("4 - Excluir contato");
    Console.WriteLine("5 - Buscar contato");
    Console.WriteLine("------------------");
    Console.WriteLine("6 - Adicionar uma categoria");
    Console.WriteLine("7 - Listar todas categorias");
    Console.WriteLine("8 - Atualizar categoria");
    Console.WriteLine("9 - Excluir categoria");
    Console.WriteLine("------------------");
    Console.WriteLine("10 - SMS");
    Console.WriteLine("11 - Caixa de Entrada");
    Console.WriteLine("------------------");
    Console.WriteLine("12 - Criar grupo");
    Console.WriteLine("13 - Listar grupos");
    Console.WriteLine("14 - Adicionar membros");
    Console.WriteLine("15 - Excluir grupos");
    Console.WriteLine("------------------");
    Console.WriteLine("0 - Finalizar");
    Console.WriteLine("Informe a opcao que deseja executar: ");
    int op = int.Parse(Console.ReadLine());
    return op;
  }
  public static void ListarContato() {
    Console.WriteLine("--- LISTA DE CONTATOS ---");
    List<Contato> lc = ncontato.Listar();
    if (lc.Count == 0) {
      Console.WriteLine("Voce nao tem nenhum contato salvo.");
      return;
    }
    foreach (Contato c in lc) Console.WriteLine(c);
    Console.WriteLine();
    
    var lista = from m in lc
                group m by m.nasc.Month into Mes
                orderby Mes.Key ascending
                select Mes;
      foreach (var mes in lista) {
        Console.WriteLine(string.Format($"Mes: {mes.Key}"));
        foreach (var c in mes) Console.WriteLine($"\t{c.nome}");
      }
      Console.WriteLine();
  }
  public static void ListarCat() {
    Console.WriteLine("--- LISTA DE CATEGORIAS ---");
    Categoria[] lc = ncategoria.Listar();
    if (lc.Length == 0) {
      Console.WriteLine("Sem categorias");
      return;
    }
    foreach (Categoria c in lc) Console.WriteLine(c);
    Console.WriteLine();
  }
  public static void ListarGrupo() {
    Console.WriteLine("--- LISTA DE GRUPOS ---");
    List<Grupo> lc = ngrupo.Listar();
    if (lc.Count == 0) {
      Console.WriteLine("Sem grupos");
      return;
    }
    foreach (Grupo c in lc) {
       Console.WriteLine(c);
      foreach (UserGrupo ugp in ngrupo.UserListar(c))
        Console.WriteLine(ugp);
    }
    Console.WriteLine();
  }
  public static void InserirContato() {
    Console.WriteLine("--- ADICIONAR CONTATO ---");
    Console.WriteLine("Informe o nome do contato: ");
    string nome = Console.ReadLine();
    Console.WriteLine("Informe o numero do contato: ");
    long numero = long.Parse(Console.ReadLine());
    Console.WriteLine("Informe o email do contato: ");
    string email = Console.ReadLine();
    Console.WriteLine("Informe a data de nascimento (dd/mm/yyyy): ");
    DateTime nasc = DateTime.Parse(Console.ReadLine());
    ListarCat();
    Console.Write("Informe a categoria do contato: ");
    int id = int.Parse(Console.ReadLine());
    Categoria cat = ncategoria.Listar(id);
    Contato f = new Contato(nome, numero, email, nasc, cat);
    ncontato.Inserir(f);
  }
public static void InserirCat() {
    Console.WriteLine("--- ADICIONAR CATEGORIA ---");
    Console.WriteLine("Informe o id da categoria: ");
    int id = int.Parse(Console.ReadLine());
    Console.WriteLine("Informe a categoria do contato: ");
    string cat = Console.ReadLine();
    Categoria c = new Categoria(id, cat);
    ncategoria.Inserir(c);
  }
  public static void CriarGrupo() {
    Console.WriteLine("--- ADICIONAR GRUPO ---");
    Console.WriteLine("Digite o id do grupo: ");
    int id = int.Parse(Console.ReadLine());
    Console.WriteLine("Nome do grupo: ");
    string gp = Console.ReadLine();
    Grupo grupo = new Grupo(id, gp);
    ngrupo.Inserir(grupo);
    ListarContato();
    Console.WriteLine("Adicionar membros: ");
    long conta = 0;
    do {
    conta = long.Parse(Console.ReadLine());
    Contato cn = ncontato.Listar(conta);
    if (cn != null) ngrupo.UserInserir(grupo, cn);
    else conta = 0;
    } while (conta != 0);
  }
  public static void AddUser() {
    Console.WriteLine("--- ADICIONAR MEMBRO ---");
    Console.WriteLine("Qual grupo você deseja adicionar mais um membro?");
    ListarGrupo();
    int id = int.Parse(Console.ReadLine());
    Grupo grupo = ngrupo.ListarGP(id);
    ListarContato();
    Console.WriteLine("Adicionar membros: ");
    long conta = long.Parse(Console.ReadLine());
    Contato cn = ncontato.Listar(conta);
    ngrupo.UserInserir(grupo, cn);
  }
  public static void AtualizarContato() {
    Console.WriteLine("--- ATUALIZAR CONTATO ---");
    ListarContato();
    Console.WriteLine("Informe o nome do contato: ");
    string nome = Console.ReadLine();
    Console.WriteLine("Informe o numero do contato: ");
    long numero = long.Parse(Console.ReadLine());
    Console.WriteLine("Informe o email do Contato: ");
    string email = Console.ReadLine();
    Console.WriteLine("Informe a data de nascimento (dd/mm/yyyy): ");
    DateTime nasc = DateTime.Parse(Console.ReadLine());
    ListarCat();
    Console.Write("Informe a categoria do contato: ");
    int cat = int.Parse(Console.ReadLine());
    Categoria s = ncategoria.Listar(cat);
    Contato f = new Contato(nome, numero, email, nasc, s);
    ncontato.Atualizar(f);
  }
  public static void AtualizarCat() {
    Console.WriteLine("--- ATUALIZAR CATEGORIA ---");
    ListarCat();
    Console.WriteLine("Informe o id da categoria: ");
    int id = int.Parse(Console.ReadLine());
    Console.WriteLine("Informe o novo nome da categoria: ");
    string nome = Console.ReadLine();
    Categoria f = new Categoria(id, nome);
    ncategoria.Atualizar(f);
  }
public static void ExcluirContato() {
  Console.WriteLine("--- EXCLUIR CONTATO ---");
  ListarContato();
  Console.WriteLine("Informe o numero que voce deseja excluir: ");
  long num = long.Parse(Console.ReadLine());
  Contato c = ncontato.Listar(num);
  ncontato.Excluir(c);
  }
public static void ExcluirCat() {
  Console.WriteLine("--- EXCLUIR CATEGORIA ---");
  ListarCat();
  Console.WriteLine("Informe o id da categoria que voce deseja excluir: ");
  int ns = int.Parse(Console.ReadLine());
  Categoria c = ncategoria.Listar(ns);
  ncategoria.Excluir(c);
  }
public static void ExcluirGP() {
  Console.WriteLine("--- EXCLUIR GRUPO ---");
  ListarGrupo();
  Console.WriteLine("Informe o id do grupo que voce deseja excluir: ");
  int ns = int.Parse(Console.ReadLine());
  Grupo c = ngrupo.ListarGP(ns);
  ngrupo.Excluir(c);
  ngrupo.UserExcluir(c);
  }
public static void BuscarContato() {
    Console.WriteLine("--- BUSCAR CONTATO ---");
    ListarContato();
    Console.WriteLine("Informe o numero do contato: ");
    long numero = long.Parse(Console.ReadLine());
    if (ncontato.Buscar(numero) != null) Console.WriteLine(ncontato.Buscar(numero));
    else Console.WriteLine("Contato nao encontrado");
  }
public static void EnviarSMS() {
  Console.WriteLine("Envie mensagem para um \n1 - Contato\n2 - Grupo");
  int x = int.Parse(Console.ReadLine());
  if (x == 1) {
    ListarContato();
    Console.WriteLine("Digite o numero do destinatario: ");
    long conta = long.Parse(Console.ReadLine());
    Contato cn = ncontato.Listar(conta);
    Console.WriteLine("Digite uma mensagem: ");
    string s = Console.ReadLine();
    Grupo grupo = null;
    SMS MSGs = new SMS(DateTime.Now, s, cn, grupo);
    nsms.Inserir(MSGs);
  }
  if (x == 2) {
    ListarGrupo();
    Console.WriteLine("Digite o id do grupo: ");
    int conta = int.Parse(Console.ReadLine());
    Grupo gp = ngrupo.ListarGP(conta);
    Console.WriteLine("Digite uma mensagem: ");
    string s = Console.ReadLine();
    Contato cn = null;
    SMS MSGs = new SMS(DateTime.Now, s, cn, gp);
    nsms.Inserir(MSGs);
  }
  }
  public static void ListarSMS() {
  Console.WriteLine("--- CAIXA DE ENTRADA ---");
  List<SMS> lc = nsms.Listar();
  if (lc.Count == 0) {
    Console.WriteLine("Sem mensagens");
    return;
    }
  foreach (SMS c in lc) {
    Console.WriteLine(c); 
    }
  }
}
