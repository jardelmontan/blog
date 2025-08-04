using Blog.Client.WebSocket;
using Microsoft.AspNetCore.SignalR.Client;

Console.WriteLine("Pressione qualquer tecla para conectar");
Console.ReadLine();

var hubUrl = "http://localhost:5001/posthub";

var connection = new HubConnectionBuilder()
    .WithUrl(hubUrl)
    .WithAutomaticReconnect()
    .Build();

connection.On<CreatedPostDto>("ReceivePostNotification", (post) =>
{
    Console.WriteLine("\n--------- Novo Post Recebido ---------");
    Console.WriteLine($"ID: {post.PostId}");
    Console.WriteLine($"Título: {post.Title}");
    Console.WriteLine($"Author: {post.Author}");
    Console.WriteLine($"Criado Em: {post.CreatedAt.ToLocalTime()}");
    Console.WriteLine("------------------------------------------\n");
});

try
{
    Console.WriteLine("Iniciando conexão...");
    await connection.StartAsync();
    Console.WriteLine($"Conectado ao Hub com ID: {connection.ConnectionId}");
    Console.WriteLine($"Aguardando notificações...");
}
catch (Exception ex)
{
    Console.WriteLine($"Erro ao conectar: {ex.Message}");
    return;
}

Console.WriteLine("Pressione 'Q' para sair.");
while (Console.ReadLine()?.ToUpper() != "Q")
{
}

await connection.StopAsync();
