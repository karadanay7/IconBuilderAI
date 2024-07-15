using MediatR;
using MextFullstackSaaS.Application;
using MextFullstackSaaS.Domain.Entities;
using Microsoft.AspNetCore.SignalR;

namespace MextFullstackSaaS.WebApi;

public class OrderHub : Hub
{
    private readonly ISender _mediatr;
    public OrderHub(ISender mediatr)
    {
        _mediatr = mediatr;
    }
       public async Task NewOrderAddedAsync(List<string> newUrls)
    {
       
    }
    public async Task<List<string>> GetAllCommunityAsync()
    {
        return  await _mediatr.Send(new OrderGetAllCommunityQuery());
    }


}
