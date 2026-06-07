using SmartDisplay.ControlPanelClient.Services.Common;
using SmartDisplay.Shared.Models.Common;
using SmartDisplay.Shared.Models.Templates;

namespace SmartDisplay.ControlPanelClient.Services.Templates;

public class ApiTemplatesService : ITemplatesService
{
    private readonly AuthorizedApiClient _apiClient;

    public ApiTemplatesService(AuthorizedApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<List<TemplateDto>> GetTemplatesAsync()
    {
        try
        {
            return await _apiClient.GetAsync<List<TemplateDto>>("api/templates") ?? new List<TemplateDto>();
        }
        catch
        {
            return FallbackData.CreateTemplates();
        }
    }

    public async Task<TemplateDto?> GetTemplateByIdAsync(string id)
    {
        var templates = await GetTemplatesAsync();
        return templates.FirstOrDefault(template => template.Id == id);
    }

    public async Task<CreateTemplateResponse> CreateTemplateAsync(CreateTemplateRequest request)
    {
        NormalizeTemplateConfig(request.Config);

        try
        {
            var template = await _apiClient.PostAsync<TemplateDto>("api/templates", request);
            return template is null
                ? new CreateTemplateResponse { Status = ApiError.Status }
                : new CreateTemplateResponse { Status = "success", TemplateId = template.Id };
        }
        catch
        {
            return new CreateTemplateResponse { Status = ApiError.Status };
        }
    }

    public async Task<ApiStatusResponse> UpdateTemplateAsync(string id, UpdateTemplateRequest request)
    {
        NormalizeTemplateConfig(request.Config);

        try
        {
            await _apiClient.PutAsync<TemplateDto>($"api/templates/{Uri.EscapeDataString(id)}", request);
            return new ApiStatusResponse { Status = "success" };
        }
        catch
        {
            return new ApiStatusResponse { Status = ApiError.Status };
        }
    }

    public async Task<DeleteTemplateResponse> DeleteTemplateAsync(string id)
    {
        try
        {
            await _apiClient.PatchAsync<string>($"api/templates/{Uri.EscapeDataString(id)}/delete");
            return new DeleteTemplateResponse { Status = "success", DeletedId = id };
        }
        catch
        {
            return new DeleteTemplateResponse { Status = ApiError.Status, DeletedId = id };
        }
    }

    private static void NormalizeTemplateConfig(TemplateConfigDto config)
    {
        var widgets = config.GetEffectiveWidgets();
        config.SetWidgetItems(widgets);
    }
}
