using SmartDisplay.ControlPanelClient.Models.Common;
using SmartDisplay.ControlPanelClient.Models.Templates;
using SmartDisplay.ControlPanelClient.Services.Common;

namespace SmartDisplay.ControlPanelClient.Services.Templates;

public class MockTemplatesService : ITemplatesService
{
    private readonly List<TemplateDto> _templates = FallbackData.CreateTemplates();

    public Task<List<TemplateDto>> GetTemplatesAsync()
    {
        return Task.FromResult(_templates.ToList());
    }

    public Task<TemplateDto?> GetTemplateByIdAsync(string id)
    {
        var template = _templates.FirstOrDefault(template => template.Id == id);
        return Task.FromResult(template);
    }

    public Task<CreateTemplateResponse> CreateTemplateAsync(CreateTemplateRequest request)
    {
        var templateId = $"uuid-template-{Guid.NewGuid():N}";

        _templates.Add(new TemplateDto
        {
            Id = templateId,
            Name = request.Name,
            Config = request.Config
        });

        return Task.FromResult(new CreateTemplateResponse
        {
            Status = "success",
            TemplateId = templateId
        });
    }

    public Task<ApiStatusResponse> UpdateTemplateAsync(string id, UpdateTemplateRequest request)
    {
        var template = _templates.FirstOrDefault(template => template.Id == id);

        if (template is not null)
        {
            template.Name = request.Name;
            template.Config = request.Config;
        }

        return Task.FromResult(new ApiStatusResponse
        {
            Status = template is null ? "not_found" : "success"
        });
    }

    public Task<DeleteTemplateResponse> DeleteTemplateAsync(string id)
    {
        var template = _templates.FirstOrDefault(template => template.Id == id);

        if (template is not null)
        {
            _templates.Remove(template);
        }

        return Task.FromResult(new DeleteTemplateResponse
        {
            Status = template is null ? "not_found" : "success",
            DeletedId = id
        });
    }
}
