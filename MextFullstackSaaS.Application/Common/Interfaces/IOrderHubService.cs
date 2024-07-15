namespace MextFullstackSaaS.Application;

public interface IOrderHubService
{
  Task NewOrderAddedAsync(List<string> urls, CancellationToken cancellationToken);

}
