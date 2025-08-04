using Blog.Api.WebSocket.Hubs;
using Blog.Application.Common.Interfaces;
using Blog.Application.Features.Posts.Dtos;
using Microsoft.AspNetCore.SignalR;
using static Blog.Api.Constants;

namespace Blog.Api.WebSocket
{
    public class PostNotifierService(IHubContext<PostHub> hubContext) : IPostNotifierService
    {
        private readonly IHubContext<PostHub> _hubContext = hubContext;

        public async Task PostCreated(NotifyPostCreatedDto post)
        {
            await _hubContext.Clients.All.SendAsync(NotifyMethods.ReceivePost, post);
        }
    }
}
