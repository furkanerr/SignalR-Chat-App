using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;
using ChatApp.Data;
using ChatApp.Models;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Hub
{
    public class ChatHub:Microsoft.AspNetCore.SignalR.Hub
    {

        public async Task GetNickName(string name)
        {
            Client client = new Client
            {
                ConnectionId = Context.ConnectionId,
                Name = name
            };


            ClientBilgileri.KisiList.Add(client);
            await Clients.Others.SendAsync("kullaniciKatildi",name);
            await Clients.All.SendAsync("clientsIsimleriAl", ClientBilgileri.KisiList);
        }
        

        public async Task SendMessage(string message,string user)
        {
          await  Clients.All.SendAsync("receiveMessage", message, user);
         
         
        }

        public async Task KisiyeMesajGonder(string connectionId,string message)
        {
            await Clients.Client(connectionId).SendAsync("kisiyeGonder", message);
        }
        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("connecitonIdAl", Context.ConnectionId);
        }
    }
}