using System.Text;
using MextFullstackSaaS.Application.Common.Interfaces;
using MextFullstackSaaS.Application.Common.Models.OpenAI;
using MextFullstackSaaS.Domain.Enums;
using Microsoft.Extensions.Primitives;
using OpenAI.ObjectModels;
using OpenAI.ObjectModels.RequestModels;
using OpenAI.ObjectModels.ResponseModels.ImageResponseModel;


namespace MextFullstackSaaS.Infrastructure.Services;

public class OpenAIManager:IOpenAIService
{
    private readonly ICurrentUserService _currentUserService;
    private readonly OpenAI.Interfaces.IOpenAIService _openAiService;

    public OpenAIManager(OpenAI.Interfaces.IOpenAIService openAiService, ICurrentUserService currentUserService)
    {
        _openAiService = openAiService;
        _currentUserService = currentUserService;
    }

    public async Task<List<string>> DallECreateIconAsync(DallECreateIconRequestDto requestDto, CancellationToken cancellationToken)
    {
        if (requestDto.Model == AIModelType.DallE3)
        {
            List<Task<ImageCreateResponse>> openAITasks = new();
            
            for (int i = 0; i < requestDto.Quantity; i++)
            {
                openAITasks.Add(_openAiService.Image.CreateImage(new ImageCreateRequest
                {
                    Prompt = CreateIconPrompt(requestDto),
                    N = 1,
                    Size = GetSize(requestDto.Size),
                    ResponseFormat = StaticValues.ImageStatics.ResponseFormat.Base64,
                    User = _currentUserService.UserId.ToString(),
                    Model = Models.Dall_e_3
                },cancellationToken));
            }
            
            await Task.WhenAll(openAITasks);

            var responses = await Task.WhenAll(openAITasks);

            return responses
                .SelectMany(response => response.Results.Select(result => result.B64))
                .ToList();
        }
       
         
        var imageResult = await _openAiService.Image.CreateImage(new ImageCreateRequest
        {
            Prompt = CreateIconPrompt(requestDto),
            N = requestDto.Quantity,
            Size = GetSize(requestDto.Size),
            ResponseFormat = StaticValues.ImageStatics.ResponseFormat.Base64,
            User = _currentUserService.UserId.ToString(),
            Model = Models.Dall_e_3
        },cancellationToken);
// TODO: Add error handling / If the model is Dall-e-3, Image size must be at least 1024x1024
        if (!imageResult.Successful)
        {
            
        }
        
        return imageResult
            .Results
            .Select(x => x.B64)
            .ToList();
       
    }
    
    private string GetSize(IconSize size)
    {
        return size switch
        {
            IconSize.Small => StaticValues.ImageStatics.Size.Size256,
            IconSize.Medium => StaticValues.ImageStatics.Size.Size512,
            IconSize.Large => StaticValues.ImageStatics.Size.Size1024,
            _ => StaticValues.ImageStatics.Size.Size256
        };
    }
    
    private string CreateIconPrompt(DallECreateIconRequestDto request)
    {
 var promptBuilder = new StringBuilder();
    
    promptBuilder.AppendLine("You are a world-class Icon Designer AI. Please generate an icon for a mobile app that fits the full screen. I will tip you $1000 if I like it.");
    
    promptBuilder.AppendLine($"The icon should fit the full size of the  <Size>{request.Size}</Size> designated area. Ensure it completely fills the space without any padding, borders, or empty areas. The icon should be visually striking and optimized for clarity and detail. Use the following specifications:");
  promptBuilder.Append($" Be carefull to match icon color with hash code.<Colour> : #{request.ColourCode}</Colour>");
    promptBuilder.AppendLine($"<DesignType>{request.DesignType}</DesignType>");

    promptBuilder.AppendLine($"<Shape>{request.Shape}</Shape>");
    promptBuilder.AppendLine($"<Size>{request.Size}</Size>");
    promptBuilder.AppendLine($"<Description>{request.Description}</Description>");
    
    return promptBuilder.ToString();
    }


}