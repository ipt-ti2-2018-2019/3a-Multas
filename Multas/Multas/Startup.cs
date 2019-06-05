using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Multas.Models;
using Owin;

namespace Multas {

// classe a ser acedida e 'executada' a quando da 1ª execução da aplicação
   public partial class Startup {

      public void Configuration(IAppBuilder app) {
         ConfigureAuth(app);

         // invocar a execução do método que carrega os dados na BD
         iniciaAplicacao();
      }


      /// <summary>
      /// cria, caso não existam, as Roles de suporte à aplicação: Agente, Funcionario, Condutor
      /// cria, nesse caso, também, um utilizador...
      /// </summary>
      private void iniciaAplicacao() {

         // identifica a base de dados de serviço à aplicação
         MultasDB db = new MultasDB();

         var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
         var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

         // criar a Role 'Agentes'
         if(!roleManager.RoleExists("Agentes")) {
            // não existe a 'role'
            // então, criar essa role
            var role = new IdentityRole();
            role.Name = "Agentes";
            roleManager.Create(role);
         }

         // criar a Role 'RecursosHumanos'
         if(!roleManager.RoleExists("RecursosHumanos")) {
            // não existe a 'role'
            // então, criar essa role
            var role = new IdentityRole();
            role.Name = "RecursosHumanos";
            roleManager.Create(role);
         }

         // criar a Role 'GestorMultas'
         if(!roleManager.RoleExists("GestorMultas")) {
            // não existe a 'role'
            // então, criar essa role
            var role = new IdentityRole();
            role.Name = "GestorMultas";
            roleManager.Create(role);
         }


         // criar um utilizador 'Agente'
         var user = new ApplicationUser();
         user.UserName = "agente@mail.pt";
         user.Email = "agente@mail.pt";
        // user.Nome = "Luís Freitas";
         string userPWD = "123_Asd";   //  wuH4)al
         var chkUser = userManager.Create(user, userPWD);

         //Adicionar o Utilizador à respetiva Role-Agente-
         if(chkUser.Succeeded) {
            var result1 = userManager.AddToRole(user.Id, "Agentes");
         }
      }

      // https://code.msdn.microsoft.com/ASPNET-MVC-5-Security-And-44cbdb97





   }
}
